using Vad.Reef.Server.Network.Connection;
using Vad.Reef.Titan.Logic.Message;
using Vad.Reef.Titan.Logic.Debug;
using Vad.Reef.Logic.Message.Auth;
using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Logic.Message.Home;

namespace Vad.Reef.Server.Protocol;

class MessageManager
{
    private ClientConnection _connection;

    public MessageManager(ClientConnection connection)
    {
        this._connection = connection;
    }

    public async Task ReceiveMessage(PiranhaMessage message)
    {
        int messageType = message.GetMessageType();

        if (messageType != 10108)
            Debugger.Log($"MessageManager.ReceiveMessage: type={messageType}, name=" + message.GetType().Name);

        switch (message.GetMessageType())
        {
            case 10101:
                await this.OnLoginMessageReceived((LoginMessage)message);
                break;
        }
    }

    private async Task OnLoginMessageReceived(LoginMessage loginMessage)
    {
        LogicLong accountId = loginMessage.GetAccountId();
        string? passToken = loginMessage.GetPassToken();

        Debugger.Log($"Tryna login id={accountId}, passToken={passToken}, client version={loginMessage.GetMajorVersion()}.{loginMessage.GetBuild()}, device language={loginMessage.GetPreferredDeviceLanguage()}");
        Debugger.Log($"client sha={loginMessage.GetResourceSHA()}");

        LoginOkMessage loginOkMessage = new();
        OwnHomeDataMessage ownHomeDataMessage = new();
        await _connection.SendMessage(loginOkMessage);
        await _connection.SendMessage(ownHomeDataMessage);

    }

}