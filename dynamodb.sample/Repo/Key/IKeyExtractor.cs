using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace dynamodb.sample.Repo.Key
{
    public interface IKeyExtractor<T>
    {
        Dictionary<string, DynamoDBEntry> ToDictionary(T obj);
    }
}
