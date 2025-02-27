using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace doanNet.Models
{
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.SqlServer;

    public class EFConfiguration : DbConfiguration
    {
        public EFConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("mssqllocaldb"));
        }
    }

}