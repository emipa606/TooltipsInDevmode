using HarmonyLib;
using LudeonTK;
using Verse;

namespace TooltipsInDevmode;

//Clear the cache every time the debug window is closed.
[HarmonyPatch(typeof(Window), nameof(Window.Close))]
public static class DevTooltipCache_ClearOnClose
{
    public static void Postfix(Window __instance)
    {
        if (__instance is Dialog_Debug)
        {
            DevGUI_Label.LabelCache.Clear();
        }
    }
}