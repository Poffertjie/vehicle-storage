using Database.DbContext.DbModels;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Database.DbContext.IdentityModels;

public class ApplicationUser 
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? ResetPasswordToken { get; set; }
    public string? PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public bool AccountLocked { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? CompanyId { get; set; }
    public bool SystemAdmin { get; set; }

    public ApplicationUser()
    {

    }

    public ApplicationUser(User user) : base()
    {
        if (user != null)
        {
            List<PropertyInfo> userProps = typeof(User).GetProperties().ToList();
            List<PropertyInfo> AppUserProps = typeof(ApplicationUser).GetProperties().ToList();


            foreach (PropertyInfo UserProp in userProps)
            {
                if (UserProp.CanRead)
                {
                    PropertyInfo AppUserprop = AppUserProps.FirstOrDefault(x => x.CanWrite && x.PropertyType == UserProp.PropertyType && x.Name.Equals(UserProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (AppUserprop != null)
                    {
                        AppUserprop.SetValue(this, UserProp.GetValue(user));
                    }
                }
            }
        }
    }

    public User ToUser()
    {
        List<PropertyInfo> userProps = typeof(User).GetProperties().ToList();
        List<PropertyInfo> AppUserProps = typeof(ApplicationUser).GetProperties().ToList();

        User item = Activator.CreateInstance<User>();

        foreach (PropertyInfo appProp in AppUserProps)
        {
            if (appProp.CanRead)
            {
                PropertyInfo userprop = userProps.FirstOrDefault(x => x.CanWrite && x.PropertyType == appProp.PropertyType && x.Name.Equals(appProp.Name, StringComparison.InvariantCultureIgnoreCase));
                if (userprop != null)
                {
                    userprop.SetValue(item, appProp.GetValue(this));
                }
            }
        }

        return item;
    }
}