using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiExtendedTemplate.Services;

namespace WebApiExtendedTemplate.Controllers {
    public class CommonControllerBase : ControllerBase {
        protected IMapper Mapper;
        protected UriService UriService;
        public CommonControllerBase(UriService uriService, IMapper mapper) {
            Mapper = mapper;
            UriService = uriService;
        }
    }
}
