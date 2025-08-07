namespace Vad.Reef.Logic.Message
{
    using Vad.Reef.Logic.Message;
    using Vad.Reef.Titan.Logic.Message;
    using Vad.Reef.Logic.Message.Auth;
    using Vad.Reef.Logic.Message.Home;

    public class LogicReefMessageFactory : LogicMessageFactory
    {
        public static LogicReefMessageFactory Instance;

        static LogicReefMessageFactory()
        {
            Instance = new LogicReefMessageFactory();
        }


        private readonly Dictionary<int, Type> _allMessages;
        public LogicReefMessageFactory() : base()
        {
            _allMessages = new Dictionary<int, Type> {
                { 10101, typeof(LoginMessage) },
                { 10212, typeof(ChangeAvatarNameMessage) }
            };
        }

        public override PiranhaMessage? CreateMessageByType(int messageType)
        {
            if (_allMessages.ContainsKey(messageType))
            {
                return Activator.CreateInstance(_allMessages[messageType]) as PiranhaMessage;
            }
            else
            {
                return null;
            }
        }

        public void Destruct()
        {
            throw new NotImplementedException();
        }
    }
}