using Database.DbContext.DbModels;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Database.DbContext.IdentityModels;

public class ApplicationRole
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public ApplicationRole() : base()
    {

    }

    public ApplicationRole(Role role) : base()
    {
        if (role != null)
        {
            List<PropertyInfo> userProps = typeof(Role).GetProperties().ToList();
            List<PropertyInfo> AppUserProps = typeof(ApplicationRole).GetProperties().ToList();


            foreach (PropertyInfo UserProp in userProps)
            {
                if (UserProp.CanRead)
                {
                    PropertyInfo AppUserprop = AppUserProps.FirstOrDefault(x => x.CanWrite && x.PropertyType == UserProp.PropertyType && x.Name.Equals(UserProp.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (AppUserprop != null)
                    {
                        AppUserprop.SetValue(this, UserProp.GetValue(role));
                    }
                }
            }
        }
    }

    public Role ToRole()
    {
        List<PropertyInfo> userProps = typeof(Role).GetProperties().ToList();
        List<PropertyInfo> AppUserProps = typeof(ApplicationRole).GetProperties().ToList();

        Role item = Activator.CreateInstance<Role>();

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