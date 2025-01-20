using MongoDB.Bson;
using MongoDB.Driver;

namespace Labb_02_Dungeon_Crawler.Utils
{
    public class MongoDb
    {
        static string connectionString = "mongodb://localhost:27017/";
        public void SaveGame(LevelData level)
        {
            MongoClient client = new(connectionString);

            var collection = client.GetDatabase("KristofferLinder").GetCollection<LevelData>("leveldata");

            collection.InsertOne(level);
        }

        public async void GetSavedGames()
        {
            MongoClient client = new(connectionString);
            var collection = client.GetDatabase("KristofferLinder").GetCollection<LevelData>("leveldata");

            //collection.AggregateToCollectionAsync
        }

        public LevelData LoadGame()
        {
            MongoClient client = new(connectionString);

            var collection = client.GetDatabase("KristofferLinder").GetCollection<LevelData>("leveldata");

            var level = collection.Find(new BsonDocument()).FirstOrDefault();
            //var level = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();

            return level;
        }
    }
}

