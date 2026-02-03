using FluentValidation;
using PatchSeller.DAL.Models;

namespace PatchSeller.Web.Schema
{
    public class UserSchema
    {
        public class CreateUpdate : AbstractValidator<User>
        {
            public CreateUpdate()
            {
                RuleFor(x => x.FullName)
                    .NotEmpty().WithMessage("Tên không được để trống")
                    .Length(2, 255).WithMessage("Tên có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.PasswordHash)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");

                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("Tên không được để trống")
                    .Length(2, 255).WithMessage("Tên có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Email không được để trống")
                    .EmailAddress().WithMessage("Email không hợp lệ")
                    .Length(1, 255).WithMessage("Email có độ dài tối đa 255 ký tự");

                RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("Số điện thoại không được để trống")
                    .Matches(Constant.Constant.Regex.PhoneNumber).WithMessage("Số điện thoại không hợp lệ")
                    .Length(10, 10).WithMessage("Số điện thoại không hợp lệ");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<User>.CreateWithOptions((User)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}
