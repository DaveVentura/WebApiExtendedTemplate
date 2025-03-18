namespace WebApiExtendedTemplate.Contracts.Responses {
    public class PersonResponse {
        public required long Id { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required int Age { get; set; }
    }
}
