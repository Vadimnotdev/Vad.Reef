using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Vad.Reef.Titan.Logic.Math;

namespace Vad.Reef.Server.Database
{
    public class Accounts
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string passToken { get; set; }

        public int highId { get; set; } 

        public int lowId { get; set; }

        public string homeJSON { get; set; }

        public string homeBaseLevel { get; set; }

        public int shieldDurationSeconds { get; set; }

        public int defenseRating { get; set; }

        public int defenseKFactor { get; set; }

        public bool isInAlliance { get; set; }

        public int townHallLevel { get; set; }

        public string name { get; set; }

        public string facebookId { get; set; }

        public int expLevel { get; set; }

        public int expPoints { get; set; }

        public int diamonds { get; set; }

        public int freeDiamonds { get; set; }

        public int attackRating { get; set; }

        public int attackKFactor { get; set; }

        public int score { get; set; }

        public bool nameSetByUser { get; set; }

        public int cumulativePurchasedDiamonds { get; set; }

        public int resourceCaps { get; set; }

        public int resourcesCount { get; set; }

        public int gold {  get; set; }

        public int wood { get; set; }

        public int stone { get; set; }

        public int iron { get; set; }

        public int unitCount { get; set; }

        public int unitUpgradeLevel { get; set; }

        public int spellUpgradeLevel { get; set; }

        public int npcSeen { get; set; }

        public int trapUpgrade {  get; set; }

        public int buildingLevel { get; set; }

        public int artifactBonus { get; set; }

        public int landingBoatLevel { get; set; }

        public int missionCompleted { get; set; }

        public int achievementRewardClaimed { get; set; }   

        public int achievementProgress {  get; set; }

        public int npcMapproress { get; set; }

        public int explorationStarted { get; set; }

        public bool explorationEnded { get; set; }
    }

    public class DatabaseManger
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Accounts> collection;

        public DatabaseManger()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("Vad_Reef");
            collection = database.GetCollection<Accounts>("Accounts");
        }

        public async Task<Accounts> GetAccountAsync(int highId, int lowId)
        {
            var filter = Builders<Accounts>.Filter.Eq(a => a.highId, highId) &
                         Builders<Accounts>.Filter.Eq(a => a.lowId, lowId);

            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAccountNameAsync(int highId, int lowId, string newName)
        {
            var filter = Builders<Accounts>.Filter.Eq(a => a.highId, highId) &
                         Builders<Accounts>.Filter.Eq(a => a.lowId, lowId);

            var update = Builders<Accounts>.Update.Set(a => a.name, newName);

            await collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateAccountExpLevelAsync(int highId, int lowId, int level)
        {
            var filter = Builders<Accounts>.Filter.Eq(a => a.highId, highId) &
                         Builders<Accounts>.Filter.Eq(a => a.lowId, lowId);

            var update = Builders<Accounts>.Update.Set(a => a.expLevel, level);

            await collection.UpdateOneAsync(filter, update);
        }


        public async Task<(int highId, int lowId)> GetLastAccountIdAsync()
        {
            var lastAccount = await collection
                .Find(_ => true)
                .SortByDescending(a => a.Id)
                .FirstOrDefaultAsync();

            return lastAccount != null ? (lastAccount.highId, lastAccount.lowId) : (0, 0);
        }


        public async Task CreateDefaultAccountAsync(int highId, int lowId)
        {
            var account = new Accounts
            {
                highId = highId,
                lowId = lowId,
                passToken = GenerateRandomToken(),
                homeJSON = "{\"buildings\":[{\"data\":1000000,\"lvl\":0,\"x\":38,\"y\":33},{\"data\":1000001,\"lvl\":0,\"x\":35,\"y\":38,\"res_time\":14993},{\"data\":1000002,\"lvl\":0,\"x\":32,\"y\":32,\"res_time\":14994},{\"data\":1000017,\"lvl\":0,\"x\":52,\"y\":32,\"units\":[],\"storage_type\":0,\"boat_index\":0,\"unit_prod\":{\"unit_type\":0,\"total_time\":0}},{\"data\":1000015,\"lvl\":0,\"x\":58,\"y\":38}],\"obstacles\":[{\"data\":8000005,\"x\":23,\"y\":5},{\"data\":8000005,\"x\":4,\"y\":21},{\"data\":8000005,\"x\":34,\"y\":5},{\"data\":8000005,\"x\":8,\"y\":29},{\"data\":8000005,\"x\":28,\"y\":9},{\"data\":8000005,\"x\":6,\"y\":14},{\"data\":8000005,\"x\":12,\"y\":8},{\"data\":8000005,\"x\":19,\"y\":20},{\"data\":8000005,\"x\":17,\"y\":13},{\"data\":8000005,\"x\":42,\"y\":20},{\"data\":8000004,\"x\":14,\"y\":25},{\"data\":8000004,\"x\":13,\"y\":35},{\"data\":8000004,\"x\":35,\"y\":18},{\"data\":8000004,\"x\":43,\"y\":14},{\"data\":8000004,\"x\":35,\"y\":27},{\"data\":8000004,\"x\":15,\"y\":41},{\"data\":8000004,\"x\":28,\"y\":21},{\"data\":8000000,\"x\":47,\"y\":34},{\"data\":8000004,\"x\":7,\"y\":38},{\"data\":8000004,\"x\":45,\"y\":20},{\"data\":8000004,\"x\":47,\"y\":36},{\"data\":8000004,\"x\":20,\"y\":31},{\"data\":8000003,\"x\":9,\"y\":11},{\"data\":8000003,\"x\":15,\"y\":5},{\"data\":8000003,\"x\":19,\"y\":5},{\"data\":8000003,\"x\":26,\"y\":5},{\"data\":8000003,\"x\":30,\"y\":5},{\"data\":8000003,\"x\":37,\"y\":5},{\"data\":8000003,\"x\":9,\"y\":15},{\"data\":8000003,\"x\":13,\"y\":11},{\"data\":8000003,\"x\":17,\"y\":9},{\"data\":8000003,\"x\":4,\"y\":17},{\"data\":8000003,\"x\":4,\"y\":24},{\"data\":8000003,\"x\":8,\"y\":19},{\"data\":8000003,\"x\":13,\"y\":15},{\"data\":8000003,\"x\":24,\"y\":9},{\"data\":8000003,\"x\":41,\"y\":6},{\"data\":8000003,\"x\":45,\"y\":9},{\"data\":8000003,\"x\":45,\"y\":13},{\"data\":8000003,\"x\":31,\"y\":9},{\"data\":8000003,\"x\":35,\"y\":9},{\"data\":8000003,\"x\":39,\"y\":10},{\"data\":8000003,\"x\":18,\"y\":16},{\"data\":8000008,\"x\":21,\"y\":10},{\"data\":8000008,\"x\":28,\"y\":12},{\"data\":8000008,\"x\":4,\"y\":30},{\"data\":8000008,\"x\":20,\"y\":13},{\"data\":8000008,\"x\":8,\"y\":23},{\"data\":8000008,\"x\":22,\"y\":21},{\"data\":8000007,\"x\":4,\"y\":28},{\"data\":8000007,\"x\":6,\"y\":28},{\"data\":8000002,\"x\":7,\"y\":34},{\"data\":8000002,\"x\":12,\"y\":19},{\"data\":8000002,\"x\":23,\"y\":13},{\"data\":8000002,\"x\":31,\"y\":13},{\"data\":8000002,\"x\":35,\"y\":13},{\"data\":8000002,\"x\":39,\"y\":14},{\"data\":8000002,\"x\":45,\"y\":22},{\"data\":8000002,\"x\":37,\"y\":18},{\"data\":8000002,\"x\":22,\"y\":17},{\"data\":8000007,\"x\":43,\"y\":16},{\"data\":8000008,\"x\":41,\"y\":18},{\"data\":8000008,\"x\":46,\"y\":17},{\"data\":8000007,\"x\":44,\"y\":18},{\"data\":8000007,\"x\":47,\"y\":20},{\"data\":8000007,\"x\":16,\"y\":19},{\"data\":8000007,\"x\":11,\"y\":37},{\"data\":8000007,\"x\":13,\"y\":41},{\"data\":8000008,\"x\":14,\"y\":38},{\"data\":8000008,\"x\":9,\"y\":32},{\"data\":8000008,\"x\":8,\"y\":26},{\"data\":8000008,\"x\":11,\"y\":23},{\"data\":8000008,\"x\":16,\"y\":21},{\"data\":8000008,\"x\":42,\"y\":22},{\"data\":8000002,\"x\":27,\"y\":14},{\"data\":8000002,\"x\":31,\"y\":17},{\"data\":8000002,\"x\":11,\"y\":28},{\"data\":8000007,\"x\":12,\"y\":26},{\"data\":8000007,\"x\":14,\"y\":23},{\"data\":8000008,\"x\":12,\"y\":32},{\"data\":8000008,\"x\":26,\"y\":18},{\"data\":8000001,\"x\":15,\"y\":35},{\"data\":8000001,\"x\":15,\"y\":27},{\"data\":8000001,\"x\":16,\"y\":24},{\"data\":8000001,\"x\":25,\"y\":21},{\"data\":8000001,\"x\":19,\"y\":24},{\"data\":8000001,\"x\":30,\"y\":21},{\"data\":8000001,\"x\":46,\"y\":26},{\"data\":8000001,\"x\":34,\"y\":21},{\"data\":8000001,\"x\":37,\"y\":22},{\"data\":8000001,\"x\":16,\"y\":31},{\"data\":8000001,\"x\":19,\"y\":28},{\"data\":8000001,\"x\":23,\"y\":25},{\"data\":8000001,\"x\":26,\"y\":24},{\"data\":8000001,\"x\":40,\"y\":24},{\"data\":8000000,\"x\":16,\"y\":43},{\"data\":8000000,\"x\":18,\"y\":34},{\"data\":8000000,\"x\":23,\"y\":29},{\"data\":8000000,\"x\":30,\"y\":25},{\"data\":8000000,\"x\":34,\"y\":24},{\"data\":8000000,\"x\":37,\"y\":26},{\"data\":8000000,\"x\":43,\"y\":27},{\"data\":8000000,\"x\":47,\"y\":30},{\"data\":8000000,\"x\":39,\"y\":28},{\"data\":8000000,\"x\":27,\"y\":29},{\"data\":8000000,\"x\":22,\"y\":35},{\"data\":8000000,\"x\":33,\"y\":28},{\"data\":8000006,\"x\":44,\"y\":31},{\"data\":8000006,\"x\":25,\"y\":33},{\"data\":8000006,\"x\":29,\"y\":27},{\"data\":8000006,\"x\":33,\"y\":26},{\"data\":8000006,\"x\":36,\"y\":30},{\"data\":8000006,\"x\":18,\"y\":39},{\"data\":8000006,\"x\":41,\"y\":29},{\"data\":8000007,\"x\":4,\"y\":35}],\"traps\":[],\"decos\":[],\"resource_ships\":[],\"map_spawn_timer\":5364,\"map_unliberation_timer\":14364,\"upgrade_outpost_defenses\":false,\"seed\":-1014833735}",
                homeBaseLevel = "layout/playerbase.level",
                shieldDurationSeconds = 0,
                defenseRating = 0,
                defenseKFactor = 0,
                isInAlliance = false,
                townHallLevel = 0,
                name = "",
                facebookId = "FacebookID",
                expLevel = 9,
                expPoints = 0,
                diamonds = 99999,
                freeDiamonds = 0,
                attackRating = 0,
                attackKFactor = 0,
                score = 99999,
                nameSetByUser = false,
                cumulativePurchasedDiamonds = 0,
                resourceCaps = 0,
                resourcesCount = 2,
                gold = 99999,
                wood = 99999,
                stone = 99999,
                iron = 99999,
                unitCount = 0,
                unitUpgradeLevel = 0,
                spellUpgradeLevel = 0,
                npcSeen = 0,
                trapUpgrade = 0,
                buildingLevel = 0,
                artifactBonus = 0,
                landingBoatLevel = 0,
                missionCompleted = 0,
                achievementRewardClaimed = 0,
                achievementProgress = 0,
                npcMapproress = 0,
                explorationStarted = 0,
                explorationEnded = false
            };


            await collection.InsertOneAsync(account);
        }


        private static string GenerateRandomToken(int length = 40)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var data = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[data[i] % chars.Length];
            }

            return new string(result);
        }

    }

}
