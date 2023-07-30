﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace vlist.Helpers
{
    public class InitializationHelper
    {
        public const string ENV_PREFIX_FULL = "VLIST__";
        public string? DB_HOST { get; set; }
        public string? DB_USERNAME { get; set; }
        public string? DB_PASSWORD { get; set; }
        public string? DB_DATABASE_NAME { get; set; }
        public string? DB_COLLECTION_NAME { get; set; }
        public string? FRONTEND_URL { get; set; }
        public List<string> Errors { get; set; }

        public InitializationHelper(
            string? _DB_HOST,
            string? _DB_USERNAME,
            string? _DB_PASSWORD,
            string? _DB_DATABASE_NAME,
            string? _DB_COLLECTION_NAME,
            string? _FRONTEND_URL
            ) 
        {
            DB_HOST = _DB_HOST;
            DB_USERNAME = _DB_USERNAME;
            DB_PASSWORD = _DB_PASSWORD;
            DB_DATABASE_NAME = _DB_DATABASE_NAME;
            DB_COLLECTION_NAME = _DB_COLLECTION_NAME;
            FRONTEND_URL = _FRONTEND_URL;
            Errors = new List<string>();
        }

        public bool IsValidEnvironmentVariables()
        {
            if (String.IsNullOrWhiteSpace(DB_HOST))
                Errors.Add($"{ENV_PREFIX_FULL}{nameof(DB_HOST)} cannot be null");
            if (String.IsNullOrWhiteSpace(DB_USERNAME))
                Errors.Add($"{ENV_PREFIX_FULL}{nameof(DB_USERNAME)} cannot be null");
            if (String.IsNullOrWhiteSpace(DB_PASSWORD))
                Errors.Add($"{ENV_PREFIX_FULL}{nameof(DB_PASSWORD)} cannot be null");
            if (String.IsNullOrWhiteSpace(DB_DATABASE_NAME))
                Errors.Add($"{ENV_PREFIX_FULL}{nameof(DB_DATABASE_NAME)} cannot be null");
            if (String.IsNullOrWhiteSpace(DB_COLLECTION_NAME))
                Errors.Add($"{ENV_PREFIX_FULL}{nameof(DB_COLLECTION_NAME)} cannot be null");
            if (String.IsNullOrWhiteSpace(FRONTEND_URL))
                Errors.Add($"{ENV_PREFIX_FULL}{nameof(FRONTEND_URL)} cannot be null");

            string connectionString = ConnectionStringBuilder();
            CheckMongoDBConnection(connectionString);

            return Math.Abs(Errors.Count) == 0;
        }

        public string ConnectionStringBuilder()
        {
            return $"mongodb://{DB_USERNAME}:{DB_PASSWORD}@{DB_HOST}";
        }

        public void CheckMongoDBConnection(string connectionString)
        {
            try
            {
                var client = new MongoClient(connectionString);
                client.GetDatabase(DB_DATABASE_NAME).RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("info: Pinged MongoDb. Successfully connected!");
            }
            catch (Exception ex)
            {
                Errors.Add($"Could not connect to MongoDb. Error: {ex.Message}");
            }
        }
    }
}
