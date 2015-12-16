using System;
using System.IO;
using Citrix.MachineCreationAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : ISynchronousDiskAccess
    {
        public Stream ReadDisk(ConnectionSettings connectionSettings, DiskAccessRequest diskAccessRequest)
        {
            throw new NotImplementedException();
        }

        public void WriteDisk(ConnectionSettings connectionSettings, DiskAccessRequest diskAccessRequest, Stream contents)
        {
            throw new NotImplementedException();
        }

        public void DeleteDisk(ConnectionSettings connectionSettings, DiskImageSpecification diskImageToDelete)
        {
            throw new NotImplementedException();
        }

        public DiskImageSpecification DetachDisk(ConnectionSettings connectionSettings, DiskAccessRequest diskToDetach,
            bool deletingMachine)
        {
            throw new NotImplementedException();
        }
    }
}
