using dynamodb.sample.Repo.Filter;
using dynamodb.sample.Domain;
using dynamodb.sample.Repo;
using System.Collections.Generic;

namespace dynamodb.sample.Business.Service
{
    public class AcaoService
    {
        public AcaoRepo repo { get; set; } = new AcaoRepo();

        public IEnumerable<Acao> Search(string setor)
        {
            return repo.Search(new AcaoSearchFilter { Setor = setor });
        }

        public Acao Get(string ticker)
        {
            return repo.Get(new AcaoKey { Ticker = ticker });
        }

        public IEnumerable<Acao> List()
        {
            return repo.List();
        }
        public void Add(Acao acao)
        {
            repo.Add(acao);
        }
    }
}
