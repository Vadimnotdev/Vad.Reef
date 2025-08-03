using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Titan.Logic.Message;
using Vad.Reef.Logic.Data;
using Vad.Reef.Logic.Helper;
namespace Vad.Reef.Logic.Message.Auth
{
    public class LoginMessage : PiranhaMessage
    {
        private LogicLong _accountId;
        private string _PassToken;
        private int _ClientMajorVersion;
        //readInt
        private int _ClientBuild;
        private string _ResourceSha;
        private string _UDID;
        private string _OpenUDID;
        private string _MacAddress;
        private string _Device;
        private LogicData? _PreferredLanguage;
        private string _PreferredDeviceLanguage;
        private string _ADID;
        private string _OSVersion;
        private bool _IsAdvertisingTrackingEnabled;

        public override void Decode()
        {
            base.Decode();
            _accountId = this.stream.ReadLong();
            _PassToken = this.stream.ReadString();
            _ClientMajorVersion = this.stream.ReadInt();
            this.stream.ReadInt();
            _ClientBuild = this.stream.ReadInt();
            _ResourceSha = this.stream.ReadString();
            _UDID = this.stream.ReadString();
            _OpenUDID = this.stream.ReadString();
            _MacAddress = this.stream.ReadString();
            _Device = this.stream.ReadString();
            _PreferredLanguage = ByteStreamHelper.ReadDataReference(stream);
            _PreferredDeviceLanguage = this.stream.ReadString();
            _ADID = this.stream.ReadString();
            _OSVersion = this.stream.ReadString();
            _IsAdvertisingTrackingEnabled = this.stream.ReadBoolean();


        }

        public LogicLong GetAccountId()
        {
            return _accountId;
        }

        public string? GetPassToken()
        {
            return _PassToken;
        }

        public int GetMajorVersion()
        {
            return _ClientMajorVersion;
        }

        public int GetBuild()
        {
            return _ClientBuild;
        }

        public string GetResourceSHA()
        {
            return _ResourceSha;
        }

        public string GetPreferredDeviceLanguage()
        {
            return _PreferredDeviceLanguage;
        }

        public override int GetMessageType()
        {
            return 10101;
        }
    }
}
