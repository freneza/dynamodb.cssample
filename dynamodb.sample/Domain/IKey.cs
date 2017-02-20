using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace dynamodb.sample.Domain
{
    public interface IKey
    {
        Dictionary<string, DynamoDBEntry> ToDictionary();
    }
}
