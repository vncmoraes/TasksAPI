using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;
using TasksAPI.Interfaces;
using Task = TasksAPI.Models.Task;

namespace TasksAPI.Controllers;

[EnableCors(origins: "*", headers: "*", methods: "*")]
[Route("tasks")]
[ApiController]
public class TaskController(IRepository<Task> repository) : ControllerBase
{
    private readonly IRepository<Task> _repository = repository;

    [HttpPost]
    public IActionResult AddTask(Task task)
    {
        if(task.Title == null || task.Description == null)
        {
            // http 400 - requisição incorreta
            return BadRequest();
        }

        Task? newTask = _repository.Add(task);

        if(newTask == null)
        {
            // http 500 - exceção interna
            return Problem(); 
        }
        else
        {
            // http 200 - sucesso, retorna Task criada
            return Ok(newTask); 
        }
    }


    [HttpGet]
    public IActionResult GetAllTasks()
    {
        List<Task>? allTasks = _repository.GetAll();

        if(allTasks == null)
        {
            // http 500 - exceção interna
            return Problem();
        }
        else
        {
            // http 200 - sucesso, retorna todas as Tasks
            return Ok(allTasks);
        }
    }


    [HttpGet("{id:int}")]
    public IActionResult GetTaskById(int id)
    {   
        List<Task>? task = _repository.GetById(id);

        if(task == null)
        {
            // http 500 - exceção interna
            return Problem();
        }
        else
        {
            // http 200 - sucesso, retorna Task ou vazio para ID inexistente
            return Ok(task); 
        }
    }


    [HttpPut("{id:int}")]
    public IActionResult UpdateTask(int id, Task task)
    {
        List<Task>? taskUpdated = _repository.Update(id, task);

        if(task.Title == null || task.Description == null || task.IsCompleted == null)
        {
            // http 400 - requisição incorreta, campos faltando
            return BadRequest();
        }
        else if(taskUpdated == null)
        {
            // http 500 - exceção interna
            return Problem();
        }
        else
        {
            // http 200 - sucesso, retorna Task atualizada ou vazio para ID inexistente
            return Ok(taskUpdated);
        }

    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteTask(int id)
    {
        bool? taskDeleted = _repository.Delete(id);

        if(taskDeleted == null)
        {
            // http 500 - exceção interna
            return Problem();
        }
        else
        {
            // http 200 - sucesso, retorna status da ação
            return Ok(taskDeleted);
        }
    }
}
