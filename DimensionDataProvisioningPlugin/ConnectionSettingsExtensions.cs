using Citrix.MachineCreationAPI;
using DD.CBU.Compute.Api.Client;
using System.Net;

namespace DimensionDataProvisioningPlugin
{
    public static class ConnectionSettingsExtensions
    {
        public static ComputeApiClient GetComputeClient(this ConnectionSettings connectionSettings)
        {
            // The Provisioning SDK currently only supports a single "default" connection
            // credential, consisting of a username and password pair (although these can also
            // be treated as API/secret keys).
            UserCredential defaultCredential = connectionSettings.Credentials["Default"];

            // We support multiple service addresses, but REST APIs endpoints are typically just a
            // single address, so let's assume that pattern here.
            string endpoint = connectionSettings.ServiceAddresses[0];

            // Create a client.
            return ComputeApiClient.GetComputeApiClient(new System.Uri(endpoint), new NetworkCredential(defaultCredential.UserName, defaultCredential.Password));
        }
    }
}
