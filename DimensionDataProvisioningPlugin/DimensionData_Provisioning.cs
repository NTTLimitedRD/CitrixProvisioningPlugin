using System;
using System.Collections.Generic;
using System.Linq;
using Citrix.MachineCreationAPI;
using DD.CBU.Compute.Api.Contracts.Network20;

namespace DimensionDataProvisioningPlugin
{
    /// <summary>
    /// Implementation Summary
    ///  * todo - find the provisioned server ID so it can be sent to the inventory
    /// </summary>
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
            var client = connectionSettings.GetComputeClient();
            List<MachineCreationResult> results = new List<MachineCreationResult>();
            foreach(var request in machineCreationRequests)
            {
                /// Something kinda like this...
                DeployServerType details = new DeployServerType
                {
                    // Provision the VM into the default network domain and VLAN
                    networkInfo = new DeployServerTypeNetworkInfo
                    {
                        networkDomainId = GetNetworkDomainId(connectionSettings).ToString(),
                        primaryNic = new VlanIdOrPrivateIpType
                        {
                            vlanId  = GetVlanId(connectionSettings).ToString()
                        }
                    },
                    administratorPassword = "thepassword",
                    imageId = GetDefaultImageId(connectionSettings),
                    cpu = new DeployServerTypeCpu {
                        coresPerSocket = _defaultCoresPerSocket,
                        coresPerSocketSpecified = true,
                        count = _defaultCpuCount,
                        countSpecified = true,
                        speed = "STANDARD" // TODO : Make this configurable
                    },
                    description = "Provisioned by Citrix",
                    name = request.MachineName
                };
                var response = client.ServerManagement.Server.DeployServer(details).Result;
                // This isn't correct. but an indication of how it's done.
                results.Add(new MachineCreationResult(response.info[0].value, response.info[0].value, null));
            }
            return results;
        }

        private string GetDefaultImageId(ConnectionSettings connectionSettings)
        {
            var client = connectionSettings.GetComputeClient();
            var images = client.ServerManagementLegacy.ServerImage.GetImages(null, _defaultImageName, null, null, null).Result;
            if (images == null || !images.Any())
                throw new Exception("Cannot find default image");
            return images.First().id;
        }

        public void DeleteMachines(ConnectionSettings connectionSettings, IList<string> machineIds)
        {
            var client = connectionSettings.GetComputeClient();
            foreach(string id in machineIds)
            {
                client.ServerManagement.Server.DeleteServer(Guid.Parse(id));
            }
        }
    }
}
