using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExtendedTemplate.Contracts.Requests {
    public class PostRequest {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}
