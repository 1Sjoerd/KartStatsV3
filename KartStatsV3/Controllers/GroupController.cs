using KartStatsV3.BLL;
using KartStatsV3.Models;
using KartStatsV3.BLL.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YourNamespace.DAL.Repositories;
using Microsoft.Ajax.Utilities;

namespace KartStatsV3.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IInviteService _inviteService;
        private readonly ICircuitService _circuitService;
        private readonly IUserService _userService;

        public GroupController(IGroupService groupService, IInviteService inviteService, ICircuitService circuitService, IUserService userService)
        {
            _groupService = groupService;
            _inviteService = inviteService;
            _userService = userService;
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
        public ActionResult Create(GroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Group group = new Group(viewModel.GroupId, viewModel.Name, viewModel.AdminUserId);
                _groupService.AddGroup(group);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var group = _groupService.GetGroup(id);
                var members = _groupService.GetGroupMembers(id);

                var viewModel = new GroupDetailsViewModel
                {
                    Group = group,
                    Members = members
                };

                return View(viewModel);
            }
            catch (ArgumentNullException ex)
            {
                var viewModel = new GroupDetailsViewModel
                {
                    Group = new Group(1, "test", 1, "Naam"),
                    Members = new List<User>(),
                    Message = ex.Message,
                };
                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var group = _groupService.GetGroup(id);
                var viewModel = new GroupViewModel
                {
                    GroupId = group.GroupId,
                    Name = group.Name,
                    AdminUserId = group.AdminUserId
                };

                return View(viewModel);
            }
            catch (ArgumentNullException ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(GroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var group = _groupService.GetGroup(viewModel.GroupId);
                if (group == null)
                {
                    return NotFound();
                }

                group.SetName(viewModel.Name); // Gebruik de nieuwe methode om de naam te zetten

                _groupService.UpdateGroup(group);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var group = _groupService.GetGroup(id);
                if (group == null)
                {
                    return NotFound();
                }
                return View(group);
            }
            catch (ArgumentNullException ex)
            {
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invite(InviteViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var toUserId = _userService.GetIdByUsername(model.ToUserName);
                    var fromUserId = _userService.GetIdByUsername(_userService.GetUsername());

                    var invite = new Invite(
                        groupId: model.GroupId,
                        fromUserId: (int)fromUserId,
                        toUserId: (int)toUserId,
                        status: InviteStatus.Pending
                    );

                    _inviteService.CreateInvite(invite);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        public ActionResult Invites()
        {
            try
            {
                var userId = _userService.GetIdByUsername(_userService.GetUsername());
                var invites = _inviteService.GetInvitesByToUserId((int)userId);
                return View(invites);
            }
            catch (ArgumentNullException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(new List<Invite>());
            }
        }

        [HttpPost]
        public ActionResult LeaveGroup(int groupId)
        {
            try
            {
                var currentUser = _userService.GetIdByUsername(_userService.GetUsername());
                bool success = _groupService.RemoveMember((int)currentUser, groupId);

                if (!success)
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveMember(int groupId, int userId)
        {
            try
            {
                bool success = _groupService.RemoveMember(userId, groupId);

                if (!success)
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }

            return RedirectToAction("Index");
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
            invite.UpdateStatus(InviteStatus.Accepted);
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
            invite.UpdateStatus(InviteStatus.Declined);
            _inviteService.UpdateInvite(invite);

            return RedirectToAction("Index");
        }

        public ActionResult AddCircuit(int groupId)
        {
            var availableCircuits = _circuitService.GetAllCircuits();
            ViewBag.Circuits = availableCircuits;

            var model = new GroupCircuitViewModel
            {
                GroupId = groupId
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddCircuit(GroupCircuitViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Voeg het circuit toe aan de groep
                _groupService.AddCircuitToGroup(model.GroupId, model.SelectedCircuitId);

                // Redirect naar de groepspagina of een andere gewenste actie
                return RedirectToAction("Index");
            }

            // Als de ModelState niet geldig is, laad het formulier opnieuw met de huidige modelgegevens
            ViewBag.Circuits = _circuitService.GetAllCircuits();
            return View(model);
        }
    }
}