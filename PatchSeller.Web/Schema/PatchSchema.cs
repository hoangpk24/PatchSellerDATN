using FluentValidation;
using PatchSeller.DAL.Models;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Schema
{
    public class PatchSchema
    {
        public class CreateUpdate : AbstractValidator<PatchModel>
        {
            public CreateUpdate()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Tên không được để trống")
                    .Length(2, 50).WithMessage("Tên có độ dài từ 2 đến 50 ký tự");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Mô tả không được để trống")
                    .Length(2, 500).WithMessage("Mô tả có độ dài từ 2 đến 500 ký tự");

                RuleFor(x => x.Price)
                    .NotEmpty().WithMessage("Giá tối thiểu từ 1.000đ")
                    .InclusiveBetween(1000, 9999999999999999).WithMessage("Giá tối thiểu 1.000đ");
            }
            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync((PatchModel)model);

                if (result.IsValid)
                    return Array.Empty<string>();

                return result.Errors
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage);
            };
        }
    }
}
