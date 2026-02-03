using FluentValidation;
using PatchSeller.DAL.Models;

namespace PatchSeller.Web.Schema
{
    public class CategorySchema
    {
        public class CreateUpdate : AbstractValidator<Category>
        {
            public CreateUpdate()
            {
                RuleFor(x => x.CategoryName)
                    .NotEmpty().WithMessage("Tên không được để trống")
                    .Length(2, 255).WithMessage("Tên có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.Description)
                    .Length(0, 1000).WithMessage("Mô tả có độ dài tối đa 500 ký tự");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<Category>.CreateWithOptions((Category)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}
