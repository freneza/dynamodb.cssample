using InvestmentControl.Domain;
using InvestmentControl.Repo.Converter;
using InvestmentControl.Repo.Key;

namespace InvestmentControl.Repo
{
    public class RecomendacaoAbertaRepo : GenericRepo<RecomendacaoAberta>
    {
        public RecomendacaoAbertaRepo() : base("recomendacao_carteira")
        {
            this.converter = new RecomendacaoAbertaConverter();
            this.key = new RecomendacaoAbertaKeyExtractor();
        }
    }
}