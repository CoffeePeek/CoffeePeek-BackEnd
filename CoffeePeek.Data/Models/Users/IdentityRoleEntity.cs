using Microsoft.AspNetCore.Identity;

namespace CoffeePeek.Data.Models.Users;

public class IdentityRoleEntity : IdentityRole<int>
{
    public IdentityRoleEntity()
    {
        
    }

    public IdentityRoleEntity(string roleName) : base(roleName)
    {
        
    }
}