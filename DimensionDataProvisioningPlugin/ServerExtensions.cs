using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citrix.ManagedMachineAPI;
using DD.CBU.Compute.Api.Contracts.Network20;

namespace DimensionDataProvisioningPlugin
{
    /// <summary>
    /// Extension methods for the CaaS server model
    /// </summary>
    public static class ServerExtensions
    {
        public static List<string> GetIpAddresses(this DD.CBU.Compute.Api.Contracts.Network20.ServerType server)
        {
            List<string> ips = new List<string>();
            ips.Add(server.networkInfo.primaryNic.privateIpv4);
            if (server.networkInfo.additionalNic != null)
            {
                ips.AddRange(server.networkInfo.additionalNic.Select(ip => ip.privateIpv4));
            }
            return ips;
        }

        public static List<string> GetFqdnList(this DD.CBU.Compute.Api.Contracts.Network20.ServerType server)
        {
            // TODO : Figure out the FQDN process
            return new List<string>() {server.name + ".servers.cloud.dimensiondata.com"};  
        }

        public static MachineState GetMachineState(this DD.CBU.Compute.Api.Contracts.Network20.ServerType server)
        {
            // TODO : This is good enough for now, but mapping transitional states would be preferable by
            // looking at the task(s) being executed on the server
            if (server.started)
                return MachineState.PoweredOn;
            else
            {
                return MachineState.PoweredOff;
            }
        }

        public static ManagedMachine ToManagedMachine(this ServerType server)
        {
            return new ManagedMachine(server.id, server.GetFqdnList(), server.name,
                server.GetIpAddresses(), server.GetMachineState(), "default");
        }
    }
}
