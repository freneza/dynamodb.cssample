using Amazon.DynamoDBv2.Model;

namespace dynamodb.sample.Repo.Filter
{
    public interface ISearchFilter
    {
        ScanRequest GetScanRequest();
    }
}
