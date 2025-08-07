using System.Security.Cryptography;
using Vad.Reef.Titan.Logic.DataStream;
using Vad.Reef.Titan.Logic.Math;

namespace Vad.Reef.Logic.Home
{
    public class LogicClientHome : LogicBase
    {
        public LogicLong _homeId;
        public string _homeJSON;
        public string _homeBaseLevel;
        public int _shieldDurationSeconds;
        public int _defenseRating;
        public int _defenseKFactor;

        public LogicClientHome()
        {
        }

        public override void Encode(ChecksumEncoder encoder)
        {
            base.Encode(encoder);
            encoder.WriteLong(_homeId);
            encoder.WriteString(_homeJSON);
            encoder.WriteString(_homeBaseLevel);
            encoder.WriteInt(_shieldDurationSeconds);
            encoder.WriteInt(_defenseRating);
            encoder.WriteInt(_defenseKFactor);
        }

    }
}
