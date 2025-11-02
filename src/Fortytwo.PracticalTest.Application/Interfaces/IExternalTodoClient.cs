namespace Fortytwo.PracticalTest.Application.Interfaces
{
    public interface IExternalTodoClient
    {
        Task<string?> GetExternalTitleAsync(int id, CancellationToken ct = default);
    }
}
