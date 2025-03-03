using AutoMapper;
using DaveVentura.WebApiExtendedTemplate.Contracts.Responses;
//#if(useMongo)
using DaveVentura.WebApiExtendedTemplate.Domain.Documents;
//#endif
//#if(UseSql)
using DaveVentura.WebApiExtendedTemplate.Domain.Models;
//#endif
namespace DaveVentura.WebApiExtendedTemplate.Mapping {
    public class DomainToResponseProfile : Profile {
        public DomainToResponseProfile() {
            //#if(UseSql)
            CreateMap<Person, PersonResponse>()
                .ForMember(dest => dest.Age, opt =>
                    opt.MapFrom(src => CalculateAge(src.Birthdate))
                );
            //#endif
            //#if(useMongo)
            CreateMap<Post, PostResponse>();
            //#endif
        }
        //#if(UseSql)
        private static int CalculateAge(DateOnly birthDate) {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            int age = currentDate.Year - birthDate.Year;

            if (currentDate < birthDate.AddYears(age)) {
                age--;
            }

            return age;
        }
        //#endif
    }
}
