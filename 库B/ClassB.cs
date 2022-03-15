using System.Reflection;
using 库C;

namespace 库B;

public class ClassB
{

    public void Invoke()
    {
        var fullName = Assembly.GetExecutingAssembly().FullName;
        Console.WriteLine($"库B {fullName}");
        var classC = new ClassC();
        var invoke = classC.Invoke();
        Console.WriteLine($"{nameof(invoke)} = {invoke}");
    }

}