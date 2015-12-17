using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Citrix.MachineCreationAPI;
using Citrix.ManagedMachineAPI;
using DD.CBU.Compute.Api.Client;
using DD.CBU.Compute.Api.Contracts.Network20;
using DD.CBU.Compute.Api.Contracts.Requests.Network20;
using DD.CBU.Compute.Api.Contracts.Requests.Server20;

namespace DimensionDataProvisioningPlugin
{
    public partial class DimensionDataService : IMachineStateDiscovery
    {
        private Guid? _networkDomainId;
        private Guid? _vlanId;

        /// <summary>
        /// Map the list of servers in the network to the 
        /// </summary>
        /// <param name="connectionSettings"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<IManagedMachine> GetMachineStates(ConnectionSettings connectionSettings, IList<string> filter)
        {
            // Step 1: Get the ID of the default Network Domain
            _networkDomainId = GetNetworkDomainId(connectionSettings);
            _vlanId = GetVlanId(connectionSettings);

            IEnumerable<IManagedMachine> results;
            ComputeApiClient client = connectionSettings.GetComputeClient();
            IEnumerable<ServerType> servers = client.ServerManagement.Server.GetServers(new ServerListOptions()
            {
                Name = filter[0],
                NetworkDomainId = _networkDomainId,
                VlanId = _vlanId
            }).Result;
            results =
                servers.Select(
                    s => s.ToManagedMachine());
                        
            return results.ToList();
        }

        /// <summary>
        /// Checks for the existence of the default VLAN (in app.config)
        /// and returns the ID of that VLAN
        /// </summary>
        /// <param name="connectionSettings"></param>
        /// <returns></returns>
        private Guid? GetVlanId(ConnectionSettings connectionSettings)
        {
            ComputeApiClient client = connectionSettings.GetComputeClient();
            var vlans = client.Networking.Vlan.GetVlans(new VlanListOptions()
            {
                Name = _defaultVlan
            }).Result;
            if (vlans == null || !vlans.Any())
                throw new Exception("Must have default VLAN provisioned first");
            else
                return Guid.Parse(vlans.First().id);
        }

        /// <summary>
        /// Checks for the existence of the default Network Domain 
        /// and returns the ID of that network domain
        /// </summary>
        /// <param name="connectionSettings"></param>
        /// <returns></returns>
        private Guid? GetNetworkDomainId(ConnectionSettings connectionSettings)
        {
            ComputeApiClient client = connectionSettings.GetComputeClient();
            var domains = client.Networking.NetworkDomain.GetNetworkDomains(new NetworkDomainListOptions()
            {
                Name = _defaultNetworkDomain
            }).Result;
            if (domains == null || !domains.Any())
                throw new Exception("Must have default network domain provisioned first");
            else
                return Guid.Parse(domains.First().id);
        }
    }
}
