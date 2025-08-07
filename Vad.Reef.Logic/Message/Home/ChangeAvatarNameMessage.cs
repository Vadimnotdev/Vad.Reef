using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vad.Reef.Logic.Helper;
using Vad.Reef.Titan.Logic.Message;

namespace Vad.Reef.Logic.Message.Home
{
    public class ChangeAvatarNameMessage : PiranhaMessage
    {
        public string _newName;
        public bool _nameSetByUser;
        public override void Decode()
        {
            base.Decode();
            _newName = this.stream.ReadString();
            _nameSetByUser = this.stream.ReadBoolean();

        }

        public string GetNewName()
        {
            return _newName;
        }
        public override int GetMessageType()
        {
            return 10212;
        }
    }
}
