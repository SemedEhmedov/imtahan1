using FluentValidation;
using WebApplication2.Models.Common;

namespace WebApplication2.Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Descriptrion { get; set; }
        public string ImgUrl { get; set; }
        public int Rating { get; set; }
        public int WebSiteId { get; set; }
        public WebSite WebSite { get; set; }
    }
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {

            RuleFor(x => x.FullName)
                .MinimumLength(3)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Descriptrion)
                .MinimumLength(3)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ImgUrl)
                .NotEmpty()
                .NotNull();
        }
    }
}
