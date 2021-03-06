﻿using System;
using dynamodb.sample.Domain;
using dynamodb.sample.Business.Service;

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

            new EncerrarRecomendacaoService().Execute(carteira, ticker, data_fechamento, saida);

            Console.WriteLine("Recomendação encerrada!");
        }
        private static void BuscarAcoes()
        {
            Console.WriteLine("Digite o setor:");
            var entrada = Console.ReadLine();
            var lista = new AcaoService().Search(entrada);
            foreach (var acao in lista)
            {
                Print(acao);
            }
        }
        private static void VerAcao()
        {
            Console.WriteLine("Digite o ticker:");
            var ticker = Console.ReadLine();
            var acao = new AcaoService().Get(ticker);
            Print(acao);
        }
        private static void ListarAcoes()
        {
            Console.WriteLine("Não faz sentido listar tabela em NoSQL. Mas fica um exemplo do mesmo jeito.");
            var lista = new AcaoService().List();

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
            var acao = ConsoleAcaoConverter.ConvertToAcao(entrada);
            new AcaoService().Add(acao);
        }
    }

    class ConsoleAcaoConverter
    {
        public static Acao ConvertToAcao(string s)
        {
            var dados = s.Split(' ');

            var acao = new Acao();
            acao.Ticker = dados[0];
            if (dados.Length >= 2) acao.Nome = dados[1];
            if (dados.Length >= 3) acao.Setor = dados[2];

            return acao;
        }
    }

}