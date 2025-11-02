
using System.ComponentModel.DataAnnotations;
using Fortytwo.PracticalTest.Api.Dto;
using Xunit;

public class TodoCreateDtoValidationTests
{
    [Fact]
    public void Title_Is_Required()
    {
        var dto = new TodoCreateDto { Title = "" };
        var ctx = new ValidationContext(dto);
        var results = new System.Collections.Generic.List<ValidationResult>();
        var valid = Validator.TryValidateObject(dto, ctx, results, true);
        Assert.False(valid);
        Assert.Contains(results, r => r.MemberNames != null && System.Linq.Enumerable.Contains(r.MemberNames, "Title"));
    }
}
