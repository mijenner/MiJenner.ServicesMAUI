using LiteDB;

namespace MiJenner.ServicesMAUI
{
    public class CollectionService<T> : ICollectionService<T>
    {
        private List<T> collection = new List<T>(); 
        private const string dbCollectionName = "db";

        public CollectionService() { }

        public Task Save(T item) 
        {
            using var db = new LiteDatabase(GetFilePath());
            var coll = db.GetCollection<T>(dbCollectionName);
            coll.Insert(item);
            return Task.CompletedTask;
        }

        public Task Update(T item)
        {
            using var db = new LiteDatabase(GetFilePath());
            var coll = db.GetCollection<T>(dbCollectionName);
            coll.Update(item);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAll()
        {
            using var db = new LiteDatabase(GetFilePath());
            var coll = db.GetCollection<T>(dbCollectionName);
            var coll2 = coll.FindAll(); 
            collection = new List<T>();
            foreach (T item in coll2)
            {
                collection.Add(item);
            }
            return collection; 
        }


        private string GetFilePath()
        {
            var path = FileSystem.Current.AppDataDirectory;
            var fullPath = Path.Combine(path, "collection.db"); 
            return fullPath;
        }
    }
}
