using Copilot.Application.Dinosaurs.Commands.UpdateDinosaur;
using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Copilot.Application.UnitTests.Dinosaurs.Commands
{
    [TestFixture]
    public class UpdateDinosaurCommandHandlerTests
    {
        private Mock<IDinosaurRepository> _repositoryMock;
        private UpdateDinosaurCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IDinosaurRepository>();
            _handler = new UpdateDinosaurCommandHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdateDinosaur()
        {
            // Arrange
            var dinosaur = new Dinosaur { Id = 1, Name = "T-Rex", Species = "Tyrannosaurus", Sex = "Male", CountryOfOrigin = "USA", NumberOfScales = 1000 };
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(dinosaur);
            var command = new UpdateDinosaurCommand(1, "T-Rex", "Tyrannosaurus", "Male", "USA", 1000);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(repo => repo.Update(It.Is<Dinosaur>(d => d.Id == 1 && d.Name == "T-Rex")), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowException_WhenDinosaurDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Throws(new ArgumentNullException("Dinosaur not found"));
            var command = new UpdateDinosaurCommand(1, "T-Rex", "Tyrannosaurus", "Male", "USA", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
