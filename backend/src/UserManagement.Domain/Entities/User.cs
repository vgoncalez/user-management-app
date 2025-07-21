namespace UserManagement.Domain.Entities;

public class User
{
    public User() { }

    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    public Guid Id { get; protected set; }

    public string Name { get; protected set; } = null!;

    public string Email { get; protected set; } = null!;

    public string PasswordHash { get; protected set; } = null!;

    public void SetPasswordHash(string value) => PasswordHash = value;
}
