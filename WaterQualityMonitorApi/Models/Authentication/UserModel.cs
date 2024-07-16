namespace WaterQualityMonitorApi.Models.Authentication;

public class UserModel {
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string [] Permissions { get; set; } = Array.Empty<string>();
    public string [] Groups { get; set; } = Array.Empty<string>();
    public bool ActiveSubscription { get; set; } = false;
}