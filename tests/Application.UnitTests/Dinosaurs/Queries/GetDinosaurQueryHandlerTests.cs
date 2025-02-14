using Copilot.Application.Dinosaurs.Queries.GetDinosaur;
using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace Copilot.Application.UnitTests.Dinosaurs.Queries
{
    [TestFixture]
    public class GetDinosaurQueryHandlerTests
    {
        private Mock<IDinosaurRepository> _repositoryMock;
        private GetDinosaurQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IDinosaurRepository>();
            _handler = new GetDinosaurQueryHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnDinosaur_WhenDinosaurExists()
        {
            // Arrange
            var dinosaur = new Dinosaur {Id = 1, Name = "T-Rex", Species = "Tyrannosaurus", Sex = "Male", CountryOfOrigin = "USA", NumberOfScales = 1000 };
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(dinosaur);

            // Act
            var result = await _handler.Handle(new GetDinosaurQuery(1), CancellationToken.None);

            // Assert
            result.Should().Be(dinosaur);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenDinosaurDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>()));

            // Act
            var result = await _handler.Handle(new GetDinosaurQuery(1), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
