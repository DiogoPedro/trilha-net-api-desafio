using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
	public class TarefaService
	{
		private readonly OrganizadorContext _context;

		public TarefaService(OrganizadorContext context)
		{
			_context = context;
		}

		public async Task<Tarefa> ObterPorId(int id)
		{
			return await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Tarefa>> ObterTodos()
		{
			return await _context.Tarefas.ToListAsync();
		}

		public async Task<List<Tarefa>> ObterPorTitulo(string titulo)
		{
			return await _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToListAsync();
		}

		public async Task<List<Tarefa>> ObterPorData(DateTime data)
		{
			return await _context.Tarefas.Where(x => x.Data.Date == data.Date).ToListAsync();
		}

		public async Task<List<Tarefa>> ObterPorStatus(EnumStatusTarefa status)
		{
			return await _context.Tarefas.Where(x => x.Status == status).ToListAsync();
		}

		public async Task<Tarefa> Atualizar(int id, Tarefa tarefa)
		{
			var tarefaAntiga = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
			_context.Entry(tarefaAntiga).CurrentValues.SetValues(tarefa);
			await _context.SaveChangesAsync();

			var ret = await ObterPorId(id);
			return ret;
		}

		public async Task Criar(Tarefa tarefa)
		{
			_context.Tarefas.Add(tarefa);
			await _context.SaveChangesAsync();
		}

		public async Task Deletar(int id)
		{
			var tarefa = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
			_context.Tarefas.Remove(tarefa);
			await _context.SaveChangesAsync();
		}
	}
}
