using Cake.Common.IO;
using Cake.Frosting;

namespace Build;

[TaskName("Clean")]
public sealed class CleanTask : FrostingTask<ValidatorBuildContext>
{
    public override void Run(ValidatorBuildContext context)
    {
        context.CleanDirectories(new[]
        {
            context.OutputDir.Path,
            context.PackageDir.Path
        });
    }
}