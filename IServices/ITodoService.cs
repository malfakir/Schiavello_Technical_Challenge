using Schiavello_Technical_Challenge.Models;

namespace Schiavello_Technical_Challenge.IServices
{
    public interface ITodoService
    {

        Task<List<TodoItem>> GetAllTodos();
        TodoItem GetTodoById(int id);
        Task AddTodo(TodoItem todo);
        Task UpdateTodo(TodoItem todo);
        Task DeleteTodo(int id);
    }
}
