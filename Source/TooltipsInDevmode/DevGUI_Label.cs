using System;
using HarmonyLib;
using LudeonTK;
using UnityEngine;
using Verse;

namespace TooltipsInDevmode;

[HarmonyPatch(typeof(DevGUI), nameof(DevGUI.Label))]
public static class DevGUI_Label
{
    public static void Postfix(Rect rect, string label)
    {
        var newRect = rect;
        var uiScaleDivision = Prefs.UIScale / 2f;
        if (Prefs.UIScale > 1f && Math.Abs(uiScaleDivision - Mathf.Floor(uiScaleDivision)) > float.Epsilon)
        {
            newRect = UIScaling.AdjustRectToUIScaling(rect);
        }

        //if (newRect.width < Text.CalcSize(label).x)
        //{
            TooltipHandler.TipRegion(newRect, label.Trim());
        //}
    }
}