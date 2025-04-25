using HarmonyLib;
using LudeonTK;
using UnityEngine;
using Verse;

namespace TooltipsInDevmode;

[HarmonyPatch(typeof(DevGUI), nameof(DevGUI.CheckboxPinnable))]
public static class DevGUI_CheckboxPinnable
{
    public static void Postfix(Rect rect, string label)
    {
        if (rect.width - 15f < Text.CalcSize(label).x)
        {
            TooltipHandler.TipRegion(rect, label.Trim());
        }
    }
}