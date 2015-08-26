using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoGemeenschap
{
    public class StripManager
    {
        private static ConnectionStringSettings conStripSetting = ConfigurationManager.ConnectionStrings["strips"];
        private static DbProviderFactory factory = DbProviderFactories.GetFactory(conStripSetting.ProviderName);
        public DbConnection GetConnection()
        {
            var conStrip = factory.CreateConnection();
            conStrip.ConnectionString = conStripSetting.ConnectionString;
            return conStrip;
        }
    }
}
