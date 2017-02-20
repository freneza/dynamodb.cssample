using dynamodb.sample.Domain;
using dynamodb.sample.Repo;
using System;

namespace dynamodb.sample.Business.Service
{
    public class EncerrarRecomendacaoService
    {
        public void Execute(string carteira, string ticker, int data_fechamento, double saida)
        {
            var rarepo = new RecomendacaoAbertaRepo();
            var recomendacao_aberta = rarepo.Get(new RecomendacaoAbertaKey { Carteira = carteira, Ticker = ticker });

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

            var rfrepo = new RecomendacaoFechadaRepo();
            rfrepo.Add(recomendacao_fechada);
            rarepo.Delete(recomendacao_aberta.Key);
        }
    }
}
