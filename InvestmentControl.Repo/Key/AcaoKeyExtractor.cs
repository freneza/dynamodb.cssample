using Amazon.DynamoDBv2.DocumentModel;
using InvestmentControl.Domain;
using System.Collections.Generic;

namespace InvestmentControl.Repo.Key
{
    public class AcaoKeyExtractor : IKeyExtractor<Acao>
    {
        public Dictionary<string, DynamoDBEntry> ToDictionary(Acao obj)
        {
            var dic = new Dictionary<string, DynamoDBEntry>();
            dic.Add("ticker", obj.Ticker);
            return dic;
        }
    }
}
