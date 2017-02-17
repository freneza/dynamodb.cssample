using Amazon.DynamoDBv2.DocumentModel;
using dynamodb.sample.Domain;
using System;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Converter
{
    public class RecomendacaoAbertaConverter
    {
        public static Document ConvertToDocument(RecomendacaoAberta recomendacao)
        {
            var doc = new Document();
            doc.Add("carteira", recomendacao.Carteira);
            doc.Add("ticker", recomendacao.Ticker);
            if(recomendacao.Qtdd.HasValue) doc.Add("Qtdd", recomendacao.Qtdd);
            if (recomendacao.Stop.HasValue) doc.Add("Stop", recomendacao.Stop);
            if (recomendacao.Alvo1p1.HasValue) doc.Add("alvoI", recomendacao.Alvo1p1);
            if (recomendacao.Alvo1p15.HasValue) doc.Add("alvoIe5", recomendacao.Alvo1p15);
            doc.Add("data", recomendacao.Data);
            doc.Add("entrada", recomendacao.Entrada);
            if (recomendacao.Risco.HasValue) doc.Add("Risco", recomendacao.Risco);
            return doc;
        }

        public static RecomendacaoAberta ConvertToRecomendacaoAberta(Dictionary<string, AttributeValue> item)
        {
            return new RecomendacaoAberta()
            {
                Ticker = item["ticker"].S,
                Carteira = item["carteira"].S,
                Data = Int32.Parse(item["data"].N),
                Entrada = Double.Parse(item["entrada"].N),
                Qtdd = item.ContainsKey("Qtdd") ? Int32.Parse(item["nome"].N) : new Int32?(),
                Stop = item.ContainsKey("Stop") ? Double.Parse(item["setor"].N) : new Double?(),
                Alvo1p1 = item.ContainsKey("alvoI") ? Double.Parse(item["nome"].N) : new Double?(),
                Alvo1p15 = item.ContainsKey("alvoIe5") ? Double.Parse(item["setor"].N) : new Double?(),
                Risco = item.ContainsKey("Risco") ? Double.Parse(item["setor"].N) : new Double?()
            };
        }
        public static RecomendacaoAberta ConvertToRecomendacaoAberta(Document doc)
        {
            return ConvertToRecomendacaoAberta(doc.ToAttributeMap());
        }
    }
}
