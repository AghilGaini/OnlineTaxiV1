using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreService
{

    public static class PermisionManager
    {
        public static class Permisions
        {
            #region Security
            public const string Security_Users_HttpGet = "DD596AB1-A8D9-4315-92C0-BC60BF13CDAE";
            public const string Security_Roles_HttpGet = "E7654C83-F195-4EDA-9F6E-66AF4092CB63";
            public const string Security_Permisions_HttpGet = "8A9E7166-EA00-4223-BDCB-02BFBF94F850";
            public const string Security_Permisions_HttpPost = "2A5694AF-7AC8-4960-A706-43E37CE362A9";
            public const string Security_UserRoles_HttpGet = "3DD9555F-40BD-4E4E-8257-5CECD5FEFFEF";
            public const string Security_UserRoles_HttpPost = "A3927161-AB66-48F3-A0F9-800B52046F03";
            #endregion
        }

        public static List<KeyValuePair<string, string>> GetPermisions()
        {
            var type = typeof(Permisions);
            var fields = type.GetFields();
            var permisions = new List<KeyValuePair<string, string>>();

            foreach (var item in fields)
            {
                var value = item.GetValue(type);
                var name = item.Name.Replace("_", " ");
                permisions.Add(new KeyValuePair<string, string>(value.ToString(), name));
            }

            return permisions;

        }

        public static async Task SetPermisions(IUnitOfWork context)
        {
            var databasePermisions = await context._permision.GetAllAsync();
            var newPermisions = new List<PermisionDomain>();
            var ProjectPermisions = GetPermisions();

            foreach (var item in ProjectPermisions)
            {
                if (!databasePermisions.Any(r => r.Value == item.Value))
                {
                    newPermisions.Add(new PermisionDomain()
                    {
                        Title = item.Key,
                        Value = item.Value
                    });
                }
            }

            if (await context._permision.AddRange(newPermisions))
            {
                context.Complete();
            }
        }
    }


}
