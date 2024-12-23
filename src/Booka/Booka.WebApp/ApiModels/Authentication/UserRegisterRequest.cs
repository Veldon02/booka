namespace Booka.WebApp.ApiModels.Authentication;

public class UserRegisterRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string UserEmail { get; set; }

    public string Password { get; set; }
}