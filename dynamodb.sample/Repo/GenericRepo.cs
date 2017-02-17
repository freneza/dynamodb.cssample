using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using dynamodb.sample.Business.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dynamodb.sample.Repo
{
    public class GenericRepo<T>
    {
        public AmazonDynamoDBClient client { get; set; }
        private Table table { get; set; }
        public string table_name { get; set; }
        public IConverter<T> converter { get; set; }


        public GenericRepo(string table_name)
        {
            client = new AmazonDynamoDBClient();
            this.table_name = table_name;
            table = Table.LoadTable(client, this.table_name);
        }
        public void Add(T obj)
        {
            table.PutItem(converter.ConvertToDocument(obj));
        }
        public IEnumerable<T> List()
        {
            var request = new ScanRequest
            {
                TableName = table_name,
            };

            var response = client.Scan(request);
            return ReadScanResponse(response);
        }
        public T Get(RecomendacaoFechadaKey chave)
        {
            return converter.ConvertToDomain(table.GetItem(chave.ToDictionary()));
        }
        public IEnumerable<T> Search(AcaoSearchFilters filters)
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
        private IEnumerable<T> ReadScanResponse(ScanResponse response)
        {
            var lista = new List<T>();

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                lista.Add(converter.ConvertToDomain(item));
            }

            return lista;
        }
    }
}
