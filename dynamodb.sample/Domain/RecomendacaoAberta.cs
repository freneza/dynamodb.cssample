using System;

namespace dynamodb.sample.Domain
{
    public class RecomendacaoAberta
    {
        public string Carteira { get; set; }
        public string Ticker { get; set; }
        public int? Qtdd { get; set; }
        public Double? Stop { get; set; }
        public Double? Alvo1p1 { get; set; }
        public Double? Alvo1p15 { get; set; }
        public int Data { get; set; }
        public Double Entrada { get; set; }
        public Double? Risco { get; set; }
    }
}
