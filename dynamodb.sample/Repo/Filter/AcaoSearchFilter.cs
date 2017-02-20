using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace dynamodb.sample.Repo.Filter
{
    public class AcaoSearchFilter : ISearchFilter
    {
        public string Setor { get; set; }

        public ScanRequest GetScanRequest()
        {
            return new ScanRequest
            {
                TableName = "acao",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":val", new AttributeValue { S = this.Setor } }
                },
                FilterExpression = "setor = :val"
            };
        }
    }
}
