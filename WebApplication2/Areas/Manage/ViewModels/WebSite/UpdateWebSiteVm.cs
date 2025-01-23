using FluentValidation;

namespace WebApplication2.Areas.Manage.ViewModels.WebSite
{
    public class UpdateWebSiteVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateWebSiteVmValidation : AbstractValidator<UpdateWebSiteVm>
    {
        public UpdateWebSiteVmValidation()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .NotEmpty()
                .NotNull();
        }
    }
}
