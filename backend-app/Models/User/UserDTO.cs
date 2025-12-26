namespace backend_app.Models.User;

public class UserDTO
{
    public int id { get; set; }
    public string fullName { get; set; }
    public string? nickname { get; set; }
    public string email { get; set; }

}
