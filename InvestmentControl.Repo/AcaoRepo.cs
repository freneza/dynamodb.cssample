using InvestmentControl.Domain;
using InvestmentControl.Repo.Converter;
using InvestmentControl.Repo.Key;

namespace InvestmentControl.Repo
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
