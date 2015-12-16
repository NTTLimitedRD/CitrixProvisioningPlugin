using System;
using System.Collections.Generic;
using Citrix.MachineCreationAPI;
using DD.CBU.Compute.Api.Contracts.Network20;

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
            var client = connectionSettings.GetComputeClient();
            List<MachineCreationResult> results = new List<MachineCreationResult>();
            foreach(var request in machineCreationRequests)
            {
                /// Something kinda like this...
                DeployServerType details = new DeployServerType
                {
                    administratorPassword = "thepassword",
                    imageId = "the image id",
                    cpu = new DeployServerTypeCpu() { coresPerSocket = 1, coresPerSocketSpecified = true, count = 2, countSpecified = true, speed = "STANDARD" }
                };
                var response = client.ServerManagement.Server.DeployServer(details).Result;
                // This isn't correct. but an indication of how it's done.
                results.Add(new MachineCreationResult(response.info[0].value, response.info[0].value, null));
            }
            
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
