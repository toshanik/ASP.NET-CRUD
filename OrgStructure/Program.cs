namespace OrgStructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.UseWelcomePage();   // ����������� WelcomePageMiddleware
            app.MapGet("/", () => "Hello World!");
            app.Run();*/

            // ��������� ������
            List<Person> users = new List<Person>
            {
                new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
                new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
            };

            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/api/users", () => users);

            app.MapGet("/api/users/{id}", (string id) =>
            {
                // �������� ������������ �� id
                Person? user = users.FirstOrDefault(u => u.Id == id);
                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, ���������� ���
                return Results.Json(user);
            });

            app.MapDelete("/api/users/{id}", (string id) =>
            {
                // �������� ������������ �� id
                Person? user = users.FirstOrDefault(u => u.Id == id);

                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (user == null) return Results.NotFound(new { message = "������������ �� ������" });

                // ���� ������������ ������, ������� ���
                users.Remove(user);
                return Results.Json(user);
            });

            app.MapPost("/api/users", (Person user) =>
            {

                // ������������� id ��� ������ ������������
                user.Id = Guid.NewGuid().ToString();
                // ��������� ������������ � ������
                users.Add(user);
                return user;
            });

            app.MapPut("/api/users", (Person userData) =>
            {

                // �������� ������������ �� id
                var user = users.FirstOrDefault(u => u.Id == userData.Id);
                // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
                if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
                // ���� ������������ ������, �������� ��� ������ � ���������� ������� �������

                user.Age = userData.Age;
                user.Name = userData.Name;
                return Results.Json(user);
            });

            app.Run();
        }
        public class Person
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public int Age { get; set; }
        }
    }
}
