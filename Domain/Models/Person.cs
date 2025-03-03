using DaveVentura.WebApiExtendedTemplate.Domain.Abstracts;

namespace DaveVentura.WebApiExtendedTemplate.Domain.Models {
    public class Person : ModelBase<long> {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required DateOnly Birthdate { get; set; }
    }
}
