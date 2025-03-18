using AutoMapper;
using WebApiExtendedTemplate.Contracts.Responses;
//#if(useMongo)
using WebApiExtendedTemplate.Domain.Documents;
using WebApiExtendedTemplate.Domain.Entities;
//#endif
//#if(UseSql)
using WebApiExtendedTemplate.Domain.Models;
//#endif
namespace WebApiExtendedTemplate.Mapping {
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
            //#if(useAzureTable)
            CreateMap<Publication, PublicationResponse>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.RowKey))
                .ForMember(dest => dest.PublicationType, opt =>
                    opt.MapFrom(src => src.PartitionKey));
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
