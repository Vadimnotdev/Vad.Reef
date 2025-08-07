using Vad.Reef.Logic.Home;
using Vad.Reef.Logic.Message.Auth;
using Vad.Reef.Logic.Message.Home;
using Vad.Reef.Logic.Avatar;
using Vad.Reef.Server.Database;
using Vad.Reef.Server.Network.Connection;
using Vad.Reef.Titan.Logic.Debug;
using Vad.Reef.Titan.Logic.Math;
using Vad.Reef.Titan.Logic.Message;

namespace Vad.Reef.Server.Protocol;

class MessageManager
{
    private ClientConnection _connection;
    private LogicLong _currentAccountId;
    DatabaseManger dbManager = new DatabaseManger();

    public MessageManager(ClientConnection connection)
    {
        this._connection = connection;
    }

    public async Task ReceiveMessage(PiranhaMessage message)
    {
        int messageType = message.GetMessageType();

        if (messageType != 14102)
            Debugger.Log($"MessageManager.ReceiveMessage: type={messageType}, name=" + message.GetType().Name);

        switch (message.GetMessageType())
        {
            case 10101:
                await this.OnLoginMessageReceived((LoginMessage)message);
                break;
            case 10212:
                await this.OnChangeAvatarNameRecived((ChangeAvatarNameMessage)message);
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
        OwnHomeDataMessage ownHomeDataMessage = null;

        if (accountId.GetHigherInt() == 0 && accountId.GetLowerInt() == 0)
        {
            var (lastHighId, lastLowId) = await dbManager.GetLastAccountIdAsync();
            int newHighId = lastHighId;
            int newLowId = lastLowId + 1;
            LogicLong accountIdnew = new LogicLong(newHighId, newLowId);
            await dbManager.CreateDefaultAccountAsync(newHighId, newLowId);
            var account = await dbManager.GetAccountAsync(newHighId, newLowId);

            loginOkMessage._accountId = new LogicLong(account.highId, account.lowId);
            loginOkMessage._homeId = new LogicLong(account.highId, account.lowId);
            loginOkMessage._passToken = account.passToken;

            ownHomeDataMessage = SetAccountData(account);
            _currentAccountId = accountIdnew;
        }
        else
        {
            int highId = accountId.GetHigherInt();
            int lowId = accountId.GetLowerInt();
            var account = await dbManager.GetAccountAsync(highId, lowId);

            loginOkMessage._accountId = new LogicLong(account.highId, account.lowId);
            loginOkMessage._homeId = new LogicLong(account.highId, account.lowId);
            loginOkMessage._passToken = account.passToken;

            ownHomeDataMessage = SetAccountData(account); // 👈 и тут
            _currentAccountId = accountId;
        }

        await _connection.SendMessage(loginOkMessage);
        await _connection.SendMessage(ownHomeDataMessage);
    }


    private async Task OnChangeAvatarNameRecived(ChangeAvatarNameMessage changeAvatarNameMessage)
    {
        int highId = _currentAccountId.GetHigherInt();
        int lowId = _currentAccountId.GetLowerInt();

        string name = changeAvatarNameMessage.GetNewName();
        await dbManager.UpdateAccountNameAsync(highId, lowId, name);
        await dbManager.UpdateAccountExpLevelAsync(highId, lowId, 10);
        var account = await dbManager.GetAccountAsync(highId, lowId);
        OwnHomeDataMessage ownHomeDataMessage = SetAccountData(account);

        await _connection.SendMessage(ownHomeDataMessage);


    }



    private OwnHomeDataMessage SetAccountData(Accounts account)
    {
        var ownHomeDataMessage = new OwnHomeDataMessage();
        ownHomeDataMessage._logicClientHome = new LogicClientHome
        {
            _homeId = new LogicLong(account.highId, account.lowId),
            _homeJSON = account.homeJSON,
            _homeBaseLevel = account.homeBaseLevel,
            _shieldDurationSeconds = account.shieldDurationSeconds,
            _defenseRating = account.defenseRating,
            _defenseKFactor = account.defenseKFactor
        };

        ownHomeDataMessage._logicClientAvatar = new LogicClientAvatar
        {
            _id = new LogicLong(account.highId, account.lowId),
            _currentHomeId = new LogicLong(account.highId, account.lowId),
            _isInAlliance = account.isInAlliance,
            _townHallLevel = account.townHallLevel,
            _name = account.name,
            _facebookId = account.facebookId,
            _expLevel = account.expLevel,
            _expPoints = account.expPoints,
            _diamonds = account.diamonds,
            _freeDiamonds = account.freeDiamonds,
            _attackRating = account.attackRating,
            _attackKFactor = account.attackKFactor,
            _score = account.score,
            _nameSetByUser = account.nameSetByUser,
            _cumulativePurchasedDiamonds = account.cumulativePurchasedDiamonds,
            _resourceCaps = account.resourceCaps,
            _resourcesCount = account.resourcesCount,
            _gold = account.gold,
            _wood = account.wood,
            _stone = account.stone,
            _iron = account.iron,
            _unitCount = account.unitCount,
            _spellUpgradeLevel = account.spellUpgradeLevel,
            _npcSeen = account.npcSeen,
            _trapUpgrade = account.trapUpgrade,
            _unitUpgradeLevel = account.unitUpgradeLevel,
            _buildingLevel = account.buildingLevel,
            _artifactBonus = account.artifactBonus,
            _landingBoatLevel = account.landingBoatLevel,
            _missionCompleted = account.missionCompleted,
            _achievementRewardClaimed = account.achievementRewardClaimed,
            _achievementProgress = account.achievementProgress,
            _npcMapproress = account.npcMapproress,
            _explorationStarted = account.explorationStarted,
            _explorationEnded = account.explorationEnded
        };

        return ownHomeDataMessage;
    }


}