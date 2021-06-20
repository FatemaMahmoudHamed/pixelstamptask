using FluentValidation;
using PixelStamp.Common.Resources.Common;
using PixelStamp.Core.Dtos;

namespace PixelStamp.ServiceInterface.Validators.Others
{
    class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            //RuleFor(x => x.UserName)
            //    .NotEmpty()
            //    .MaximumLength(50);
        }
    }
}
