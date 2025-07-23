using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MCMsoPluginTester
{
    public class DbConnectionParams
    {
        public enum DataSourceEnum
        {
            Database,
            HTML
        };

        public DbConnectionParams()
        {
            DataSource = DataSourceEnum.Database;
            ConnectionString = "http://tc22153-VirtualBox/nominatim?accept-language=es&limit=25";
            HostName = "localhost";
            Port = 5432;
            Database = "nominatim";
            User = "postgres";
            Password = "admin";
            RoutingDatabase = "routing";
        }

        [Category("Connectivity")]
        [DisplayName("Data source")]
        [Description("The nominatim data source")]
        public DataSourceEnum DataSource {get; set;}

        [Category("HTML")]
        [DisplayName("HTML Connection string")]
        [Description("The connection string used to connect to nominatim web server")]
        public string ConnectionString { get; set; }

        [Category("Database")]
        [DisplayName("Host name")]
        [Description("The name/address of the computer hosting the database")]
        public string HostName { get; set; }

        [Category("Database")]
        [DisplayName("TCP Port")]
        [Description("The port used by the network to maintain the database connectivity in TCP protocol")]
        public uint Port { get; set; }

        [Category("Nominatim")]
        [DisplayName("Database")]
        [Description("The database that is used to store geocoding data")]
        public string Database { get; set; }

        [Category("Routing")]
        [DisplayName("Database")]
        [Description("The database that is used to store routing data")]
        public string RoutingDatabase { get; set; }

        [Category("Database")]
        [DisplayName("User name")]
        [Description("The user who logs in to the database")]
        public string User { get; set; }

        [Category("Database")]
        [DisplayName("Password")]
        [Description("Password used to login the user into the database")]
        public string Password { get; set; }

        [Category("Filter")]
        [DisplayName("Exclude Villages")]
        [Description("Exclude villages from settlements")]
        public bool ExcludeVillages { get; set; }
    }
}
