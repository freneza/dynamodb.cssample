using dynamodb.sample.Repo.Converter;
using dynamodb.sample.Domain;
using dynamodb.sample.Repo.Key;

namespace dynamodb.sample.Repo
{
    public class AcaoRepo : GenericRepo<Acao>
    {
        public AcaoRepo() : base("acao")
        {
            this.converter = new AcaoConverter();
            this.key = new AcaoKeyExtractor();
        }
    }
}
