using System;
using Citrix.MachineCreationAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : ISynchronousReplication
    {
        public bool SupportsRemoteCopy { get; private set; }

        public DiskImageSpecification LocalCopyDisk(ConnectionSettings connectionSettings, HostingSettings hostingSettings,
            Action<int> progressCallback, DiskImageSpecification sourceDisk, DiskImageSpecification destinationDisk)
        {
            throw new NotImplementedException();
        }

        public DiskImageSpecification RemoteCopyDisk(ConnectionSettings sourceConnectionSettings, HostingSettings sourceHostingSettings,
            ConnectionSettings destinationConnectionSettings, HostingSettings destinationHostingSettings,
            Action<int> progressCallback, DiskImageSpecification sourceDisk, DiskImageSpecification destinationDisk)
        {
            throw new NotImplementedException();
        }
    }
}
