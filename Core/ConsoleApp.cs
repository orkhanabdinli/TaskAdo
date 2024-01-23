using ConsoleAppAdo;
using Microsoft.Data.SqlClient;
using System.Text.Json;

using (HttpClient client = new HttpClient())
{
    string response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts");
    List<Post>? posts = JsonSerializer.Deserialize<List<Post>>(response);
    //foreach (var post in posts)
    //{
    //    Console.WriteLine($"UserId: {post.userId}; Id: {post.id}; title: {post.title}");
    //}

    string connString = @"Server=DESKTOP-Q9OE9MV\SQLEXPRESS;Database=TaskDb;Trusted_Connection=True;";
    void Add(int id)
    {
        using (SqlConnection conn = new SqlConnection(connString))
        {
            conn.Open();
            Post post = posts.Find(p => p.id == id);
            string query = $"INSERT INTO Posts VALUES({post.userId},{post.id},'{post.title}','{post.body}')";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                int affectedRow = cmd.ExecuteNonQuery();
                if (affectedRow > 0)
                {
                    Console.WriteLine("Succesfully added");
                }
            }
        }
    }
    Console.WriteLine("Welcome");
    Console.WriteLine("1 - Add to database from API\n"+
                      "2 - Show posts that are not in Database\n"+
                      "3 - Get users post count");
    Console.WriteLine("--------------------------\n" +
                          "Choose the option:");
    string? option = Console.ReadLine();
    int optionNumber;
    bool isInt = int.TryParse(option, out optionNumber);
    if (isInt)
    {
        if (optionNumber >= 0 && optionNumber <= 3)
        {
            switch (optionNumber)
            {
                case 1:
                    Console.WriteLine("Enter post Id");
                    int Id = Convert.ToInt32(Console.ReadLine());
                    Add(Id);
                    break;

            }
        }
    }
}



