using ArtWarehouse.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ArtWarehouse.Services
{
    public class Users_DbService
    {
        SqlConnection _db;
        public IConfiguration Configuration { get; }

        public Users_DbService(IConfiguration configuration)
        {
            Configuration = configuration;
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public User_Model EnterUser(UserEnter_Model userEnter)
        {
            _db.Open();

            string query = @"
SELECT
    u.Name,
    u.Nickname
FROM 
    Users u
WHERE
    u.Hashpassword = @arghash
AND u.Nickname = @argnick
";

            var obj = new
            {
                arghash = userEnter.Password,
                argnick = userEnter.Nickname
            };

            var result =_db.Query<User_Model>(query, obj).ToList();

            if (result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return new User_Model
                {
                    Name = "Error",
                    Nickname = "Error"
                };
            }
        }
    }
}
