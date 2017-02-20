using System.Collections.Generic;
using InvestmentControl.Domain;
using InvestmentControl.Repo;
using InvestmentControl.Repo.Filter;

namespace InvestmentControl.Business.Service
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
            return repo.Get(new Acao { Ticker = ticker }); // nao ficou legal. tem que saber que a chave é o ticker.
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
