using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICI.ProvaCandidato.Dados;
using ICI.ProvaCandidato.Dados.Models;
using ICI.ProvaCandidato.Negocio.Services;

namespace ICI.ProvaCandidato.Web.Controllers
{
    public class NoticiasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NoticiaServico _noticiaServico;

        public NoticiasController(ApplicationDbContext context)
        {
            _context = context;
            _noticiaServico = new NoticiaServico(_context);
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Noticias.Include(n => n.UsuarioFk);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _noticiaServico.PesquisarDetalhesNoticia(id);

            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            ViewBag.Tags = new SelectList(_context.Tags, "Id", "Descricao");

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Text,UsuarioId")] Noticia noticia, string[] tag)
        {
            if (ModelState.IsValid)
            {
                await _noticiaServico.AdicionarNoticiaComTagsAsync(noticia, tag);
                _context.Add(noticia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _noticiaServico.PesquisarNoticia(id);
            
            if (noticia == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Text,UsuarioId")] Noticia noticia)
        {
            if (id != noticia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _noticiaServico.EditNoticia(noticia);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome", noticia.UsuarioId);
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _noticiaServico.PesquisarDeleteNoticia(id);

            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _noticiaServico.DeletarNoticia(id);

            return RedirectToAction(nameof(Index));
        }        
    }
}
