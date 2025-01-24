using Microsoft.Data.SqlClient;
using System.Data;
using TasksAPI.Interfaces;
using Task = TasksAPI.Models.Task;

namespace TasksAPI.Data;

public class TaskRepository : IRepository<Task>
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TaskRepository> _logger;

    private readonly SqlConnection _conn;

    public TaskRepository(ILogger<TaskRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _conn = new SqlConnection(_configuration.GetConnectionString("conn"));
    }

    public Task? Add(Task task)
    {
        Task? newTask = null;

        try
        {
            using(SqlCommand cmd = new("usp_AddTask", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = task.Title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = task.Description;
                _conn.Open();

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        newTask = new (
                            Id: Convert.ToInt32(reader["id"]),
                            Title: reader["title"]?.ToString(),
                            Description: reader["description"]?.ToString(),
                            IsCompleted: Convert.ToBoolean(reader["isCompleted"]),
                            CreatedAt: Convert.ToDateTime(reader["createdAt"])
                        );
                    }
                }
            }   
        }
        catch(SqlException sqlException)
        {
            _logger.LogError("{sqlException}", sqlException.Message);
        }
        catch(Exception generalException)
        {
            _logger.LogError("{generalException}", generalException.Message);
        }

        return newTask;
    }

    public List<Task>? GetAll()
    {
        List<Task>? allTasks = null;

        try
        {
            using(SqlCommand cmd = new("usp_GetAllTasks", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    allTasks = [];

                    while(reader.Read())
                    {
                        allTasks.Add( 
                            new (
                                Id: Convert.ToInt32(reader["id"]),
                                Title: reader["title"]?.ToString(),
                                Description: reader["description"]?.ToString(),
                                IsCompleted: Convert.ToBoolean(reader["isCompleted"]),
                                CreatedAt: Convert.ToDateTime(reader["createdAt"])
                            )
                        );
                    }
                }
            }   
        }
        catch(SqlException sqlException)
        {
            _logger.LogError("{sqlException}", sqlException.Message);
        }
        catch(Exception generalException)
        {
            _logger.LogError("{generalException}", generalException.Message);
        }

        return allTasks;
    }

    public List<Task>? GetById(int id)
    {
        List<Task>? task = null;

        try
        {
            using(SqlCommand cmd = new("usp_GetTaskById", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.SmallInt).Value = id;
                _conn.Open();

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    task = [];
                    
                    if(reader.Read())
                    {
                        task.Add( 
                            new (
                                Id: Convert.ToInt32(reader["id"]),
                                Title: reader["title"]?.ToString(),
                                Description: reader["description"]?.ToString(),
                                IsCompleted: Convert.ToBoolean(reader["isCompleted"]),
                                CreatedAt: Convert.ToDateTime(reader["createdAt"])
                            )
                        );
                    }
                }
            }   
        }
        catch(SqlException sqlException)
        {
            _logger.LogError("{sqlException}", sqlException.Message);
        }
        catch(Exception generalException)
        {
            _logger.LogError("{generalException}", generalException.Message);
        }

        return task;
    }

    public List<Task>? Update(int id, Task task)
    {
        List<Task>? taskUpdated = null;

        try
        {
            using(SqlCommand cmd = new("usp_UpdateTask", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.SmallInt).Value = id;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = task.Title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = task.Description;
                cmd.Parameters.Add("@isCompleted", SqlDbType.Bit).Value = task.IsCompleted;
                _conn.Open();

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    taskUpdated = [];

                    if(reader.Read())
                    {
                        taskUpdated.Add( 
                            new (
                                Id: id,
                                Title: reader["title"]?.ToString(),
                                Description: reader["description"]?.ToString(),
                                IsCompleted: Convert.ToBoolean(reader["isCompleted"]),
                                CreatedAt: Convert.ToDateTime(reader["createdAt"])
                            )
                        );
                    }
                }
            }   
        }
        catch(SqlException sqlException)
        {
            _logger.LogError("{sqlException}", sqlException.Message);
        }
        catch(Exception generalException)
        {
            _logger.LogError("{generalException}", generalException.Message);
        }

        return taskUpdated;
    }

    public bool? Delete(int id)
    {
        bool? taskDeleted = null;

        try
        {
            using(SqlCommand cmd = new("usp_DeleteTask", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.SmallInt).Value = id;
                _conn.Open();

                taskDeleted = cmd.ExecuteNonQuery() == 1;
            }   
        }
        catch(SqlException sqlException)
        {
            _logger.LogError("{sqlException}", sqlException.Message);
        }
        catch(Exception generalException)
        {
            _logger.LogError("{generalException}", generalException.Message);
        }

        return taskDeleted;
    }
}