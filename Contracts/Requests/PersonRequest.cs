namespace WebApiExtendedTemplate.Contracts.Requests {
    public class PersonRequest {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        [MinAge(18)]
        public DateOnly Birthdate { get; set; }
    }
}
