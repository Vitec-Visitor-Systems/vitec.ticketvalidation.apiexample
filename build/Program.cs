using System.Threading.Tasks;
using Build;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<ValidatorBuildContext>()
            .Run(args);
    }
}


[TaskName("Default")]
[IsDependentOn(typeof(DiagnosticsTask))]
[IsDependentOn(typeof(CleanTask))]
[IsDependentOn(typeof(DockerProjectBuildTask))]
public class DefaultTask : FrostingTask
{

}