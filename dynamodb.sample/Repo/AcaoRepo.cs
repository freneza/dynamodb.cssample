using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamodb.sample.Business.Converter;
using dynamodb.sample.Domain;
using System.Collections.Generic;

namespace dynamodb.sample.Repo
{
    public class AcaoRepo
    {
        AmazonDynamoDBClient client;
        Table table;
        public AcaoRepo()
        {
            client = new AmazonDynamoDBClient();
            table = Table.LoadTable(client, "acao");
        }
        public void Add(Acao acao)
        {
            table.PutItem(AcaoConverter.ConvertToDocument(acao));
        }
        public IEnumerable<Acao> List()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            var request = new ScanRequest
            {
                TableName = "acao",
            };

            var response = client.Scan(request);
            return ReadScanResponse(response);
        }
        public Acao Get(string ticker)
        {
            return AcaoConverter.ConvertToAcao(table.GetItem(ticker));
        }
        public IEnumerable<Acao> Search(AcaoSearchFilters filters)
        {
            var request = new ScanRequest
            {
                TableName = "acao",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":val", new AttributeValue { S = filters.Setor } }
                },
                FilterExpression = "setor = :val"
            };

            var response = client.Scan(request);
            return ReadScanResponse(response);
        }
        private IEnumerable<Acao> ReadScanResponse(ScanResponse response)
        {
            var lista = new List<Acao>();

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                lista.Add(AcaoConverter.ConvertToAcao(item));
            }

            return lista;
        }
    }
}
