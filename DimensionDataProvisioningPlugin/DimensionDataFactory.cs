using System;
using System.Collections.Generic;
using System.Linq;
using System.AddIn;
using System.Text;
using System.Threading.Tasks;
using Citrix.MachineCreationAPI;
using Citrix.ManagedMachineAPI;

namespace DimensionDataProvisioningPlugin
{
    [AddIn("DimensionDataProvisioningFactory", Publisher = "Dimension Data, Ltd.", Version = "1.0.0.0")]
    public class DimensionDataProvisioningFactory : IMachineCreationFactory
    {
        public ICitrixMachineCreation CreateService(ILogProvider logger)
        {
            return new DimensionDataService(logger);
        }

        public string LocalizedName(string localeName)
        {
            return "Dimension Data Provisioning Plugin";
        }

        public string LocalizedDescription(string localeName)
        {
            // Example readable description, currently ignoring the locale.
            return "Provisioning for Dimension Data Cloud";
        }

        public string ExampleConnectionAddress(string localeName)
        {
            // Ignoring locale here. This should be a readable "hint" to the user as to
            // how the connection address should be formatted.
            return "https://api-na.dimensiondata.com/";
        }

        public bool ValidateConnectionAddressFormat(string connectionAddress)
        {
            // No validation performed in this template. Add code here to check the syntax of a candidate
            // REST API endpoint address.
            return true;
        }

        public string FactoryFor
        {
            // Just boilerplate here...
            get { return this.GetType().Name; }
        }

        public string Label
        {
            // Arbitrary "unique" identifier for this plugin.
            get { return "DimensionData"; }
        }
    }
}
