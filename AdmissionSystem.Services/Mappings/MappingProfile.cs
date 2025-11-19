using AutoMapper;
using AdmissionSystem.Core.DTOs.Auth;
using AdmissionSystem.Core.Models;

namespace AdmissionSystem.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<RegisterRequest, Applicant>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // We'll set this manually
                .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => "Applicant"));
        }
    }
}