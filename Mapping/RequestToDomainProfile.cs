using AutoMapper;
using DaveVentura.WebApiExtendedTemplate.Contracts.Requests;
//#if(useMongo)
using DaveVentura.WebApiExtendedTemplate.Domain.Documents;
//#endif
//#if(UseSql)
using DaveVentura.WebApiExtendedTemplate.Domain.Models;
//#endif

namespace DaveVentura.WebApiExtendedTemplate.Mapping {
    public class RequestToDomainProfile : Profile {
        public RequestToDomainProfile() {
            //#if(UseSql)
            CreateMap<PersonRequest, Person>();
            //#endif
            //#if(useMongo)
            CreateMap<PostRequest, Post>();
            //#endif
        }
    }
}
