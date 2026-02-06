using FluentValidation;
using PatchSeller.Web.Constant;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Schema
{
    public class GameSchema
    {
        public class CreateUpdate: AbstractValidator<GameModel>
        {
            public CreateUpdate() 
            {
                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Tiêu đề không được để trống")
                    .Length(2, 255).WithMessage("Tiêu đề có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Mô tả không được để trống");

                RuleFor(x => x.Developer)
                    .NotEmpty().WithMessage("Nhà phát triển không được để trống")
                    .Length(2, 255).WithMessage("Nhà phát triển có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.ReleaseDate)
                    .NotEmpty().WithMessage("Ngày phát hành không được để trống");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<GameModel>.CreateWithOptions((GameModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }

        public class Login : AbstractValidator<LoginModel>
        {
            public Login()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                    .Length(2, 255);

                RuleFor(x => x.PasswordHash)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<LoginModel>.CreateWithOptions((LoginModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }

        public class AdminLogin : AbstractValidator<LoginModel>
        {
            public AdminLogin()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                    .Length(2, 255);

                RuleFor(x => x.PasswordHash)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<LoginModel>.CreateWithOptions((LoginModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }

        public class ForgotPassword : AbstractValidator<ForgotModel>
        {
            public ForgotPassword()
            {
                RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty().WithMessage("Email không được để trống")
                    .EmailAddress().WithMessage("Email không hợp lệ")
                    .Length(1, 255).WithMessage("Email có độ dài tối đa 255 ký tự");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<ForgotModel>.CreateWithOptions((ForgotModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }

        public class ResetPassword : AbstractValidator<ChangePasswordModel>
        {
            public ResetPassword()
            {
                RuleFor(x => x.CurrentPassword)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");

                RuleFor(x => x.NewHashPassword)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<ChangePasswordModel>.CreateWithOptions((ChangePasswordModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }

        public class StaffChangePassword : AbstractValidator<StaffChangePasswordModel>
        {
            public StaffChangePassword()
            {
                RuleFor(x => x.CurrentPassword)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");
                RuleFor(x => x.NewHashPassword)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");
                RuleFor(x => x.ConfirmNewPassword)
                    .NotEmpty().WithMessage("Mật khẩu không được để trống")
                    .Matches(Constant.Constant.Regex.Password).WithMessage("Mật khẩu phải có từ 8 đến 16 ký tự chữ và số, bao gồm cả chữ hoa, chữ thường, số và ký hiệu")
                    .Length(2, 255).WithMessage("Mật khẩu có đội dài tối đa 255 ký tự");
            }
            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<StaffChangePasswordModel>.CreateWithOptions((StaffChangePasswordModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}
