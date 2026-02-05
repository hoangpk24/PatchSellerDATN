using FluentValidation;
using PatchSeller.DAL.Models;

namespace PatchSeller.Web.Schema
{
    public class RankSchema
    {
        public class CreateUpdate : AbstractValidator<Rank>
        {
            public CreateUpdate()
            {
                RuleFor(x => x.RankName)
                    .NotEmpty().WithMessage("Tên không được để trống")
                    .Length(2, 255).WithMessage("Tên có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Mô tả không được để trống")
                    .Length(0, 1000).WithMessage("Mô tả có độ dài tối đa 500 ký tự");

                RuleFor(x => x.MiniumSpend)
                    .InclusiveBetween(0, double.MaxValue).WithMessage("Mốc áp dụng thiểu phải từ 0đ");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<Rank>.CreateWithOptions((Rank)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}
