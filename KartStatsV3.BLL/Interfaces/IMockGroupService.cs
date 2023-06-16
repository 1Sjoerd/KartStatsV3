using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KartStatsV3.Models;

namespace KartStatsV3.BLL.Interfaces
{
    public interface IMockGroupService
    {
        Group GetGroup(int id);
    }
}