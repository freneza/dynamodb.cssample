using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace InvestmentControl.Repo.Key
{
    public interface IKeyExtractor<T>
    {
        Dictionary<string, DynamoDBEntry> ToDictionary(T obj);
    }
}
