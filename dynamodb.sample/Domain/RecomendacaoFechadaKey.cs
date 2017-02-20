using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using System;

namespace dynamodb.sample.Domain
{
    public class RecomendacaoFechadaKey : IKey
    {
        public Double DataFechamento { get; set; }
        public string Ticker { get; set; }
        public Dictionary<string, DynamoDBEntry> ToDictionary()
        {
            var dic = new Dictionary<string, DynamoDBEntry>();
            dic.Add("data_fechamento", this.DataFechamento);
            dic.Add("ticker", this.Ticker);
            return dic;
        }
    }
}
