using System.Reflection;
using HarmonyLib;
using Verse;

namespace TooltipsInDevmode;

[StaticConstructorOnStartup]
public static class TooltipsInDevmode
{
    static TooltipsInDevmode()
    {
        new Harmony("Mlie.TooltipsInDevmode").PatchAll(Assembly.GetExecutingAssembly());
    }
}