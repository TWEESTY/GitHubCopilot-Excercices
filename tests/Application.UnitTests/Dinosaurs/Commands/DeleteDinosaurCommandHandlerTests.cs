using Copilot.Application.Dinosaurs.Commands.DeleteDinosaur;
using Copilot.Application.Dinosaurs.Repositories;
using Moq;
using NUnit.Framework;

namespace Copilot.Application.UnitTests.Dinosaurs.Commands
{
    [TestFixture]
    public class DeleteDinosaurCommandHandlerTests
    {
        private Mock<IDinosaurRepository> _repositoryMock;
        private DeleteDinosaurCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IDinosaurRepository>();
            _handler = new DeleteDinosaurCommandHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldDeleteDinosaur()
        {
            // Arrange
            var command = new DeleteDinosaurCommand(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(repo => repo.Delete(1), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowException_WhenDinosaurDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Throws(new ArgumentException("Dinosaur not found"));
            var command = new DeleteDinosaurCommand(1);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
