namespace WebApiExtendedTemplate.Contracts.Responses {
    public class InfoResponse {
        public bool IsHealthy { get; } = true;
        public string? Version { get; set; }
    }
}
