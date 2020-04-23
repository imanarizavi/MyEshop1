using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyEshop.Classes
{
    public class MyRoleProVider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
#pragma warning disable CS0012 // The type 'DbContext' is defined in an assembly that is not referenced. You must add a reference to assembly 'EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
#pragma warning disable CS1674 // 'MyEshop_DBEntities': type used in a using statement must be implicitly convertible to 'System.IDisposable'.
            using (MyEshop_DBEntities db= new MyEshop_DBEntities())
#pragma warning restore CS1674 // 'MyEshop_DBEntities': type used in a using statement must be implicitly convertible to 'System.IDisposable'.
#pragma warning restore CS0012 // The type 'DbContext' is defined in an assembly that is not referenced. You must add a reference to assembly 'EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
            {
#pragma warning disable CS0012 // The type 'DbSet<>' is defined in an assembly that is not referenced. You must add a reference to assembly 'EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
                return db.Users.Where(u=>u.UserName==username).Select(u=>u.Role.RoleName).ToArray();
#pragma warning restore CS0012 // The type 'DbSet<>' is defined in an assembly that is not referenced. You must add a reference to assembly 'EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}