using Citrix.MachineCreationAPI;
using Citrix.ManagedMachineAPI;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : IMachineHardwareProfiler
    {
        private readonly DimensionDataHardwareProfile demoHardwareProfile = new DimensionDataHardwareProfile()
        {
            NumberOfVirtualProcessors = 1,
            MemorySizeInMegabytes = 4096,
            FullProfile = "vCPU=1,RAM=4096,..."
        };

        public IHardwareProfile GatherHardwareProfile(ConnectionSettings connectionSettings, IInventoryItem sourceItem)
        {
            // Return hard-wired dummy data.
            // A real implementation should look up the inventory item, and catalog its properties (assuming that it's
            // something like a VM or a snapshot).
            return demoHardwareProfile;
        }

        public IHardwareProfile ReconstructHardwareProfile(string fullProfileString)
        {
            // Return hard-wired dummy data.
            // A real implementation should "deserialize" the profile string here.
            return demoHardwareProfile;
        }
    }

    internal class DimensionDataHardwareProfile : IHardwareProfile
    {
        public int NumberOfVirtualProcessors { get; internal set; }
        public int MemorySizeInMegabytes { get; internal set; }
        public string FullProfile { get; internal set; }
    }
}
