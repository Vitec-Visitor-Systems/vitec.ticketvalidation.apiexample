using Cake.Core;
using Cake.Frosting;
using System.Collections.Generic;
using Cake.Common.Build;
using Cake.Core.IO;

namespace Build;

public class ValidatorBuildContext(ICakeContext context) : FrostingContext(context)
{
    private readonly ICakeContext _context = context;

    public new string Version => _context.BuildSystem().IsRunningOnTeamCity
        ? _context.BuildSystem().TeamCity.Environment.Build.Number
        : Argument("version", "2025.1.1");
    public IDirectory OutputDir => FileSystem.GetDirectory("../release");
    public IDirectory PackageDir => FileSystem.GetDirectory("../packages");

    public string DockerRegistry => EnvironmentVariable("docker_registry", "nexus.entryevent.se:8085");
    public string DockerUser => EnvironmentVariable("docker_username");
    public string DockerPassword => EnvironmentVariable("docker_password");
    public string DockerUrl => "https://" + DockerRegistry;
    public bool DockerAvailable => !string.IsNullOrWhiteSpace(DockerUser);
    private string EnvironmentVariable(string key, string defaultValue = "")
        => base.Environment.GetEnvironmentVariables().TryGetValue(key, out var value)
            ? value ?? defaultValue
            : defaultValue;
    private string Argument(string key, string defaultValue)
        => base.Arguments.HasArgument(key)
            ? base.Arguments.GetArgument(key)
            : defaultValue;

    public IEnumerable<string> DockerEnabledProjects()
        => ["../src/Vitec.TicketValidation.ApiExample.Host/Vitec.TicketValidation.ApiExample.Host.csproj"];
}