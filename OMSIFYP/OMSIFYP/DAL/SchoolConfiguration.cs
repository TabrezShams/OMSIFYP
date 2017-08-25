using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace OMSIFYP.DAL
{
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {//for extra hits
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}