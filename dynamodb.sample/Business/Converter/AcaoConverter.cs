using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamodb.sample.Domain;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Converter
{
    public class AcaoConverter : DocumentConverter<Acao>, IConverter<Acao>
    {
        public Document ConvertToDocument(Acao acao)
        {
            var doc = new Document();
            doc.Add("ticker", acao.Ticker);
            if (!string.IsNullOrEmpty(acao.Nome)) doc.Add("nome", acao.Nome);
            if (!string.IsNullOrEmpty(acao.Setor)) doc.Add("setor", acao.Setor);
            return doc;
        }
        public override Acao ConvertToDomain(Dictionary<string, AttributeValue> item)
        {
            return new Acao()
            {
                Ticker = item["ticker"].S,
                Nome = item.ContainsKey("nome") ? item["nome"].S : string.Empty,
                Setor = item.ContainsKey("setor") ? item["setor"].S : string.Empty
            };
        }
    }
}
