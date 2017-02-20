using dynamodb.sample.Domain;
using dynamodb.sample.Repo;
using System;

namespace dynamodb.sample.Business.Service
{
    public class EncerrarRecomendacaoService
    {
        public RecomendacaoAbertaRepo repoA { get; set; } = new RecomendacaoAbertaRepo();
        public RecomendacaoFechadaRepo repoF { get; set; } = new RecomendacaoFechadaRepo();

        public void Execute(string carteira, string ticker, int data_fechamento, double saida)
        {
            var recomendacao_aberta = repoA.Get(new RecomendacaoAberta { Carteira = carteira, Ticker = ticker }); // precisa saber qual a chave...

            var recomendacao_fechada = new RecomendacaoFechada
            {
                Carteira = carteira,
                Ticker = ticker,
                DataEntrada = recomendacao_aberta.Data,
                DataFechamento = data_fechamento,
                Entrada = recomendacao_aberta.Entrada,
                Qtdd = recomendacao_aberta.Qtdd,
                Saida = saida,
                Resultado = Math.Round(((saida - recomendacao_aberta.Entrada) / recomendacao_aberta.Entrada) * 100, 2)
            };

            repoF.Add(recomendacao_fechada);
            repoA.Delete(recomendacao_aberta);
        }
    }
}
