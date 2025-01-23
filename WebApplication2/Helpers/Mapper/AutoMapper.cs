using AutoMapper;
using WebApplication2.Areas.Manage.ViewModels.User;
using WebApplication2.Areas.Manage.ViewModels.WebSite;
using WebApplication2.Models;

namespace WebApplication2.Helpers.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, CreateUserVm>().ReverseMap();
            CreateMap<User, UpdateUserVm>().ReverseMap();

            CreateMap<WebSite, CreateWebSiteVm>().ReverseMap();
            CreateMap<WebSite, UpdateWebSiteVm>().ReverseMap();


        }
    }
}
