using Application.Common.ViewModel;
using Application.Core.Identity;
using AutoMapper;

namespace API.DotNetCore.Mapping
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>();
        }
    }
}
