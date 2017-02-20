using dynamodb.sample.Repo.Converter;
using dynamodb.sample.Domain;
using dynamodb.sample.Repo.Key;

namespace dynamodb.sample.Repo
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
