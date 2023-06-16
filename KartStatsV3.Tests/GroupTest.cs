using KartStatsV3.BLL.Interfaces;
using KartStatsV3.BLL;
using KartStatsV3.Models;
using Moq;

namespace KartStatsV3.Tests
{
    [TestClass]
    public class GroupTest
    {
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<ICircuitRepository> _circuitRepositoryMock;
        private GroupService _groupService;

        [TestInitialize]
        public void Setup()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _circuitRepositoryMock = new Mock<ICircuitRepository>();
            _groupService = new GroupService(_groupRepositoryMock.Object, _circuitRepositoryMock.Object);
        }

        [TestMethod]
        public void GetGroup_WhenGroupExists_ReturnsCorrectGroup()
        {
            // Arrange
            var testGroupId = 1;
            var testName = "TestGroepje";
            var testAdminUserId = 2;
            var testAdminUserName = "Test";
            var expectedGroup = new Group(testGroupId, testName, testAdminUserId, testAdminUserName);

            _groupRepositoryMock.Setup(repo => repo.GetGroup(testGroupId)).Returns(expectedGroup);

            // Act
            var result = _groupService.GetGroup(testGroupId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedGroup.GroupId, result.GroupId);
            Assert.AreEqual(expectedGroup.Name, result.Name);
            Assert.AreEqual(expectedGroup.AdminUserId, result.AdminUserId);
            Assert.AreEqual(expectedGroup.AdminUserName, result.AdminUserName);
        }

        [TestMethod]
        public void GetGroup_WhenGroupDoesNotExist_ThrowsArgumentNullException()
        {
            // Arrange
            var testGroupId = 1;
            _groupRepositoryMock.Setup(repo => repo.GetGroup(testGroupId)).Returns((Group)null);
            
            // Act


            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => _groupService.GetGroup(testGroupId));
        }

        [TestMethod]
        public void UpdateGroup_WhenGroupExists_ReturnsTrue()
        {
            // Arrange
            var mockGroup = new Group(1, "Test Group", 1, "Admin");

            _groupRepositoryMock.Setup(repo => repo.UpdateGroup(mockGroup)).Returns(true);

            // Act
            var result = _groupService.UpdateGroup(mockGroup);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateGroup_WhenGroupIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Group nullGroup = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _groupService.UpdateGroup(nullGroup));
        }

        [TestMethod]
        public void DeleteGroup_WhenGroupExists_DeletesSuccessfully()
        {
            // Arrange
            int testGroupId = 1;
            var group = new Group(testGroupId, "Test Group", 1);
            _groupRepositoryMock.Setup(repo => repo.GetGroup(testGroupId)).Returns(group);
            _groupRepositoryMock.Setup(repo => repo.DeleteGroup(testGroupId)).Returns(true);

            // Act
            bool result = _groupService.DeleteGroup(testGroupId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteGroup_WhenGroupDoesNotExist_ThrowsArgumentNullException()
        {
            // Arrange
            int testGroupId = 1;
            _groupRepositoryMock.Setup(repo => repo.GetGroup(testGroupId)).Returns((Group)null);

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _groupService.DeleteGroup(testGroupId));
        }
    }
}   