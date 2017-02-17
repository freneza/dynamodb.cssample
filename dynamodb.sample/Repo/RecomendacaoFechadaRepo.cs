using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamodb.sample.Business.Converter;
using dynamodb.sample.Domain;
using System.Collections.Generic;

namespace dynamodb.sample.Repo
{
    public class RecomendacaoFechadaRepo
    {
        public AmazonDynamoDBClient client;
        private Table table;
        public string table_name;
        public IConverter<RecomendacaoFechada> converter;
        public RecomendacaoFechadaRepo(string table_name = "recomendacao_fechada")
        {
            client = new AmazonDynamoDBClient();
            this.table_name = table_name;
            table = Table.LoadTable(client, this.table_name);
            converter = new RecomendacaoFechadaConverter();
        }
        public void Add(RecomendacaoFechada recomendacao)
        {
            table.PutItem(converter.ConvertToDocument(recomendacao));
        }
        public IEnumerable<RecomendacaoFechada> List()
        {
            var request = new ScanRequest
            {
                TableName = table_name,
            };

            var response = client.Scan(request);
            return ReadScanResponse(response);
        }
        public RecomendacaoFechada Get(RecomendacaoFechadaKey chave)
        {
            return converter.ConvertToDomain(table.GetItem(chave.ToDictionary()));
        }
        public IEnumerable<RecomendacaoFechada> Search(AcaoSearchFilters filters)
        {
            var request = new ScanRequest
            {
                TableName = table_name,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":val", new AttributeValue { S = filters.Setor } }
                },
                FilterExpression = "setor = :val"
            };

            var response = client.Scan(request);

            return ReadScanResponse(response);
        }
        private IEnumerable<RecomendacaoFechada> ReadScanResponse(ScanResponse response)
        {
            var lista = new List<RecomendacaoFechada>();

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                lista.Add(converter.ConvertToDomain(item));
            }

            return lista;
        }
    }
}
