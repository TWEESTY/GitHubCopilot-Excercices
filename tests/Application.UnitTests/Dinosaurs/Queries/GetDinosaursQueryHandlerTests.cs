using Copilot.Application.Dinosaurs.Queries.GetDinosaurs;
using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace Copilot.Application.UnitTests.Dinosaurs.Queries
{
    [TestFixture]
    public class GetDinosaursQueryHandlerTests
    {
        private Mock<IDinosaurRepository> _repositoryMock;
        private GetDinosaursQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IDinosaurRepository>();
            _handler = new GetDinosaursQueryHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnListOfDinosaurs()
        {
            // Arrange
            var dinosaurs = new List<Dinosaur> { new Dinosaur {Id = 1, Name = "T-Rex", Species = "Tyrannosaurus", Sex = "Male", CountryOfOrigin = "USA", NumberOfScales = 1000 } };
            _repositoryMock.Setup(repo => repo.GetList()).Returns(dinosaurs);

            // Act
            var result = await _handler.Handle(new GetDinosaursQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(dinosaurs);
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoDinosaursExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetList()).Returns(new List<Dinosaur>());

            // Act
            var result = await _handler.Handle(new GetDinosaursQuery(), CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
