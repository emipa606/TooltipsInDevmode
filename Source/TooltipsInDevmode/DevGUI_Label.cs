using System;
using System.Collections.Generic;
using HarmonyLib;
using LudeonTK;
using UnityEngine;
using Verse;

namespace TooltipsInDevmode;

[HarmonyPatch(typeof(DevGUI), nameof(DevGUI.Label))]
public static class DevGUI_Label
{
    //Cache. Dictionary to store: label <=> length + trimmedLabel
    public static readonly Dictionary<string, (float width, string trimmed)> LabelCache = new();

    //It will calculate the width & trimmed label only once for each label and then will use these values.

    public static void Postfix(Rect rect, string label)
    {
        (float width, string trimmed) entry;

        // Add tooltip only if the top window is the debug menu
        if (Find.WindowStack?.WindowOfType<Dialog_Debug>() is not null)
        {
            if (!LabelCache.TryGetValue(label, out entry))
            {
                var width = Text.CalcSize(label).x;
                var trimmed = label.Trim();
                entry = (width, trimmed);
                LabelCache[label] = entry;
            }

			var newRect = rect;
			var uiScaleDivision = Prefs.UIScale / 2f;
			if (Prefs.UIScale > 1f && Math.Abs(uiScaleDivision - Mathf.Floor(uiScaleDivision)) > float.Epsilon)
			{
				newRect = UIScaling.AdjustRectToUIScaling(rect);
			}

			if (newRect.width < entry.width) //cached width (if in debug)
			{
				TooltipHandler.TipRegion(newRect, entry.trimmed); //cached trim (if in debug)
			}
		}        
	}
}