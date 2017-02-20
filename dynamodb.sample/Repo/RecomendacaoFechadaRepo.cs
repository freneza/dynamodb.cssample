using dynamodb.sample.Business.Converter;
using dynamodb.sample.Domain;

namespace dynamodb.sample.Repo
{
    public class RecomendacaoFechadaRepo : GenericRepo<RecomendacaoFechada>
    {
        public RecomendacaoFechadaRepo() : base("recomendacao_fechada")
        {
            this.converter = new RecomendacaoFechadaConverter();
        }
    }
}
