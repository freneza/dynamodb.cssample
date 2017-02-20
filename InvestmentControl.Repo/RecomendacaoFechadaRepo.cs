using InvestmentControl.Repo.Key;
using InvestmentControl.Repo.Converter;
using InvestmentControl.Domain;

namespace InvestmentControl.Repo
{
    public class RecomendacaoFechadaRepo : GenericRepo<RecomendacaoFechada>
    {
        public RecomendacaoFechadaRepo() : base("recomendacao_fechada")
        {
            this.converter = new RecomendacaoFechadaConverter();
            this.key = new RecomendacaoFechadaKeyExtractor();
        }
    }
}
