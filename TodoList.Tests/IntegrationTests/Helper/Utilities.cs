using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoList.Server.Models;
using TodoList.Shared.Auth;

namespace TodoList.Tests.IntegrationTests.Helper
{
    public class Utilities
    {
        public static void InitializeDbForTests(TodoContext db)
        {
            var user = new User
            {
                Username = "user",
                Password = "05926FD3E6EC8C13C5DA5205B546037BDCF861528E0BDB22E9CECE29E567A1BC"
            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(TodoContext db)
        {
            db.Users.RemoveRange(db.Users);
            db.Todos.RemoveRange(db.Todos);
            db.ListsOfTodos.RemoveRange(db.ListsOfTodos);
            InitializeDbForTests(db);
        }
        public static async Task<string> GetNewToken(HttpClient client)
        {
            var user = new AuthenticateRequest
            {
                Username = "user",
                Password = "strongpassword"
            };
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Users/authenticate", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var authenticateResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(responseContent);

            return authenticateResponse.Token;
        }
    }
}
