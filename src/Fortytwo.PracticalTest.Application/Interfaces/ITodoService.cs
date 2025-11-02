using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.Interfaces
{
    public interface ITodoService
    {
        Task<Todo?> GetAsync(int id, CancellationToken ct = default);
        Task<Todo> CreateAsync(string title, bool isCompleted);
    }
}
