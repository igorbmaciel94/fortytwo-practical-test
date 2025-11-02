using Fortytwo.PracticalTest.Application.Interfaces;
using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.Features
{
    public class TodoService(ITodoRepository repo, IExternalTodoClient ext) : ITodoService
    {
        private readonly ITodoRepository _repo = repo;
        private readonly IExternalTodoClient _ext = ext;

        public Task<IReadOnlyList<Todo>> GetAllAsync() => _repo.GetAllAsync();

        public async Task<Todo?> GetAsync(int id, CancellationToken ct = default)
        {
            var todo = await _repo.GetAsync(id);
            if (todo is null) return null;

            todo.ExternalTitle = await _ext.GetExternalTitleAsync(id, ct);
            return todo;
        }

        public async Task<Todo> CreateAsync(string title, bool isCompleted)
        {
            var todo = new Todo { Title = title, IsCompleted = isCompleted };
            return await _repo.AddAsync(todo);
        }
    }
}
