using System;

namespace dynamodb.sample.Domain
{
    public class RecomendacaoFechada
    {
        public string Carteira { get; set; }
        public string Ticker { get; set; }
        public int? Qtdd { get; set; }
        public Double DataFechamento { get; set; }
        public Double Saida { get; set; }
        public Double Resultado { get; set; }
        public int DataEntrada { get; set; }
        public Double Entrada { get; set; }
    }
}
