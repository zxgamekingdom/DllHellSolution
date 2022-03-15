using System.Reflection;
using 库C;

namespace 库A;

public class ClassA
{

    public void Invoke()
    {
        var fullName = Assembly.GetExecutingAssembly().FullName;
        Console.WriteLine($"库A {fullName}");
        var classC = new ClassC();
        var invoke = classC.Invoke();
        Console.WriteLine($"{nameof(invoke)} = {invoke}");
    }

}