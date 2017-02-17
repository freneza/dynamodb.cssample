using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace dynamodb.sample.Domain
{
    public class RecomendacaoAbertaKey
    {
        public string Carteira { get; set; }
        public string Ticker { get; set; }
        public Dictionary<string, DynamoDBEntry> ToDictionary()
        {
            var dic = new Dictionary<string, DynamoDBEntry>();
            dic.Add("carteira", this.Carteira);
            dic.Add("ticker",this.Ticker);
            return dic;
        }
    }
}