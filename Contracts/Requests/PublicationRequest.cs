using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApiExtendedTemplate.Contracts.Requests {
    public class PublicationRequest {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required int YearPublished { get; set; }
    }



}
