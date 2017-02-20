using dynamodb.sample.Business.Converter;
using dynamodb.sample.Domain;

namespace dynamodb.sample.Repo
{
    public class AcaoRepo : GenericRepo<Acao>
    {
        public AcaoRepo() : base("acao")
        {
            this.converter = new ConsoleAcaoConverter();
        }
    }
}
