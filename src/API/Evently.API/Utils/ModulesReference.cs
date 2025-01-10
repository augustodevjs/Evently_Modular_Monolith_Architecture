using System.Reflection;

namespace Evently.API.Utils;

internal static class ModulesReference
{
    internal static readonly Assembly[] Assemblies =
    [
        Modules.Events.Application.AssemblyReference.Assembly,
        Modules.Users.Application.AssemblyReference.Assembly,
        Modules.Ticketing.Application.AssemblyReference.Assembly
    ];

    internal static readonly string[] ModuleNames =
    [
        "events",
        "users",
        "ticketing"
    ];
}
