using WebApiExtendedTemplate.Domain.Abstracts;

namespace WebApiExtendedTemplate.Domain.Entities {
    public class Publication : TableEntityBase {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required int YearPublished { get; set; }
    }
}
