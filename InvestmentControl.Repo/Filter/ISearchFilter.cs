using Amazon.DynamoDBv2.Model;

namespace InvestmentControl.Repo.Filter
{
    public interface ISearchFilter
    {
        ScanRequest GetScanRequest();
    }
}
