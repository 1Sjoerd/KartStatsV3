using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IInviteService
    {
        bool CreateInvite(Invite invite);
        List<Invite> GetInvitesByToUserId(int toUserId);
        Invite GetInvite(int inviteId);
        bool UpdateInvite(Invite invite);
    }
}
