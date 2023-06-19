using Schiavello_Technical_Challenge.Models;
using Schiavello_Technical_Challenge.IServices;
using Schiavello_Technical_Challenge.DataStore;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using Schiavello_Technical_Challenge.Pages;

namespace Schiavello_Technical_Challenge.Services
{   

    public class TodoService : ITodoService
    {
        private List<TodoItem> todos;

        private readonly NavigationManager navigationManager;

        //public TodoService(NavigationManager navigationManager)
        //{
        //    this.navigationManager = navigationManager;
        //    LoadData();
        //}
        public TodoService()
        {
           // LoadData();
        }

        public async Task<List<TodoItem>> GetAllTodos()
        {
            using (var client = new HttpClient())
            {
                string Url = "https://schiavelloapi20230616235054.azurewebsites.net/api/todos/";

                var results = await client.GetFromJsonAsync<JsonElement>(Url);

                todos = JsonConvert.DeserializeObject<List<TodoItem>>(results.ToString());              

                return todos;
            }           
        }

        public TodoItem GetTodoById(int id)
        {
            return todos.FirstOrDefault(todo => todo.Id == id);
        }

        public async Task AddTodo(TodoItem todo)
        {
            
            todo.Id = todos.Count > 0 ? todos.Max(t => t.Id) + 1 : 1;
            todos.Add(todo);
            using (var client = new HttpClient())
            {
                string url = "https://schiavelloapi20230616235054.azurewebsites.net/api/todos/";
                
                var RequestUri = new Uri(url);

                var JsonTodo = JsonConvert.SerializeObject(todo);

              
                JObject json = JObject.Parse(JsonTodo.ToString());
                // Serialize the new object back into JSON
                string newJson = JsonTodo.ToString();
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var results = await client.PostAsync(RequestUri, content);

                return;

            }

          
        }

        public async Task UpdateTodo(TodoItem todo)
        {
            var existingTodo = todos.FirstOrDefault(t => t.Id == todo.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.Status = todo.Status;
                
                using(var client = new HttpClient()) 
                {
                    string url = "https://schiavelloapi20230616235054.azurewebsites.net/api/todos/";
                    url += todo.Id.ToString();
                    var RequestUri = new Uri(url);
                   
                    var JsonTodo = JsonConvert.SerializeObject(existingTodo);
                    
                            JObject newObject = new JObject
                            {
                                ["id"] = existingTodo.Id,
                                ["title"] = existingTodo.Title + " ",
                                ["status"] = existingTodo.Status.ToString()
                            };
                    JObject json = JObject.Parse(JsonTodo.ToString());
                    // Serialize the new object back into JSON
                    string newJson = JsonTodo.ToString();
                    var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    var results = await client.PutAsync(RequestUri, content);

                    return;
                    
                }
            }
        }

        public async Task DeleteTodo( int id)
        {
            var existingTodo = todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo != null)
            {              

                using (var client = new HttpClient())
                {
                    string url = "https://schiavelloapi20230616235054.azurewebsites.net/api/todos/";
                    url += id.ToString();
                    var RequestUri = new Uri(url);

                    //var JsonTodo = JsonConvert.SerializeObject(existingTodo);

                    //JObject newObject = new JObject
                    //{
                    //    ["id"] = existingTodo.Id,
                    //    ["title"] = existingTodo.Title + " ",
                    //    ["status"] = existingTodo.Status.ToString()
                    //};
                    //JObject json = JObject.Parse(JsonTodo.ToString());
                    //// Serialize the new object back into JSON
                    //string newJson = JsonTodo.ToString();
                    //var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                    var results = await client.DeleteAsync(url);

                    return;

                }

            }
            return;
        }

       

        
    }


}
