
using System.Net.Http.Json;
using Fortytwo.PracticalTest.Application.Interfaces;

namespace Fortytwo.PracticalTest.Infrastructure.Repositories
{
    public class JsonPlaceholderTodoClient(HttpClient http) : IExternalTodoClient
    {
        private readonly HttpClient _http = http;

        private sealed record JsonPlaceholderTodo(int userId, int id, string title, bool completed);

        public async Task<string?> GetExternalTitleAsync(int id, CancellationToken ct = default)
        {
            var data = await _http.GetFromJsonAsync<JsonPlaceholderTodo>($"https://jsonplaceholder.typicode.com/todos/{id}", ct);
            return data?.title;
        }
    }
}
