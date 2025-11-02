
using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo> AddAsync(Todo todo);
        Task<Todo?> GetAsync(int id);
        Task<IReadOnlyList<Todo>> GetAllAsync();
    }
}
