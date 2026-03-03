using FluentValidation;
using PatchSeller.Web.Constant;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Schema
{
    public class PatchVersionSchema
    {
        public class CreateUpdate: AbstractValidator<PatchVersionModel>
        {
            public CreateUpdate() 
            {
                RuleFor(x => x.Changelog)
                    .NotEmpty().WithMessage("Nội dung thay đổi không được để trống")
                    .Length(2, 500).WithMessage("Nội dung thay đổi có độ dài từ 2 đến 500 ký tự");

                RuleFor(x => x.Changelog)
                    .NotEmpty().WithMessage("Mô tả không được để trống");

                RuleFor(x => x.ExtractionPassword)
                    .Length(0, 255).WithMessage("Mật khẩu có độ dài tối đa 255 ký tự");

                RuleFor(x => x.VersionName)
                    .NotEmpty().WithMessage("Tên phiên bản không được để trống")
                    .Length(2, 255).WithMessage("Tên phiên bản có độ dài từ 2 đến 255 ký tự");

                RuleFor(x => x.InstallationGuide)
                    .NotEmpty().WithMessage("Các bước cài đặt không được để trống");

                RuleFor(x => x.Note)
                    .Length(0, 500).WithMessage("Ghi chú có độ dài tối đa đến 500 ký tự");

                RuleFor(x => x.PatchId)
                    .GreaterThan(0).WithMessage("Vui lòng chọn gói");

                RuleFor(x => x.StaffId)
                    .GreaterThan(0).WithMessage("Vui lòng chọn nhân viên");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<PatchVersionModel>.CreateWithOptions((PatchVersionModel)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
        }
    }
}
