using System.Reflection;

namespace Evently.API.Utils;

internal static class ModulesReference
{
    internal static readonly Assembly[] Assemblies =
    [
        Modules.Events.Application.AssemblyReference.Assembly
    ];

    internal static readonly string[] ModuleNames =
    [
        "Events"
    ];
}
