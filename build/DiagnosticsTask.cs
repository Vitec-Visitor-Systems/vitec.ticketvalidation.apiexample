using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build;

[TaskName("Diagnostics")]
public sealed class DiagnosticsTask : FrostingTask<ValidatorBuildContext>
{
    public override void Run(ValidatorBuildContext context)
    {
        context.Log.Information("Version:" + context.Version);
        context.Log.Information("OutputDir:" + context.OutputDir.Path.MakeAbsolute(context.Environment).FullPath);
        context.Log.Information("PackageDir:" + context.PackageDir.Path.MakeAbsolute(context.Environment).FullPath);

        if (context.DockerAvailable)
        {
            context.Log.Information("DockerRegistry: " + context.DockerRegistry);
            context.Log.Information("DockerUser: " + context.DockerUser);
        }
        else
        {
            context.Log.Information("Docker not available");
        }
    }
}