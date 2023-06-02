using KartStatsV3.BLL;
using KartStatsV3.Models;
using KartStatsV3.BLL.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YourNamespace.DAL.Repositories;

namespace KartStatsV3.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IInviteService _inviteService;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ICircuitService _circuitService;
        private readonly UserService _userService;

        public GroupController(IGroupService groupService, IInviteService inviteService, IConfiguration configuration, IUserRepository userRepository, ICircuitService circuitService)
        {
            _groupService = groupService;
            _inviteService = inviteService;
            _configuration = configuration;
            _userRepository = userRepository;
            _userService = new UserService(_userRepository);
            _circuitService = circuitService;
        }


        public ActionResult Index()
        {
            var currentUserId = _userService.GetIdByUsername(_userService.GetUsername());
            var groups = _groupService.GetGroupsForUser((int)currentUserId);
            return View(groups);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Group group)
        {
            ModelState.Remove("AdminUserName");
            if (ModelState.IsValid)
            {
                _groupService.AddGroup(group);
                return RedirectToAction("Index");
            }

            return View(group);
        }

        public ActionResult Details(int id)
        {
            var group = _groupService.GetGroup(id);
            if (group == null)
            {
                return NotFound();
            }

            var members = _groupService.GetGroupMembers(id);

            var viewModel = new GroupDetailsViewModel
            {
                Group = group,
                Members = members
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var group = _groupService.GetGroup(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(Group group)
        {
            ModelState.Remove("AdminUserName");
            if (ModelState.IsValid)
            {
                _groupService.UpdateGroup(group);
                return RedirectToAction("Index");
            }
            return View(group);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var group = _groupService.GetGroup(id);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _groupService.DeleteGroup(id);
            return RedirectToAction("Index");
        }

        public ActionResult Invite(int groupId)
        {
            var model = new InviteViewModel
            {
                GroupId = groupId
            };

            return View();
        }

        public ActionResult Invites()
        {
            var userId = _userService.GetIdByUsername(_userService.GetUsername());
            var invites = _inviteService.GetInvitesByToUserId((int)userId);
            return View(invites);
        }

        [HttpPost]
        public ActionResult LeaveGroup(int groupId)
        {
            var group = _groupService.GetGroup(groupId);
            var currentUser = _userService.GetIdByUsername(_userService.GetUsername());

            if (group == null)
            {
                return NotFound();
            }

            if (group.AdminUserId == currentUser)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Beheerders kunnen hun eigen groep niet verlaten");
            }

            _groupService.RemoveMember((int)currentUser, groupId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveMember(int groupId, int userId)
        {
            var group = _groupService.GetGroup(groupId);
            var currentUser = _userService.GetIdByUsername(_userService.GetUsername());

            if (group == null)
            {
                return NotFound();
            }

            if (group.AdminUserId != currentUser)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Beheerders kunnen hun eigen groep niet verlaten");
            }

            _groupService.RemoveMember(userId, groupId);

            return RedirectToAction("Details", new RouteValueDictionary(new { id = groupId }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(InviteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var toUser = _userService.GetIdByUsername(model.ToUserName);
                var fromUserId = _userService.GetIdByUsername(_userService.GetUsername());
                var group = _groupService.GetGroup(model.GroupId);

                if (toUser == null)
                {
                    ModelState.AddModelError("", "Gebruikersnaam niet gevonden.");
                }
                else if (group.AdminUserId != fromUserId)
                {
                    ModelState.AddModelError("", "Alleen de beheerder kan gebruikers uitnodigen.");
                }
                else if (_groupService.IsUserInGroup((int)toUser, model.GroupId))
                {
                    ModelState.AddModelError("", "Deze gebruiker is al een lid van de groep.");
                }
                else if (group.AdminUserId == toUser)
                {
                    ModelState.AddModelError("", "De beheerder kan zichzelf niet uitnodigen.");
                }
                else
                {
                    var invite = new Invite
                    {
                        GroupId = model.GroupId,
                        FromUserId = (int)fromUserId,
                        ToUserId = (int)toUser,
                        Status = InviteStatus.Pending.ToString()
                    };
                    _inviteService.CreateInvite(invite);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }




        public ActionResult AcceptInvite(int inviteId)
        {
            var invite = _inviteService.GetInvite(inviteId);

            // Controleer of de uitnodiging bestaat en of de huidige gebruiker de ontvanger is
            if (invite == null || invite.ToUserId != _userService.GetIdByUsername(_userService.GetUsername()))
            {
                return RedirectToAction("Index");
            }

            // Update de status van de uitnodiging naar Geaccepteerd
            invite.Status = InviteStatus.Accepted.ToString();
            _inviteService.UpdateInvite(invite);

            // Voeg de gebruiker toe aan de groep
            _groupService.AddMember(invite.GroupId, invite.ToUserId);

            return RedirectToAction("Index");
        }

        public ActionResult DeclineInvite(int inviteId)
        {
            var invite = _inviteService.GetInvite(inviteId);

            // Controleer of de uitnodiging bestaat en of de huidige gebruiker de ontvanger is
            if (invite == null || invite.ToUserId != _userService.GetIdByUsername(_userService.GetUsername()))
            {
                return RedirectToAction("Index");
            }

            // Update de status van de uitnodiging naar Geweigerd
            invite.Status = InviteStatus.Declined.ToString();
            _inviteService.UpdateInvite(invite);

            return RedirectToAction("Index");
        }

        public ActionResult AddCircuitToGroup(int groupId)
        {
            var circuits = _circuitService.GetAllCircuits(); // Haal alle circuits op.
            var groupCircuitViewModel = new GroupCircuitViewModel
            {
                GroupId = groupId,
                Circuits = circuits
            };
            return View(groupCircuitViewModel);
        }

    }
}