using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.Models
{
    public class Invite
    {
        public int InviteId { get; private set; }
        public int GroupId { get; private set; }
        public int FromUserId { get; private set; }
        public int ToUserId { get; private set; }
        public InviteStatus Status { get; private set; }

        public Invite(int inviteId,int groupId, int fromUserId, int toUserId, InviteStatus status)
        {
            InviteId = inviteId;
            GroupId = groupId;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Status = status;
        }

        public Invite(int groupId, int fromUserId, int toUserId, InviteStatus status)
        {
            GroupId = groupId;
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Status = status;
        }

        public void UpdateStatus(InviteStatus newStatus)
        {
            Status = newStatus;
        }
    }

    public enum InviteStatus
    {
        Pending = 1,
        Accepted = 2,
        Declined = 3
    }
}
