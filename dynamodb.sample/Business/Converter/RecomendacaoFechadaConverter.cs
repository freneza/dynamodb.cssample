using dynamodb.sample.Domain;
using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace dynamodb.sample.Business.Converter
{
    public class RecomendacaoFechadaConverter : DocumentConverter<RecomendacaoFechada>, IConverter<RecomendacaoFechada>
    {
        public Document ConvertToDocument(RecomendacaoFechada recomendacao)
        {
            var doc = new Document();
            doc.Add("carteira", recomendacao.Carteira);
            doc.Add("ticker", recomendacao.Ticker);
            if (recomendacao.Qtdd.HasValue) doc.Add("qtdd", recomendacao.Qtdd);
            doc.Add("resultado", recomendacao.Resultado);
            doc.Add("saida", recomendacao.Saida);
            doc.Add("data_fechamento", recomendacao.DataFechamento);
            doc.Add("data_entrada", recomendacao.DataEntrada);
            doc.Add("entrada", recomendacao.Entrada);
            return doc;
        }
        public override RecomendacaoFechada ConvertToDomain(Dictionary<string, AttributeValue> item)
        {
            return new RecomendacaoFechada()
            {
                Ticker = item["ticker"].S,
                Carteira = item["carteira"].S,
                DataFechamento = Int32.Parse(item["data_fechamento"].N),
                DataEntrada = Int32.Parse(item["data_entrada"].N),
                Entrada = Double.Parse(item["entrada"].N),
                Saida = Double.Parse(item["saida"].N),
                Resultado = Double.Parse(item["resultado"].N),
                Qtdd = item.ContainsKey("Qtdd") ? Int32.Parse(item["nome"].N) : new Int32?()
            };
        }
    }
}
