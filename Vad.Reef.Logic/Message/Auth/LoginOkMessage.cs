using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Titan.Logic.Message;

namespace Vad.Reef.Logic.Message.Auth;

public class LoginOkMessage : PiranhaMessage
{
    private LogicLong _accountId;
    private LogicLong _homeId;
    private string _passToken;
    private string _facebookId;
    private string _gamecenterId;
    private int _serverMajorVersion;
    private int _serverBuild;
    private int _contentVersion;
    private string _serverEnvironment;
    private string _facebookAppId;
    private string _googleServiceId;

    public LoginOkMessage() : base(0)
    {
        this._accountId = new LogicLong(0, 1);
        this._homeId = new LogicLong(0, 1);
        this._passToken = "PassToken";
        this._facebookId = "FacebookId";
        this._gamecenterId = "GameCenterId";
        this._serverMajorVersion = 3;
        this._serverBuild = 16;
        this._contentVersion = 1;
        this._serverEnvironment = "ServerEnvironment";
        this._facebookAppId = "FacebookAppId";
        this._googleServiceId = "GoogleServiceId";

    }

    public override void Encode()
    {
        base.Encode();
        this.stream.WriteLong(this._accountId);
        this.stream.WriteLong(this._homeId);
        this.stream.WriteString(this._passToken);
        this.stream.WriteString(this._facebookId);
        this.stream.WriteString(this._gamecenterId);
        this.stream.WriteInt(this._serverMajorVersion);
        this.stream.WriteInt(this._serverBuild);
        this.stream.WriteInt(this._contentVersion);
        this.stream.WriteString(this._serverEnvironment);
        this.stream.WriteString(this._facebookAppId);
        this.stream.WriteString(this._googleServiceId);

    }

    public override int GetMessageType()
    {
        return 20104;
    }

}