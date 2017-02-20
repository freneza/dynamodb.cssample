using System;
using dynamodb.sample.Domain;
using dynamodb.sample.Repo;
using dynamodb.sample.Business.Converter;
using dynamodb.sample.Business.Filter;

namespace dynamodb.sample
{
    class Program
    {
        public static void Main(string[] args)
        {
            var option = 0;

            do
            {
                Console.WriteLine("Escolha uma opção: 0) sair 1) incluir ação 2) listar ações 3) ver ação 4) buscar ações 5) encerrar recomendação");
                option = Int32.Parse(Console.ReadLine());

                ExecutarOpcao(option);

            } while (option != 0);
        }
        private static void ExecutarOpcao(int option)
        {
            switch(option)
            {
                case 1:
                    IncluirAcao();
                    break;
                case 2:
                    ListarAcoes();
                    break;
                case 3:
                    VerAcao();
                    break;
                case 4:
                    BuscarAcoes();
                    break;
                case 5:
                    EncerrarRecomendacao();
                    break;
            }
        }
        private static void EncerrarRecomendacao()
        {
            Console.WriteLine("Digite o ticker:");
            var ticker = Console.ReadLine();
            Console.WriteLine("Digite a carteira:");
            var carteira = Console.ReadLine();
            Console.WriteLine("Digite a data do encerramento YYYYMMDD (default data atual):");
            var sFechamento = Console.ReadLine();
            var data_fechamento = string.IsNullOrEmpty(sFechamento) ? DateTime.Today.Year * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day : Int32.Parse(sFechamento);
            Console.WriteLine("Digite o valor de saída:");
            var saida = Double.Parse(Console.ReadLine());

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

            Console.WriteLine("Recomendação encerrada!");
        }
        private static void BuscarAcoes()
        {
            Console.WriteLine("Digite o setor:");
            var entrada = Console.ReadLine();
            var lista = new AcaoRepo().Search(new AcaoSearchFilter { Setor = entrada });
            foreach (var acao in lista)
            {
                Print(acao);
            }
        }
        private static void VerAcao()
        {
            Console.WriteLine("Digite o ticker:");
            var ticker = Console.ReadLine();
            var acao = new AcaoRepo().Get(new AcaoKey { Ticker = ticker });
            Print(acao);
        }
        private static void ListarAcoes()
        {
            Console.WriteLine("Não faz sentido listar tabela em NoSQL. Mas fica um exemplo do mesmo jeito.");
            var lista = new AcaoRepo().List();

            foreach (var acao in lista)
            {
                PrintSimples(acao);
            }
        }
        private static void Print(Acao acao)
        {
            Console.WriteLine();
            Console.WriteLine(nameof(acao.Ticker) + ": " + acao.Ticker);
            Console.WriteLine(nameof(acao.Nome) + ": " + acao.Nome);
            Console.WriteLine(nameof(acao.Setor) + ": " + acao.Setor);
        }
        private static void PrintSimples(Acao acao)
        {
            Console.WriteLine(acao.Ticker);
        }
        private static void IncluirAcao()
        {
            Console.WriteLine("Digite: ticker empresa setor");
            var entrada = Console.ReadLine();
            var acao = AcaoConverter.ConvertToAcao(entrada);
            new AcaoRepo().Add(acao);
        }
    }

}