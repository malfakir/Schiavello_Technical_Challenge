
using System.Text.Json.Serialization;

namespace Schiavello_Technical_Challenge.Models
{
    public enum TodoStatus
    {
        New ,
        Completed,
        Active,
        Deleted
    }
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Status { get; set; }

    }
}
