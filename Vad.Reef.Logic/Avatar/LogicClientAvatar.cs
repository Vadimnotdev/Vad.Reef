using System.Security.Cryptography;
using Vad.Reef.Titan.Logic.DataStream;
using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Logic.Home;
using System.Diagnostics;
namespace Vad.Reef.Logic.Avatar
{
    public class LogicClientAvatar : LogicBase
    {
        public LogicLong _id;
        public LogicLong _currentHomeId;
        public bool _isInAlliance;
        public int _townHallLevel;
        public string _name;
        public string _facebookId;
        public int _expLevel;
        public int _expPoints;
        public int _diamonds;
        public int _freeDiamonds;
        public int _attackRating;
        public int _attackKFactor;
        public int _score;
        public bool _nameSetByUser;
        public int _cumulativePurchasedDiamonds;
        public int _resourceCaps;
        public int _resourcesCount; //cycle
        public int _gold;
        public int _wood;
        public int _stone;
        public int _iron;
        public int[] _resourcesId1 = { 3000001, 3000002 };
        public int[] _resourcesId2 = { 3000001, 3000002, 3000003 };
        public int[] _resourcesId3 = { 3000001, 3000002, 3000003, 3000004 };
        public int _unitCount; //cycle
        public int _spellUpgradeLevel; //cycle
        public int _npcSeen; //cycle
        public int _trapUpgrade; //cycle
        public int _unitUpgradeLevel; //cycle
        public int _buildingLevel; //cycle
        public int _artifactBonus; //cycle
        public int _landingBoatLevel; //cycle
        public int _missionCompleted; //cycle
        public int _achievementRewardClaimed; //cycle
        public int _achievementProgress; //cycle
        public int _npcMapproress; //cycle
        //Logic player map encode
        public int _regionCount;
        public int _explorationStarted;
        public bool _explorationEnded;
        //unknown int
        //unknown int
        //unknown int
        //unknown int
        //unknown int
        //unknown int
        //unknown int

        public LogicClientAvatar()
        {
            
        }

        public void Encode(ChecksumEncoder encoder)
        {
            base.Encode(encoder);
            encoder.WriteLong(_id);
            encoder.WriteLong(_currentHomeId);
            encoder.WriteBoolean(_isInAlliance);
            encoder.WriteInt(_townHallLevel);
            encoder.WriteString(_name);
            encoder.WriteString(_facebookId);
            encoder.WriteInt(_expLevel);
            encoder.WriteInt(_expPoints);
            encoder.WriteInt(_diamonds);
            encoder.WriteInt(_freeDiamonds);
            encoder.WriteInt(_attackRating);
            encoder.WriteInt(_attackKFactor);
            encoder.WriteInt(_score);
            encoder.WriteBoolean(_nameSetByUser);
            encoder.WriteInt(_cumulativePurchasedDiamonds);
            encoder.WriteInt(_resourceCaps);
            encoder.WriteInt(_resourcesCount);
            int[] _resources;
            if (_resourcesCount == 2)
            {
                _resources = new int[] { _gold, _wood };
                for (int i = 0; i < _resourcesCount; i++)
                {
                    encoder.WriteInt(_resourcesId1[i]);
                    encoder.WriteInt(_resources[i]);
                }
            }
            else if (_resourcesCount == 3)
            {
                _resources = new int[] { _gold, _wood, _stone };
                for (int i = 0; i < _resourcesCount; i++)
                {
                    encoder.WriteInt(_resourcesId2[i]);
                    encoder.WriteInt(_resources[i]);
                }
            }
            else
            {
                _resources = new int[] { _gold, _wood, _stone, _iron };
                for (int i = 0; i < _resourcesCount; i++)
                {
                    encoder.WriteInt(_resourcesId3[i]);
                    encoder.WriteInt(_resources[i]);
                }
            }

            encoder.WriteInt(_unitCount);
            encoder.WriteInt(_unitUpgradeLevel);
            encoder.WriteInt(_spellUpgradeLevel);
            encoder.WriteInt(_npcSeen);
            encoder.WriteInt(_trapUpgrade);
            encoder.WriteInt(_buildingLevel);
            encoder.WriteInt(_artifactBonus);            
            encoder.WriteInt(_landingBoatLevel);
            encoder.WriteInt(_missionCompleted);
            encoder.WriteInt(_achievementRewardClaimed);
            encoder.WriteInt(_achievementProgress);
            encoder.WriteInt(_npcMapproress);
            encoder.WriteInt(_explorationStarted);
            encoder.WriteBoolean(_explorationEnded);
            encoder.WriteInt(0);
            encoder.WriteInt(0);
            encoder.WriteInt(0);
            encoder.WriteInt(0);
            encoder.WriteInt(0);
            encoder.WriteInt(0);
            encoder.WriteInt(0);

        }

    }
}
