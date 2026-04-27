using TestFramework.Models;

namespace TestFramework.Builders;

public class UserBuilder : BuilderBase<UserBuilder, User>
{
    public UserBuilder()
    {
        Entity = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = $"test-{Guid.NewGuid():N}@example.com",
            Name = "Test User",
            Role = UserRole.Standard,
            IsActive = true
        };
    }

    public UserBuilder WithEmail(string email) { Entity.Email = email; return this; }
    public UserBuilder WithName(string name) { Entity.Name = name; return this; }
    public UserBuilder WithRole(UserRole role) { Entity.Role = role; return this; }
    public UserBuilder WithId(string id) { Entity.Id = id; return this; }
    public UserBuilder AsInactive() { Entity.IsActive = false; return this; }
    public UserBuilder AsAdmin() { Entity.Role = UserRole.Admin; return this; }
}
