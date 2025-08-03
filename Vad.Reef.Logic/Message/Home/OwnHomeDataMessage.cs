using Vad.Reef.Logic.Avatar;
using Vad.Reef.Logic.Home;
using Vad.Reef.Titan.Logic.Message;
using Vad.Reef.Titan.Logic.Debug;

namespace Vad.Reef.Logic.Message.Home
{
    public class OwnHomeDataMessage : PiranhaMessage
    {
        public LogicClientHome _logicClientHome = new();
        public LogicClientAvatar _logicClientAvatar = new();
        private int _secondsSinceLastSave = 0;

        public OwnHomeDataMessage() : base(0)
        {
        }

        public override void Encode()
        {
            base.Encode();
            this.stream.WriteInt(_secondsSinceLastSave);
            _logicClientHome.Encode(stream);
            _logicClientAvatar.Encode(stream);
            this.stream.WriteInt(0);
            this.stream.WriteInt(0);
        }

        public override int GetMessageType()
        {
            return 24101;
        }

    }
}
