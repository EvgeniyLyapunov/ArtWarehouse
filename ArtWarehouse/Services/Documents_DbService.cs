using ArtWarehouse.Models;
using ArtWarehouse.Models.ModelsView;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ArtWarehouse.Services
{
    public class Documents_DbService
    {
        SqlConnection _db;
        public IConfiguration Configuration { get; }

        public Documents_DbService(IConfiguration configuration)
        {
            Configuration = configuration;
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public DocumentsList_MV DocumentsList_Get()
        {
            DocumentsList_MV documentsList_MV = new DocumentsList_MV();

            _db.Open();

            string query = @"SELECT * FROM Orders";

            documentsList_MV.Documents = _db.Query<Document>(query, null).ToList();

            return documentsList_MV;
        }
    }
}
