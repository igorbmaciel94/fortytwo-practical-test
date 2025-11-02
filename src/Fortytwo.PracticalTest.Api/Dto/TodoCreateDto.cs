
using System.ComponentModel.DataAnnotations;

namespace Fortytwo.PracticalTest.Api.Dto
{
    public class TodoCreateDto
    {
        [Required, MinLength(1)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}
