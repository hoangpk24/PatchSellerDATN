using FluentValidation;
using PatchSeller.DAL.Models;
using PatchSeller.Web.Models;

namespace PatchSeller.Web.Schema
{
    public class DiscountSchema
    {
        public class CreateUpdate : AbstractValidator<DiscountModel>
        {
            public CreateUpdate()
            {
                RuleFor(x => x.Code)
                    .NotEmpty().WithMessage("Mã không được để trống")
                    .Length(2, 50).WithMessage("Mã có độ dài từ 2 đến 50 ký tự");

                RuleFor(x => x.Value)
                    .NotEmpty().WithMessage("Giá trị không được để trống");

                RuleFor(x => x.Value)
                    .InclusiveBetween(1, 100)
                    .WithMessage("Phần trăm giảm giá phải nằm trong khoảng 1% đến 100%")
                    .When(x => x.DiscountType == Constant.Constant.DiscountTypePercent);

                RuleFor(x => x.Value)
                    .InclusiveBetween(1000, 9999999999999999)
                    .WithMessage("Số tiền giảm giá tối thiểu 1.000đ")
                    .When(x => x.DiscountType == Constant.Constant.DiscountTypeFixed);

                RuleFor(x => x.DiscountType)
                    .NotEmpty().WithMessage("Loại giảm giá không được để trống")
                    .Must(dt => dt == Constant.Constant.DiscountTypePercent || dt == Constant.Constant.DiscountTypeFixed)
                    .WithMessage("Loại giảm giá không hợp lệ");

                RuleFor(x => x.StartDate)
                    .NotEmpty().WithMessage("Ngày bắt đầu không được để trống")
                    .Must(date => date >= DateTime.Now.AddMinutes(-5))
                    .WithMessage("Ngày bắt đầu không được ở quá khứ");

                RuleFor(x => x.EndDate)
                    .NotEmpty().WithMessage("Ngày kết thúc không được để trống")
                    .GreaterThan(x => x.StartDate).WithMessage("Ngày kết thúc phải sau ngày bắt đầu");

                RuleFor(x => x.UsageLimit)
                    .NotEmpty().WithMessage("Số lượng phát hành không được để trống")
                    .InclusiveBetween(1, 999999).WithMessage("Số lượng phát hành ít nhất là 1");

                RuleFor(x => x.LimitPerUser)
                    .NotEmpty().WithMessage("Giới hạn sử dụng cho mỗi người dùng không được để trống")
                    .InclusiveBetween(1, 999999).WithMessage("Giới hạn sử dụng cho mỗi người dùng ít nhất là 1");

                RuleFor(x => x.MinOrderValue)
                    .InclusiveBetween(1000, 9999999999999999).WithMessage("Giá trị tối thiểu áp dụng tối thiểu 1.000đ");

                RuleFor(x => x.MaxDiscount)
                    .InclusiveBetween(1000, 9999999999999999).WithMessage("Giá trị giảm tối đa tối thiểu 1.000đ")
                    .When(x => x.DiscountType == Constant.Constant.DiscountTypePercent);
            }
            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync((DiscountModel)model);

                if (result.IsValid)
                    return Array.Empty<string>();

                return result.Errors
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage);
            };
        }
    }
}
