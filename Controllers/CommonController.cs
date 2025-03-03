using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace DaveVentura.WebApiExtendedTemplate.Controllers {
    public class CommonController : ControllerBase {
        protected IMapper Mapper;
        public CommonController(IMapper mapper) {
            Mapper = mapper;
        }
    }
}
