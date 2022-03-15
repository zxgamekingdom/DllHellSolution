#region

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using 库A;
using 库B;

#endregion

namespace 可执行程序;

public static class Program
{

    [STAThread]
    public static void Main(string[] args)
    {
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        var classA = new ClassA();
        classA.Invoke();
        var classB = new ClassB();
        classB.Invoke();
        Console.ReadLine();
    }

    private static Assembly? CurrentDomainOnAssemblyResolve(object? sender,
        ResolveEventArgs args)
    {
        var requestAssemblyInfomation = args.Name;

        var array = requestAssemblyInfomation.Split(",")
            .Select(s => s.Trim())
            .ToArray();

        var assemblyName = array[0];
        var assemblyVersion = array[1].Replace("Version=", string.Empty);
        var assemblyPath = GetAssemblyPath(assemblyName, assemblyVersion);

        return string.IsNullOrWhiteSpace(assemblyPath) is false
            ? Assembly.LoadFile(assemblyPath)
            : default;
    }

    private static string? GetAssemblyPath(string assemblyName, string assemblyVersion)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        return (assemblyName, assemblyVersion) switch
        {
            ("库C", "1.0.0.0") => Path.Combine(baseDirectory, @"库A\库C.dll"),
            ("库C", "2.0.0.0") => Path.Combine(baseDirectory, @"库B\库C.dll"),
            _ => default
        };
    }

}
