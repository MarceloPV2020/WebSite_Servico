using Microsoft.EntityFrameworkCore;
using WebPrestadores.Context;
using WebPrestadores.Models;
using WebPrestadores.Repositories.Interfaces;
using WebPrestadores.ViewModels;

namespace WebPrestadores.Repositories
{
    public class PrestadorServicoRepository : IPrestadorServicoRepository
    {
        private readonly AppDbContext _context;
        public PrestadorServicoRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<PrestadorServico> Prestadores
        {
            get
            {
                var prestadorTemp = _context.PrestadorServico.Include(c => c.CategoriaServico).Include(x => x.Usuario);
                foreach (var item in prestadorTemp)
                {
                    item.ListaPrestadorServicoAvaliacao = _context.PrestadorServicoAvaliacao
                        .Include(x => x.UsuarioAvaliador)
                        .Where(x => x.PrestadorServicoId == item.Id)
                        .ToList();
                    item.ListaPrestadorServicoCidade = _context.PrestadorServicoCidade
                         .Include(x => x.Cidade)
                         .Where(x => x.PrestadorServicoId == item.Id)
                         .ToList();
                }

                return prestadorTemp;
            }
        }

        public PrestadorServico GetPrestadorById(int prestadorId)
        {
            return _context.PrestadorServico.FirstOrDefault(l => l.Id == prestadorId);
        }

        public PrestadorServicoListViewModel GetPrestadorServicoListViewModel(string aspNetUsersId, string categoria)
        {
            IEnumerable<PrestadorServico> prestadoresServico;
            string categoriaAtual = string.Empty;
            int idCidadeUsuario = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == aspNetUsersId)?.CidadeId ?? 0;
            if (string.IsNullOrEmpty(categoria))
            {
                var query = this.Prestadores;
                if (idCidadeUsuario > 0)
                {
                    query = query.Where(x => x.ListaPrestadorServicoCidade.Any(y => y.CidadeId == idCidadeUsuario));
                }

                prestadoresServico = query.OrderBy(l => l.CategoriaServico.Nome).ThenBy(x => x.Nome);
                categoriaAtual = "Todos os prestadores";
            }
            else
            {
                var query = this.Prestadores;
                if (idCidadeUsuario > 0)
                {
                    query = query.Where(x => x.ListaPrestadorServicoCidade.Any(y => y.CidadeId == idCidadeUsuario));
                }

                prestadoresServico = query
                         .Where(l => l.CategoriaServico.Nome.Equals(categoria))
                         .OrderBy(l => l.CategoriaServico.Nome).ThenBy(x => x.Nome);
                categoriaAtual = categoria;
            }

            return
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadoresServico,
                    CategoriaServicoAtual = categoriaAtual
                };
        }

        public PrestadorServicoListViewModel GetPrestadorServicoListViewModelBySearchNome(string aspNetUsersId, string searchNomeString)
        {
            IEnumerable<PrestadorServico> prestadores;
            string mensagem = string.Empty;
            int idCidadeUsuario = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == aspNetUsersId)?.CidadeId ?? 0;

            if (string.IsNullOrEmpty(searchNomeString))
            {
                var query = this.Prestadores;
                if (idCidadeUsuario > 0)
                {
                    query = query.Where(x => x.ListaPrestadorServicoCidade.Any(y => y.CidadeId == idCidadeUsuario));
                }

                prestadores = query.OrderBy(l => l.CategoriaServico.Nome).ThenBy(x => x.Nome);
                mensagem = "Todos os Prestadores";
            }
            else
            {
                var query = this.Prestadores;
                if (idCidadeUsuario > 0)
                {
                    query = query.Where(x => x.ListaPrestadorServicoCidade.Any(y => y.CidadeId == idCidadeUsuario));
                }

                prestadores = query.Where(p => p.Nome.ToLower().Contains(searchNomeString.ToLower())).OrderBy(l => l.CategoriaServico.Nome).ThenBy(x => x.Nome);
                if (prestadores.Any())
                    mensagem = "Prestadores";
                else
                    mensagem = "Nenhum prestador foi encontrado";
            }

            return
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadores,
                    CategoriaServicoAtual = mensagem
                };
        }

        public PrestadorServicoListViewModel GetPrestadorServicoListViewModelBySearchCategoria(string aspNetUsersId, string searchCategoriaString)
        {
            IEnumerable<PrestadorServico> prestadores;
            string mensagem = string.Empty;
            int idCidadeUsuario = _context.Usuario.FirstOrDefault(x => x.AspNetUsersId == aspNetUsersId)?.CidadeId ?? 0;

            if (string.IsNullOrEmpty(searchCategoriaString))
            {
                var query = this.Prestadores;
                if (idCidadeUsuario > 0)
                {
                    query = query.Where(x => x.ListaPrestadorServicoCidade.Any(y => y.CidadeId == idCidadeUsuario));
                }

                prestadores = query.OrderBy(l => l.CategoriaServico.Nome).ThenBy(x => x.Nome);
                mensagem = "Todos os Prestadores";
            }
            else
            {
                var query = this.Prestadores;
                if (idCidadeUsuario > 0)
                {
                    query = query.Where(x => x.ListaPrestadorServicoCidade.Any(y => y.CidadeId == idCidadeUsuario));
                }

                prestadores = query.Where(p => p.CategoriaServico.Nome.ToLower().Contains(searchCategoriaString.ToLower())).OrderBy(l => l.CategoriaServico.Nome).ThenBy(x => x.Nome);
                if (prestadores.Any())
                    mensagem = "Prestadores";
                else
                    mensagem = "Nenhum prestador foi encontrado";
            }

            return
                new PrestadorServicoListViewModel
                {
                    PrestadoresServico = prestadores,
                    CategoriaServicoAtual = mensagem
                };
        }
    }
}
