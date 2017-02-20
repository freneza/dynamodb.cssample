using Amazon.DynamoDBv2.Model;

namespace dynamodb.sample.Business.Filter
{
    public interface ISearchFilter
    {
        ScanRequest GetScanRequest();
    }
}
