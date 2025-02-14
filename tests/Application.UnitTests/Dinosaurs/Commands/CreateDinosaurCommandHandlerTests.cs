using Copilot.Application.Dinosaurs.Commands.CreateDinosaur;
using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;
using FluentValidation;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace Copilot.Application.UnitTests.Dinosaurs.Commands
{
    [TestFixture]
    public class CreateDinosaurCommandHandlerTests
    {
        private Mock<IDinosaurRepository> _repositoryMock;
        private CreateDinosaurCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IDinosaurRepository>();
            _handler = new CreateDinosaurCommandHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateDinosaur()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Male", "US", 1000);
            var dinosaur = new Dinosaur { Id = 1, Name = "T-Rex", Species = "Tyrannosaurus", Sex = "Male", CountryOfOrigin = "US", NumberOfScales = 1000 };
            _repositoryMock.Setup(repo => repo.Create(It.IsAny<Dinosaur>())).Returns(dinosaur);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(dinosaur);
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNameExceedsMaxLength()
        {
            // Arrange
            var command = new CreateDinosaurCommand(new string('A', 21), "Tyrannosaurus", "Male", "US", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenSexIsInvalid()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Unknown", "US", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNameIsEmpty()
        {
            // Arrange
            var command = new CreateDinosaurCommand("", "Tyrannosaurus", "Male", "US", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenCountryOfOriginIsInvalid()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Male", "USA", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNumberOfScalesIsLessThan90()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Male", "US", 80);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNumberOfScalesIsOdd()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Male", "US", 101);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
