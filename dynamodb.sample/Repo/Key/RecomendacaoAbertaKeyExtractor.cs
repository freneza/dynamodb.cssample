using Amazon.DynamoDBv2.DocumentModel;
using dynamodb.sample.Domain;
using System.Collections.Generic;

namespace dynamodb.sample.Repo.Key
{
    public class RecomendacaoAbertaKeyExtractor : IKeyExtractor<RecomendacaoAberta>
    {
        public Dictionary<string, DynamoDBEntry> ToDictionary(RecomendacaoAberta obj)
        {
            var dic = new Dictionary<string, DynamoDBEntry>();
            dic.Add("carteira", obj.Carteira);
            dic.Add("ticker",obj.Ticker);
            return dic;
        }
    }
}