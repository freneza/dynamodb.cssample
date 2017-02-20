using dynamodb.sample.Domain;

namespace dynamodb.sample.Repo
{
    public class RecomendacaoAbertaRepo : GenericRepo<RecomendacaoAberta>
    {
        public RecomendacaoAbertaRepo() : base("recomendacao_carteira")
        {
        }
    }
}