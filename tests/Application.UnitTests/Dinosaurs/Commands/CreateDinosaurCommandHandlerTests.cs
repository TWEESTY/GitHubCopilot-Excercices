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
        private Mock<IValidator<CreateDinosaurCommand>> _validatorMock;
        private CreateDinosaurCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IDinosaurRepository>();
            _validatorMock = new Mock<IValidator<CreateDinosaurCommand>>();
            _handler = new CreateDinosaurCommandHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateDinosaur()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Male", "USA", 1000);
            var dinosaur = new Dinosaur { Id = 1, Name = "T-Rex", Species = "Tyrannosaurus", Sex = "Male", CountryOfOrigin = "USA", NumberOfScales = 1000};
            _repositoryMock.Setup(repo => repo.Create(It.IsAny<Dinosaur>())).Returns(dinosaur);
            _validatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>())).ReturnsAsync(new FluentValidation.Results.ValidationResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(dinosaur);
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNameExceedsMaxLength()
        {
            // Arrange
            var command = new CreateDinosaurCommand(new string('A', 21), "Tyrannosaurus", "Male", "USA", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenSexIsInvalid()
        {
            // Arrange
            var command = new CreateDinosaurCommand("T-Rex", "Tyrannosaurus", "Unknown", "USA", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNameIsEmpty()
        {
            // Arrange
            var command = new CreateDinosaurCommand("", "Tyrannosaurus", "Male", "USA", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenNameIsTooLong()
        {
            // Arrange
            var command = new CreateDinosaurCommand("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "Tyrannosaurus", "Male", "USA", 1000);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }


    }
}
