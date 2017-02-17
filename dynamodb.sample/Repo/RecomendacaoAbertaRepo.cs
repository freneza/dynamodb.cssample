using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamodb.sample.Business.Converter;
using dynamodb.sample.Domain;
using System.Collections.Generic;

namespace dynamodb.sample.Repo
{
    public class RecomendacaoAbertaRepo
    {
        AmazonDynamoDBClient client;
        Table table;
        public RecomendacaoAbertaRepo()
        {
            client = new AmazonDynamoDBClient();
            table = Table.LoadTable(client, "recomendacao_carteira");
        }
        public void Add(RecomendacaoAberta recomendacao)
        {
            table.PutItem(RecomendacaoAbertaConverter.ConvertToDocument(recomendacao));
        }
        public IEnumerable<RecomendacaoAberta> List()
        {
            var request = new ScanRequest
            {
                TableName = "recomendacao_carteira",
            };

            var response = client.Scan(request);
            return ReadScanResponse(response);
        }
        public RecomendacaoAberta Get(RecomendacaoAbertaKey chave)
        {
            return RecomendacaoAbertaConverter.ConvertToRecomendacaoAberta(table.GetItem(chave.ToDictionary()));
        }
        public IEnumerable<RecomendacaoAberta> Search(AcaoSearchFilters filters)
        {
            var request = new ScanRequest
            {
                TableName = "recomendacao_carteira",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":val", new AttributeValue { S = filters.Setor } }
                },
                FilterExpression = "setor = :val"
            };

            var response = client.Scan(request);

            return ReadScanResponse(response);
        }
        private IEnumerable<RecomendacaoAberta> ReadScanResponse(ScanResponse response)
        {
            var lista = new List<RecomendacaoAberta>();

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                lista.Add(RecomendacaoAbertaConverter.ConvertToRecomendacaoAberta(item));
            }

            return lista;
        }
    }
}