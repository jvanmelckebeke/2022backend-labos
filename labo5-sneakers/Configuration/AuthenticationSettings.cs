namespace labo5_sneakers.Configuration;

public class AuthenticationSettings
{
    public string SecretForKey { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
}