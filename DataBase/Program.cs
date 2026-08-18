namespace DataBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var DBForTask3 = new CreateDBContext();
            DBForTask3.Database.EnsureCreated();
        }
    }
}
