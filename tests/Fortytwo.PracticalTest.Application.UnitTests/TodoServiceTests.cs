using Fortytwo.PracticalTest.Application.Features;
using Fortytwo.PracticalTest.Application.Interfaces;
using Fortytwo.PracticalTest.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fortytwo.PracticalTest.Application.UnitTests
{
    public class TodoServiceTests
    {
        [Fact]
        public async Task GetAsync_Should_Enrich_With_ExternalTitle_When_Todo_Exists()
        {
            // Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(r => r.GetAsync(1)).ReturnsAsync(new Todo { Id = 1, Title = "Local", IsCompleted = false });

            var external = new Mock<IExternalTodoClient>();
            external.Setup(e => e.GetExternalTitleAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync("External Title");

            var logger = new Mock<ILogger<TodoService>>();
            var sut = new TodoService(repo.Object, external.Object);

            // Act
            var result = await sut.GetAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Local", result.Title);
            Assert.Equal("External Title", result.ExternalTitle);
        }

        [Fact]
        public async Task CreateAsync_Should_Call_Repo_Add_And_Return_Created_Entity()
        {
            // Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<Todo>()))
                .ReturnsAsync((Todo t) =>
                {
                    t.Id = 42; // mimic the in-memory repository behavior
                    return t;
                });

            var external = new Mock<IExternalTodoClient>();
            var sut = new TodoService(repo.Object, external.Object);

            // Act
            var created = await sut.CreateAsync("New Item", isCompleted: true);

            // Assert
            Assert.NotNull(created);
            Assert.Equal(42, created.Id);
            Assert.Equal("New Item", created.Title);
            Assert.True(created.IsCompleted);
            external.Verify(e => e.GetExternalTitleAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            repo.Verify(r => r.AddAsync(It.IsAny<Todo>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_From_Repository()
        {
            // Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new[]
                {
                    new Todo { Id = 1, Title = "A" },
                    new Todo { Id = 2, Title = "B" },
                });

            var external = new Mock<IExternalTodoClient>();
            var sut = new TodoService(repo.Object, external.Object);

            // Act
            var all = await sut.GetAllAsync();

            // Assert
            Assert.NotNull(all);
            Assert.Collection(all,
                x => Assert.Equal(1, x.Id),
                x => Assert.Equal(2, x.Id));
        }
    }
}
