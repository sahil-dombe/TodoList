using TodoList.Server.Models;

namespace TodoList.Tests.IntegrationTests.Helper
{
    public class Utilities
    {
        public static void InitializeDbForTests(TodoContext db)
        {
            var user = new User
            {
                Username = "simo",
                Password = "strongpassword"
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
    }
}
