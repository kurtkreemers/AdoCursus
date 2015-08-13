using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using AdoGemeenschap;

namespace AdoGemeenschap
{
    public class BierenDbManager
    {
        private static ConnectionStringSettings conBierenSetting = ConfigurationManager.ConnectionStrings["Bieren"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conBierenSetting.ProviderName);

        public DbConnection GetConnection ()
        {
            var conBieren = factory.CreateConnection();
            conBieren.ConnectionString = conBierenSetting.ConnectionString;
            return conBieren;
        }
    }
}
