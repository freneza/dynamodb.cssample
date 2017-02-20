using Amazon.DynamoDBv2.DocumentModel;
using System;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Globalization;
using InvestmentControl.Domain;

namespace InvestmentControl.Repo.Converter
{
    public class RecomendacaoAbertaConverter : DocumentConverter<RecomendacaoAberta>, IConverter<RecomendacaoAberta>
    {
        public Document ConvertToDocument(RecomendacaoAberta recomendacao)
        {
            var doc = new Document();
            doc.Add("carteira", recomendacao.Carteira);
            doc.Add("ticker", recomendacao.Ticker);
            if (recomendacao.Qtdd.HasValue) doc.Add("Qtdd", recomendacao.Qtdd);
            if (recomendacao.Stop.HasValue) doc.Add("Stop", recomendacao.Stop);
            if (recomendacao.Alvo1p1.HasValue) doc.Add("alvoI", recomendacao.Alvo1p1);
            if (recomendacao.Alvo1p15.HasValue) doc.Add("alvoIe5", recomendacao.Alvo1p15);
            doc.Add("data", recomendacao.Data);
            doc.Add("entrada", recomendacao.Entrada);
            if (recomendacao.Risco.HasValue) doc.Add("Risco", recomendacao.Risco);
            return doc;
        }
        public override RecomendacaoAberta ConvertToDomain(Dictionary<string, AttributeValue> item)
        {
            CultureInfo culture = new CultureInfo("en"); // cultura no DynamoDb...

            return new RecomendacaoAberta()
            {
                Ticker = item["ticker"].S,
                Carteira = item["carteira"].S,
                Data = Int32.Parse(item["data"].N),
                Entrada = Double.Parse(item["entrada"].N, culture),
                Qtdd = item.ContainsKey("Qtdd") ? Int32.Parse(item["Qtdd"].N) : new Int32?(),
                Stop = item.ContainsKey("Stop") ? Double.Parse(item["Stop"].N, culture) : new Double?(),
                Alvo1p1 = item.ContainsKey("alvoI") ? Double.Parse(item["alvoI"].N, culture) : new Double?(),
                Alvo1p15 = item.ContainsKey("alvoIe5") ? Double.Parse(item["alvoIe5"].N, culture) : new Double?(),
                Risco = item.ContainsKey("Risco") ? Double.Parse(item["Risco"].N, culture) : new Double?()
            };
        }
    }
}
