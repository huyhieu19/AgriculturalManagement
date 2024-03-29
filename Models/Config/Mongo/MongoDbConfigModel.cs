﻿namespace Models.Config.Mongo
{
    public class MongoDbConfigModel
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
        public string CollectionLog { get; set; } = null!;
        public string CollectionLogDevice { get; set; } = null!;
    }
}