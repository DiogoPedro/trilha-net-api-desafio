using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {
            
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        //CRUD - Only Database

        public void Criar(Tarefa tarefa)
		{
			Tarefas.Add(tarefa);
			SaveChanges();
		}

        public void Atualizar(Tarefa tarefa)
        {
            Tarefas.Update(tarefa);
            SaveChanges();
        }

        public void Deletar(Tarefa tarefa)
		{
			Tarefas.Remove(tarefa);
			SaveChanges();
		}

    }
}