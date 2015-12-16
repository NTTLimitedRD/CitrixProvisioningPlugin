﻿using Citrix.MachineCreationAPI;
using Citrix.ManagedMachineAPI;
using System.Net;

namespace DimensionDataProvisioningPlugin
{
    /// <summary>
    /// This single class implements the entire plugin, but is split into partial classes to improve the
    /// clarity and functional separation between the many distinct interfaces that the class must support.
    /// </summary>
    public partial class DimensionDataService : ICitrixMachineCreation
    {
        /// <summary>
        /// Use this object to issue diagnostic traces from any plugin operation.
        /// </summary>
        private readonly ILogProvider logger;

        public DimensionDataService(ILogProvider logger)
        {
            this.logger = logger;
            GatherInventory();
        }

        public void ValidateConnectionSettings(ConnectionSettings connectionSettings)
        {
            // The Provisioning SDK currently only supports a single "default" connection
            // credential, consisting of a username and password pair (although these can also
            // be treated as API/secret keys).
            UserCredential defaultCredential = connectionSettings.Credentials["Default"];

            // We support multiple service addresses, but REST APIs endpoints are typically just a
            // single address, so let's assume that pattern here.
            string endpoint = connectionSettings.ServiceAddresses[0];

            // Currently just issue a trace log (avoiding the password), but this is where you would
            // add code to verify the correct address and credentials.
            logger.TraceMsg(
                "Connecting to Example API endpoint {0} as user {1}",
                endpoint,
                defaultCredential.UserName);

            // Create a client.
            DD.CBU.Compute.Api.Client.ComputeApiClient.GetComputeApiClient(new System.Uri(endpoint), new NetworkCredential(defaultCredential.UserName, defaultCredential.Password));
        }

        public StorageModel PluginStorageModel
        {
            // Let's assume that storage repositories are modelled explicitly in the inventory
            // tree, and can be selected as storage targets for provisioning. (See the API documentation
            // for more details about the choice of storage model).
            get { return StorageModel.ExplicitManagedByMachineCreationServices; }
        }
    }
}