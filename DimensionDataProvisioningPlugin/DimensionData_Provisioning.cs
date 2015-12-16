using System;
using System.Collections.Generic;
using Citrix.MachineCreationAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : ISynchronousProvisioning
    {
        public bool SupportsBatchedCreationRequests
        { 
            // In current versions of the SDK, all calls are unbatched (a separate call is made for each operation),
            // so we may as well return false here. A return value of true is valid, but wouldn't actually change the
            // behaviour in terms of the pattern of calls to the plugin.
            get { return false; }
        }

        public bool SupportsBatchedDeletionRequests
        { 
            // In current versions of the SDK, all calls are unbatched (a separate call is made for each operation),
            // so we may as well return false here. A return value of true is valid, but wouldn't actually change the
            // behaviour in terms of the pattern of calls to the plugin.
            get { return false; }
        }

        public IList<MachineCreationResult> CreateMachines(ConnectionSettings connectionSettings, HostingSettings hostingSettings,
            ProvisioningSettings provisioningSettings, IList<MachineCreationRequest> machineCreationRequests)
        {
            // Just log to prove that we got here, but no implementation as yet.
            logger.TraceMsg("An instance creation request has been received!");
            throw new NotImplementedException();
        }

        public void DeleteMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            throw new NotImplementedException();
        }
    }
}
