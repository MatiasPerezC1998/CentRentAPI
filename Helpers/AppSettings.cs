namespace CentRent.Helpers;

public class AppSettings {
    public string? Secret { get; set; }
    public double Time { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}