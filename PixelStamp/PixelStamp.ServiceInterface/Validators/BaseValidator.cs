using FluentValidation;
using PixelStamp.Core.Dtos;

namespace PixelStamp.ServiceInterface.Validators
{
    public class BaseValidator : AbstractValidator<BaseDto<int>>
    {
        public BaseValidator()
        {

        }
    }
}
