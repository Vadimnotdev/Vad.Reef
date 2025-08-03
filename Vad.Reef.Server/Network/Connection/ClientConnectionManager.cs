using System.Collections.Concurrent;
using System.Net.Sockets;
using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Titan.Logic.Debug;

namespace Vad.Reef.Server.Network.Connection;

public class ClientConnectionManager
{
    public void OnConnect(Socket client)
    {
        Debugger.Log($"New connection from {client.RemoteEndPoint}");
        _ = RunSessionAsync(client);
    }


    private async Task RunSessionAsync(Socket client)
    {
        ClientConnection session = new ClientConnection(client);
        try
        {
            await session.Receive();
        }
        catch (OperationCanceledException ex) { }
        catch (Exception ex)
        {
            Debugger.Error($"Unhandled exception occurred while processing session, trace:\n{ex}");
        }
        finally
        {
            Debugger.Warning("User has disconnected");
            client.Dispose();
        }
    }
}