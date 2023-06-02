using KartStatsV3.BLL.Interfaces;
using KartStatsV3.DAL.Repositories;
using KartStatsV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartStatsV3.BLL
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public List<LapTime> GetGroupResults(int groupId, int circuitId)
        {
            return _resultRepository.GetGroupResults(groupId, circuitId);
        }
    }
}
