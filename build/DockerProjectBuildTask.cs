using System.Linq;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Publish;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Docker;
using Cake.Frosting;

namespace Build;

[TaskName("DockerProjectBuildTask")]
public class DockerProjectBuildTask : FrostingTask<ValidatorBuildContext>
{
    public override void Run(ValidatorBuildContext context)
    {
        if (context.DockerAvailable)
        {
            var loginSettings = new DockerRegistryLoginSettings
            {
                Password = context.DockerPassword,
                Username = context.DockerUser
            };
            context.Log.Information("Logging in to Docker registry");
            context.DockerLogin(loginSettings, context.DockerUrl);

            var projects = context.DockerEnabledProjects().ToArray();
            var settings = new DotNetPublishSettings
            {
                ArgumentCustomization = args => args.Append("/t:PublishContainer /p:SemVer=" + context.Version),
                Configuration = "release",
                Runtime = "linux-x64"
            };

            foreach (var project in projects)
            {
                context.Log.Information(" ------------------ Publish Docker image for " + project + " ------------------------ ");
                context.DotNetPublish(project, settings);
            }
        }
        else
        {
            context.Log.Information("Docker registry not available");
        }
    }
}