using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApiExtendedTemplate.Common;
using WebApiExtendedTemplate.Contracts.Responses;

namespace WebApiExtendedTemplate.Controllers {
    [ApiController()]
    [Route(ApiRoutes.Info.ROUTE)]
    public class InfoController : ControllerBase {
        [HttpGet]
        public ActionResult<InfoResponse> GetInfo() {
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";
            var response = new InfoResponse {
                Version = version
            };
            return base.Ok(response);
        }
    }
}
