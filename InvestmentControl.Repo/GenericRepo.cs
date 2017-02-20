using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using InvestmentControl.Repo.Converter;
using InvestmentControl.Repo.Key;
using InvestmentControl.Repo.Filter;

namespace InvestmentControl.Repo
{
    public class GenericRepo<T>
    {
        #region Properties
        public AmazonDynamoDBClient client { get; set; }
        private Table table { get; set; }
        public string table_name { get; set; }
        public IConverter<T> converter { get; set; }
        public IKeyExtractor<T> key { get; set; }
        #endregion

        #region Constructors
        public GenericRepo(string table_name)
        {
            client = new AmazonDynamoDBClient();
            this.table_name = table_name;
            table = Table.LoadTable(client, this.table_name);
        }
        #endregion

        #region Methods
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
        public T Get(T obj)
        {
            return converter.ConvertToDomain(table.GetItem(key.ToDictionary(obj)));
        }
        public void Delete(T obj)
        {
            table.DeleteItem(key.ToDictionary(obj));
        }
        public IEnumerable<T> Search(ISearchFilter filters)
        {
            var response = client.Scan(filters.GetScanRequest());

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
        #endregion
    }
}
