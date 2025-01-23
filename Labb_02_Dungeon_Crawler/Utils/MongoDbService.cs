using MongoDB.Bson;
using MongoDB.Driver;

namespace Labb_02_Dungeon_Crawler.Utils
{
    public class MongoDbService
    {
        public MongoClient Client { get; init; }
        public string Database { get; init; }

        public MongoDbService(string connection, string database)
        {
            Client = new(connection);
            Database = database;
        }

        public void SaveGame(LevelData level)
        {
            level.Saved = DateTime.Now;

            var collection = Client.GetDatabase(Database).GetCollection<LevelData>("savedgames");

            var filter = Builders<LevelData>.Filter.Eq("_id", level.Id);
            var saved = collection.Find(filter).FirstOrDefault();

            if (saved is null) collection.InsertOne(level);
            else
            {
                collection.ReplaceOne(filter, level);
            }
        }
        public async Task<List<LevelDataLight>> GetSavedGames()
        {
            var collection = Client.GetDatabase(Database).GetCollection<LevelDataLight>("savedgames");
            var games = await collection.Find(new BsonDocument()).ToListAsync();

            return games;
        }
        public LevelData LoadGame(ObjectId id)
        {
            var collection = Client.GetDatabase(Database).GetCollection<LevelData>("savedgames");
            var filter = Builders<LevelData>.Filter.Eq("_id", id);

            var level = collection.Find(filter).FirstOrDefault();

            return level;
        }
        public void RemoveGame(ObjectId id)
        {
            var collection = Client.GetDatabase(Database).GetCollection<LevelData>("savedgames");
            var filter = Builders<LevelData>.Filter.Eq("_id", id);

            collection.DeleteOne(filter);
        }
        public void SaveHighScore(HighScore score)
        {
            var collection = Client.GetDatabase(Database).GetCollection<HighScore>("highscores");

            collection.InsertOne(score);
        }
        public async Task<List<HighScore>> GetHighScores(string level)
        {
            var collection = Client.GetDatabase(Database).GetCollection<HighScore>("highscores");
            var filter = Builders<HighScore>.Filter.Eq("Level", level);

            var scores = await collection.Find(filter).ToListAsync();

            return scores;
        }
    }
}