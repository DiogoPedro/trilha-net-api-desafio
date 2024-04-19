using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;


namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;
		private readonly TarefaService _tarefaService; // Adicione esta linha


		public TarefaController(OrganizadorContext context, TarefaService tarefaService)
		{
			_context = context;
			_tarefaService = tarefaService;
		}

		[HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
			// TODO: Buscar o Id no banco utilizando o EF
			var retTarefa = await _tarefaService.ObterPorId(id);
			// TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            if (retTarefa == null)
				return NotFound();
			// caso contrário retornar OK com a tarefa encontrada
			return Ok(retTarefa);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefas = await _tarefaService.ObterTodos();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = await _tarefaService.ObterPorTitulo(titulo);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
			var tarefa = await _tarefaService.ObterPorData(data);

			return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
        {
			// TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
			// Dica: Usar como exemplo o endpoint ObterPorData
			var tarefa = await _tarefaService.ObterPorStatus(status);
			return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            await _tarefaService.Criar(tarefa);

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAsync(int id, Tarefa tarefa)
        {
            var tarefaBanco = await _tarefaService.ObterPorId(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            var tarefaAtualizada = await _tarefaService.Atualizar(id, tarefa);
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            return Ok(tarefaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

			// TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
			await _tarefaService.Deletar(id);
            return NoContent();
        }
    }
}
