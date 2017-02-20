using dynamodb.sample.Business.Filter;
using dynamodb.sample.Domain;
using dynamodb.sample.Repo;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Service
{
    public class AcaoService
    {
        public IEnumerable<Acao> Search(string setor)
        {
            return new AcaoRepo().Search(new AcaoSearchFilter { Setor = setor });
        }

        public Acao Get(string ticker)
        {
            return new AcaoRepo().Get(new AcaoKey { Ticker = ticker });
        }

        public IEnumerable<Acao> List()
        {
            return new AcaoRepo().List();
        }
        public void Add(Acao acao)
        {
            new AcaoRepo().Add(acao);
        }
    }
}
