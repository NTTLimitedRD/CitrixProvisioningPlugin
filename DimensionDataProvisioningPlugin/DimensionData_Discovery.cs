using System;
using System.Collections.Generic;
using Citrix.MachineCreationAPI;
using Citrix.ManagedMachineAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : IMachineStateDiscovery
    {
        public IList<IManagedMachine> GetMachineStates(ConnectionSettings connectionSettings, IList<string> filter)
        {
            throw new NotImplementedException();
        }
    }
}
