
using Fortytwo.PracticalTest.Application.Interfaces;
using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Infrastructure.Repositories
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly List<Todo> _items = new();
        private int _nextId = 1;

        public Task<Todo> AddAsync(Todo todo)
        {
            todo.Id = _nextId++;
            _items.Add(todo);
            return Task.FromResult(todo);
        }

        public Task<IReadOnlyList<Todo>> GetAllAsync() => Task.FromResult((IReadOnlyList<Todo>)_items.ToList());

        public Task<Todo?> GetAsync(int id) => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
    }
}
