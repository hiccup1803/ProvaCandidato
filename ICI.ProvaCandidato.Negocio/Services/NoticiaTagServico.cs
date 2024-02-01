using ICI.ProvaCandidato.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICI.ProvaCandidato.Negocio.Services
{
    public class NoticiaTagServico
    {
        private ApplicationDbContext _context;
        public NoticiaTagServico(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task PesquisarNoticiaTagAsync(int idTag)
        {
            var noticiaTagDb = await _context.NoticiasTags.Where(t => t.TagId == idTag).FirstOrDefaultAsync();

            if (noticiaTagDb != null) throw new Exception("A Tag não pode ser excluida, pois tem um vinculo com a uma ou mais notícias.");
        }
    }
}
