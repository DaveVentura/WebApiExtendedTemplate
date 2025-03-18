using AutoMapper;
using WebApiExtendedTemplate.Contracts.Requests;
//#if(useMongo)
using WebApiExtendedTemplate.Domain.Documents;
//#endif
//#if(useAzureTable)
using WebApiExtendedTemplate.Domain.Entities;
//#endif
//#if(UseSql)
using WebApiExtendedTemplate.Domain.Models;
//#endif

namespace WebApiExtendedTemplate.Mapping {
    public class RequestToDomainProfile : Profile {
        public RequestToDomainProfile() {
            //#if(UseSql)
            CreateMap<PersonRequest, Person>();
            //#endif
            //#if(useMongo)
            CreateMap<PostRequest, Post>();
            //#endif
            //#if(useAzureTable)
            CreateMap<PublicationRequest, Publication>();
            //#endif
        }
    }
}
