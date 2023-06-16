using KartStatsV3.BLL.Interfaces;
using KartStatsV3.BLL;
using KartStatsV3.Models;
using Moq;
using KartStatsV3.DAL.Repositories;
using YourNamespace.DAL.Repositories;

namespace KartStatsV3.Tests
{
    [TestClass]
    public class ResultTest
    {
        private MockResultRepository _resultRepository;
        private MockGroupService _groupService;
        private MockCircuitService _circuitService;
        private ResultService _resultService;

        [TestInitialize]
        public void SetUp()
        {
            _resultRepository = new MockResultRepository();
            _groupService = new MockGroupService();
            _circuitService = new MockCircuitService();
            _resultService = new ResultService(_resultRepository, _groupService, _circuitService);
        }

        [TestMethod]
        public void GetGroupResults_ReturnsResults_WhenGroupIdAndCircuitIdAreValid()
        {
            var results = _resultService.GetGroupResults(1, 1);

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void GetGroupResults_ThrowsException_WhenGroupIdIsInvalid()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _resultService.GetGroupResults(-1, 1));
        }

        [TestMethod]
        public void GetGroupResults_ThrowsException_WhenCircuitIdIsInvalid()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _resultService.GetGroupResults(1, -1));
        }
    }


}