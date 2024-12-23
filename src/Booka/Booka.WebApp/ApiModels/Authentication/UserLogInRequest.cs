namespace Booka.WebApp.ApiModels.Authentication;

public class UserLogInRequest
{
    public string UserEmail { get; set; }

    public string Password { get; set; }
}