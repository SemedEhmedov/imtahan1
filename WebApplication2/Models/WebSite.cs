using FluentValidation;
using WebApplication2.Models.Common;

namespace WebApplication2.Models
{
    public class WebSite : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
    public class WebSiteValidation : AbstractValidator<WebSite>
    {
        public WebSiteValidation()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .NotEmpty()
                .NotNull();
        }
    }
}
