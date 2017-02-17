using System;
using System.Collections.Generic;
using dynamodb.sample.Domain;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using dynamodb.sample.Repo;
using dynamodb.sample.Business.Converter;

// Add using statements to access AWS SDK for .NET services. 
// Both the Service and its Model namespace need to be added 
// in order to gain access to a service. For example, to access
// the EC2 service, add:
// using Amazon.EC2;
// using Amazon.EC2.Model;

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

            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            Table recomendacao_aberta_table = Table.LoadTable(client, "recomendacao_carteira");
            var key = new Dictionary<string, DynamoDBEntry>();
            key.Add("carteira", carteira);
            key.Add("ticker", ticker);
            Document recomendacao_aberta_doc = recomendacao_aberta_table.GetItem(key);

            var entrada = recomendacao_aberta_doc["entrada"].AsDouble();
            var resultado = Math.Round(((saida - entrada) / entrada) * 100, 2);
            var qtdd = recomendacao_aberta_doc["Qtdd"].AsInt();
            var data_entrada = recomendacao_aberta_doc["data"].AsInt();

            Table recomendacao_fechada_table = Table.LoadTable(client, "recomendacao_fechada");
            Document closedItem = new Document();
            closedItem.Add("ticker", ticker);
            closedItem.Add("data_fechamento", data_fechamento);
            closedItem.Add("carteira", carteira);
            closedItem.Add("entrada", entrada);
            closedItem.Add("saida", saida);
            closedItem.Add("resultado", resultado);
            closedItem.Add("qtdd", qtdd);
            closedItem.Add("data_entrada", data_entrada);

            recomendacao_fechada_table.PutItem(closedItem);

            // risco de problema de incluir e não excluir...

            recomendacao_aberta_table.DeleteItem(recomendacao_aberta_doc);

            Console.WriteLine("Recomendação encerrada!");
            //Print(closedItem.ToAttributeMap());
        }
        private static void BuscarAcoes()
        {
            Console.WriteLine("Digite o setor:");
            var entrada = Console.ReadLine();
            var lista = new AcaoRepo().Search(new AcaoSearchFilters { Setor = entrada });
            foreach (var acao in lista)
            {
                Print(acao);
            }
        }
        private static void VerAcao()
        {
            Console.WriteLine("Digite o ticker:");
            var ticker = Console.ReadLine();
            var acao = new AcaoRepo().Get(ticker);
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


    public class AcaoSearchFilters
    {
        public string Setor { get; set; }
    }

}