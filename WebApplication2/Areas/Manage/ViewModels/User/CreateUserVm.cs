using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Areas.Manage.ViewModels.User
{
    public class CreateUserVm
    {
        public string FullName { get; set; }
        public string Descriptrion { get; set; }
        public int WebSiteId { get; set; }
        public int Rating { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
    public class CreateUserVmValidation : AbstractValidator<CreateUserVm>
    {
        public CreateUserVmValidation()
        {
            RuleFor(x => x.Rating)
                .NotEmpty()
                .NotNull();
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
