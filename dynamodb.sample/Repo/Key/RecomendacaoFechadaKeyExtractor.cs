using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using dynamodb.sample.Domain;

namespace dynamodb.sample.Repo.Key
{
    public class RecomendacaoFechadaKeyExtractor : IKeyExtractor<RecomendacaoFechada>
    {
        public Dictionary<string, DynamoDBEntry> ToDictionary(RecomendacaoFechada obj)
        {
            var dic = new Dictionary<string, DynamoDBEntry>();
            dic.Add("data_fechamento", obj.DataFechamento);
            dic.Add("ticker", obj.Ticker);
            return dic;
        }
    }
}
