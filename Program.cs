using System.Reflection;

class Program
{
  static StreamWriter? writer;
  static List<string> tasks = new List<string>();

  static void Main(string[] args)
  {
    if (args.Length < 1)
    {
      Console.WriteLine("Must have at least 1 argument.");
      Environment.Exit(1);
    }

    using (StreamReader reader = new StreamReader($"{Assembly.GetExecutingAssembly().Location}/../../../../tasks.txt"))
    {
      string? line = reader.ReadLine();
      while (line != null) 
      {
        tasks.Add(line);
        line = reader.ReadLine();
      }
      reader.Close();
    }
      
    writer = new StreamWriter($"{Assembly.GetExecutingAssembly().Location}/../../../../tasks.txt");
    string command = args[0];
    switch (command.ToLower())
    {
      case "create":
        Create(args[1]);
        break;
      case "delete":
        Delete(args[1]);
        break;
      case "move":
        Move(int.Parse(args[1]), int.Parse(args[2]));
        break;
      case "list":
        List();
        break;
      default:
        Console.WriteLine("Unknown argument!");
        Environment.Exit(1);
        break;
    }
    foreach (string task in tasks)
    {
      writer.WriteLine(task);
    }
    writer.Close();
  }

  static void Create(string task)
  {
    tasks.Add(task);
    Console.WriteLine($"Created task: {task}.");
  }

  static void Delete(string task)
  {
    tasks.Remove(task);
    Console.WriteLine($"Deleted task: {task}.");
  }

  static void Move(int oldPos, int newPos)
  {
    
    if (oldPos < 1 || newPos < 1)
    {
      Console.WriteLine("This command is one-based, not zero-based.");
      Environment.Exit(1);
      return;
    }
    if (oldPos > tasks.Count || newPos > tasks.Count)
    {
      Console.WriteLine($"Invalid position, position must be less than or equal to {tasks.Count}");
      Environment.Exit(1);
      return;
    }
    string task = tasks[oldPos - 1];
    tasks.RemoveAt(oldPos - 1);
    tasks.Insert(newPos - 1, task);
  }

  static void List()
  {
    foreach (string task in tasks)
    {
      Console.WriteLine(task);
    }
  }
}