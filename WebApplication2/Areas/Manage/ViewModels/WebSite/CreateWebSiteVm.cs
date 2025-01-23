using FluentValidation;

namespace WebApplication2.Areas.Manage.ViewModels.WebSite
{
    public class CreateWebSiteVm
    {
        public string Name { get; set; }
    }
    public class CreateWebSiteVmValidation : AbstractValidator<CreateWebSiteVm>
    {
        public CreateWebSiteVmValidation()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .NotEmpty()
                .NotNull();
        }
    }
}
