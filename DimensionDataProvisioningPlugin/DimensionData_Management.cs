using System;
using System.Collections.Generic;
using Citrix.MachineCreationAPI;
using Citrix.ManagedMachineAPI;
using DD.CBU.Compute.Api.Client;
using DD.CBU.Compute.Api.Contracts.Network20;

namespace DimensionDataProvisioningPlugin
{
    /// <summary>
    /// Implementation summary:
    ///  * Complete
    /// </summary>
    public partial class DimensionDataService : ISynchronousManagement
    {
        /// <summary>
        /// Get the Citrix machine model for the given server ID
        /// </summary>
        /// <param name="connectionSettings"></param>
        /// <param name="machineId"></param>
        /// <returns></returns>
        public IManagedMachine GetMachineDetails(ConnectionSettings connectionSettings, string machineId)
        {
            ComputeApiClient client = connectionSettings.GetComputeClient();
            ServerType server = client.ServerManagement.Server.GetServer(Guid.Parse(machineId)).Result;
            return server.ToManagedMachine();
        }

        public bool SupportsBatchedManagementRequests
        { 
            // In current versions of the SDK, all calls are unbatched (a separate call is made for each operation),
            // so we may as well return false here. A return value of true is valid, but wouldn't actually change the
            // behaviour in terms of the pattern of calls to the plugin.
            get { return false; }
        
        }
        public IList<SupportedPowerAction> SupportedPowerActions
        {
            get
            {
                // Let's assume that the plugin can support a basic set of power operations. Feel free to add
                // more from the SupportedPowerAction enum where relevant.
                List<SupportedPowerAction> result = new List<SupportedPowerAction>();
                result.Add(SupportedPowerAction.PowerOn);
                result.Add(SupportedPowerAction.PowerOff);
                result.Add(SupportedPowerAction.Shutdown);
                result.Add(SupportedPowerAction.Reset);
                return result;
            }
        }

        public void PowerOnMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            var client = connectionSettings.GetComputeClient();
            foreach( string id in machineIds)
            {
                client.ServerManagement.Server.StartServer(Guid.Parse(id));
            }
        }

        public void PowerOffMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            var client = connectionSettings.GetComputeClient();
            foreach (string id in machineIds)
            {
                client.ServerManagement.Server.PowerOffServer(Guid.Parse(id));
            }
        }

        public void ShutdownMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            var client = connectionSettings.GetComputeClient();
            foreach (string id in machineIds)
            {
                client.ServerManagement.Server.ShutdownServer(Guid.Parse(id));
            }
        }

        public void RestartMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            var client = connectionSettings.GetComputeClient();
            foreach (string id in machineIds)
            {
                client.ServerManagement.Server.RebootServer(Guid.Parse(id));
            }
        }

        public void ResetMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            var client = connectionSettings.GetComputeClient();
            foreach (string id in machineIds)
            {
                client.ServerManagement.Server.ResetServer(Guid.Parse(id));
            }
        }

        public void SuspendMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            throw new NotImplementedException();
        }
    }
}
