using dynamodb.sample.Repo.Converter;
using dynamodb.sample.Domain;
using dynamodb.sample.Repo.Key;

namespace dynamodb.sample.Repo
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