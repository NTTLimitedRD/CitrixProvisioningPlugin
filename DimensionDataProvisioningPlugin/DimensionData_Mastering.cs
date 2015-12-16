using System;
using Citrix.MachineCreationAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : ISynchronousMastering
    {
        public MasteringDurationHints GetMasteringOperationRelativeDurationHints(ConnectionSettings connectionSettings)
        {
            // Let's assume that we spend all of our time in consolidation.
            return new MasteringDurationHints(100, 0);
        }

        public DiskImageSpecification ConsolidateMasterImage(ConnectionSettings connectionSettings, HostingSettings hostingSettings,
            Action<int> progressCallback, InventoryItemReference sourceItem, DiskImageSpecification requestedDiskProperties)
        {
            // Dummy implementation
            logger.TraceEntryExit("ExampleService.GetFinalizeMasterImage (no-op)");
            progressCallback(100);
            return requestedDiskProperties;
        }

        public DiskImageSpecification FinalizeMasterImage(ConnectionSettings connectionSettings, HostingSettings hostingSettings,
            DiskAccessRequest masterDiskAccess, Action<int> progressCallback, DiskImageSpecification requestedDiskProperties)
        {
            // Dummy implementation
            logger.TraceEntryExit("ExampleService.GetFinalizeMasterImage (no-op)");
            progressCallback(100);
            return requestedDiskProperties;
        }
    }
}
