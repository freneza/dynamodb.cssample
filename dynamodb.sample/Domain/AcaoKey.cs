using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace dynamodb.sample.Domain
{
    public class AcaoKey : IKey
    {
        public string Ticker { get; set; }
        public Dictionary<string, DynamoDBEntry> ToDictionary()
        {
            var dic = new Dictionary<string, DynamoDBEntry>();
            dic.Add("ticker", this.Ticker);
            return dic;
        }
    }
}
