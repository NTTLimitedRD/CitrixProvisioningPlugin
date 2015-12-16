using System;
using System.Collections.Generic;
using Citrix.MachineCreationAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : ISynchronousLifecycle
    {
        public bool SupportsBatchedResetRequests
        {
            // In current versions of the SDK, all calls are unbatched (a separate call is made for each operation),
            // so we may as well return false here. A return value of true is valid, but wouldn't actually change the
            // behaviour in terms of the pattern of calls to the plugin.
            get { return false; }
        }

        public bool SupportsBatchedUpdateRequests
        {
            // In current versions of the SDK, all calls are unbatched (a separate call is made for each operation),
            // so we may as well return false here. A return value of true is valid, but wouldn't actually change the
            // behaviour in terms of the pattern of calls to the plugin.
            get { return false; }
        }

        public IList<MachineLifecycleResult> ResetMachines(ConnectionSettings connectionSettings, HostingSettings hostingSettings, IList<MachineLifecycleRequest> machineRequests,
            DiskImageSpecification masterImage)
        {
            throw new NotImplementedException();
        }

        public IList<MachineLifecycleResult> UpdateMachines(ConnectionSettings connectionSettings, HostingSettings hostingSettings, IList<MachineLifecycleRequest> machineRequests,
            DiskImageSpecification newMasterImage)
        {
            throw new NotImplementedException();
        }
    }
}
