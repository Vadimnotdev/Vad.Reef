using System.Security.Cryptography;
using Vad.Reef.Titan.Logic.DataStream;
using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Logic.Home;
using System.Diagnostics;
namespace Vad.Reef.Logic.Avatar
{
    public class LogicClientAvatar : LogicBase
    {
        private LogicLong _id;
        private LogicLong _currentHomeId;
        private bool _isInAlliance;
        private int _townHallLevel;
        private string _name;
        private string _facebookId;
        private int _expLevel;
        private int _expPoints;
        private int _diamonds;
        private int _freeDiamonds;
        private int _attackRating;
        private int _attackKFactor;
        private int _score;
        private bool _nameSetByUser;
        private int _cumulativePurchasedDiamonds;
        private int _resourceCaps;
        private int _resourcesCount; //cycle
        private int[] _resourcesId = { 3000001, 3000002 };
        private int[] _resources = { 888, 999 };
        private int _unitCount; //cycle
        private int _spellUpgradeLevel; //cycle
        private int _npcSeen; //cycle
        private int _trapUpgrade; //cycle
        private int _unitUpgradeLevel; //cycle
        private int _buildingLevel; //cycle
        private int _artifactBonus; //cycle
        private int _landingBoatLevel; //cycle
        private int _missionCompleted; //cycle
        private int _achievementRewardClaimed; //cycle
        private int _achievementProgress; //cycle
        private int _npcMapproress; //cycle
        //Logic player map encode
        private int _regionCount;
        private int _explorationStarted;
        private bool _explorationEnded;
        //unknown int
        //unknown int
        //unknown int

        public LogicClientAvatar()
        {
            this._id = new LogicLong(0, 1);
            this._currentHomeId = new LogicLong(0, 1);
            this._isInAlliance = false;
            this._townHallLevel = 1;
            this._name = "Vadim_not_dev";
            this._facebookId = "FacebookID";
            this._expLevel = 15;
            this._expPoints = 5;
            this._diamonds = 99999;
            this._freeDiamonds = 0;
            this._attackRating = 0;
            this._attackKFactor = 0;
            this._score = 9999;
            this._nameSetByUser = false;
            this._cumulativePurchasedDiamonds = 0;
            this._resourceCaps = 0;
            this._resourcesCount = 2;
            this._unitCount = 0;
            this._spellUpgradeLevel = 0;
            this._npcSeen = 0;
            this._trapUpgrade = 0;
            this._unitUpgradeLevel = 0;
            this._buildingLevel = 0;
            this._artifactBonus = 0;
            this._landingBoatLevel = 0;
            this._missionCompleted = 0;
            this._achievementRewardClaimed = 0;
            this._achievementProgress = 0;
            this._npcMapproress = 0;
            this._regionCount = 3;
            this._explorationStarted = 0;
            this._explorationEnded = false;
            
        }

        public void Encode(ChecksumEncoder encoder)
        {
            base.Encode(encoder);
            encoder.WriteLong(this._id);
            encoder.WriteLong(this._currentHomeId);
            encoder.WriteBoolean(this._isInAlliance);
            encoder.WriteInt(this._townHallLevel);
            encoder.WriteString(this._name);
            encoder.WriteString(this._facebookId);
            encoder.WriteInt(this._expLevel);
            encoder.WriteInt(this._expPoints);
            encoder.WriteInt(this._diamonds);
            encoder.WriteInt(this._freeDiamonds);
            encoder.WriteInt(this._attackRating);
            encoder.WriteInt(this._attackKFactor);
            encoder.WriteInt(this._score);
            encoder.WriteBoolean(this._nameSetByUser);
            encoder.WriteInt(this._cumulativePurchasedDiamonds);
            encoder.WriteInt(this._resourceCaps);
            encoder.WriteInt(this._resourcesCount);
            for (int i = 0; i < 2; i++)
            {
                encoder.WriteInt(_resourcesId[i]);
                encoder.WriteInt(_resources[i]);
            }
            encoder.WriteInt(this._unitCount);
            encoder.WriteInt(this._unitUpgradeLevel);
            encoder.WriteInt(this._spellUpgradeLevel);
            encoder.WriteInt(this._npcSeen);
            encoder.WriteInt(this._trapUpgrade);
            encoder.WriteInt(this._buildingLevel);
            encoder.WriteInt(this._artifactBonus);            
            encoder.WriteInt(this._landingBoatLevel);
            encoder.WriteInt(this._missionCompleted);
            encoder.WriteInt(this._achievementRewardClaimed);
            encoder.WriteInt(this._achievementProgress);
            encoder.WriteInt(this._npcMapproress);
            encoder.WriteInt(this._explorationStarted);
            encoder.WriteBoolean(this._explorationEnded);
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
