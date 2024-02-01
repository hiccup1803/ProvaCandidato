using ICI.ProvaCandidato.Dados;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICI.ProvaCandidato.Negocio.Services
{
    public class TagServico
    {
        private ApplicationDbContext _context;
        private NoticiaTagServico _noticiaTagServico;

        
        public TagServico(ApplicationDbContext context)
        {
            _context = context;
            _noticiaTagServico = new NoticiaTagServico(context);
        }

        public async Task PesquisarTagAsync(int idTag)
        {
            var tagDb = await _context.Tags.Where(t => t.Id == idTag ).FirstOrDefaultAsync();

            if (tagDb == null) throw new Exception("Tag não encontrada");

            await _noticiaTagServico.PesquisarNoticiaTagAsync(tagDb.Id);
        }        
    }
}
