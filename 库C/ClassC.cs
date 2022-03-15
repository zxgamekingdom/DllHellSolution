using System.Reflection;

namespace 库C;

public class ClassC
{

    public int Invoke()
    {
        var fullName = Assembly.GetExecutingAssembly().FullName;
        Console.WriteLine($"库C {fullName}");

        return 2;
    }

}