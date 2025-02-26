using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace BCI2000 {
    public class NetworkExtensions {
        public static bool IsConnected(this TcpClient client) {
            return client.GetState() is not (
                TcpState.Unknown or TcpState.Closed or
                TcpState.Closing or TcpState.CloseWait
            );
        }

        public static TcpState GetState(this TcpClient client) {
            TcpConnectionInformation matchingConnection
            = IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpConnections()
                .SingleOrDefault(x => x.LocalEndPoint.Equals(
                    client.Client.LocalEndPoint
                    )
                );
            return matchingConnection?.State ?? TcpState.Unknown;
        }
    }
}