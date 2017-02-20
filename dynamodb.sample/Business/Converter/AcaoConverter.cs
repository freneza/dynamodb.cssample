using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamodb.sample.Domain;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Converter
{
    public class AcaoConverter : IConverter<Acao>
    {
        public static Acao ConvertToAcao(string s)
        {
            var dados = s.Split(' ');

            var acao = new Acao();
            acao.Ticker = dados[0];
            if (dados.Length >= 2) acao.Nome = dados[1];
            if (dados.Length >= 3) acao.Setor = dados[2];

            return acao;
        }
        public Document ConvertToDocument(Acao acao)
        {
            var doc = new Document();
            doc.Add("ticker", acao.Ticker);
            if (!string.IsNullOrEmpty(acao.Nome)) doc.Add("nome", acao.Nome);
            if (!string.IsNullOrEmpty(acao.Setor)) doc.Add("setor", acao.Setor);
            return doc;
        }
        public Acao ConvertToDomain(Dictionary<string, AttributeValue> item)
        {
            return new Acao()
            {
                Ticker = item["ticker"].S,
                Nome = item.ContainsKey("nome") ? item["nome"].S : string.Empty,
                Setor = item.ContainsKey("setor") ? item["setor"].S : string.Empty
            };
        }
        public Acao ConvertToDomain(Document doc)
        {
            return ConvertToDomain(doc.ToAttributeMap());
        }
    }
}
