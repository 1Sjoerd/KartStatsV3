using KartStatsV3.BLL.Interfaces;
using KartStatsV3.Models;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL
{
    public class InviteService : IInviteService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IGroupRepository _groupRepository;

        public InviteService(IInviteRepository inviteRepository, IGroupRepository groupRepository)
        {
            _inviteRepository = inviteRepository;
            _groupRepository = groupRepository;
        }

        public bool CreateInvite(Invite invite)
        {
            if (invite == null)
            {
                throw new ArgumentNullException("Invite niet gevonden.");
            }
            int toUser = invite.ToUserId;
            int fromUser = invite.FromUserId;
            var group = _groupRepository.GetGroup(invite.GroupId);

            if (group.AdminUserId != fromUser)
            {
                throw new InvalidOperationException("Alleen de beheerder kan gebruikers uitnodigen.");
            }

            if (_groupRepository.IsUserInGroup(invite.ToUserId, invite.GroupId))
            {
                throw new InvalidOperationException("Deze gebruiker is al een lid van de groep.");
            }

            if (group.AdminUserId == toUser)
            {
                throw new InvalidOperationException("De beheerder kan zichzelf niet uitnodigen.");
            }

            _inviteRepository.CreateInvite(invite);

            return true;
        }




        public List<Invite> GetInvitesByToUserId(int toUserId)
        {
            var invites = _inviteRepository.GetInvitesByToUserId(toUserId);

            if (invites == null)
            {
                throw new ArgumentNullException("Uitnodigingen kunnen niet worden geladen.");
            }

            return invites;
        }

        public Invite GetInvite(int inviteId)
        {
            var invite = _inviteRepository.GetInvite(inviteId);
            if (invite == null)
            {
                throw new ArgumentNullException("Invite niet gevonden");
            }
            return invite;
        }

        public bool UpdateInvite(Invite invite)
        {
            if (invite == null)
            {
                throw new ArgumentNullException("Invite niet gevonden");
            }
            return _inviteRepository.UpdateInvite(invite);
        }
    }
}
