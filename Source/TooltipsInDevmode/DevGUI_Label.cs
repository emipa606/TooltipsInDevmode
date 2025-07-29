using System;
using HarmonyLib;
using LudeonTK;
using UnityEngine;
using Verse;
using System.Collections.Generic;

namespace TooltipsInDevmode;

[HarmonyPatch(typeof(DevGUI), nameof(DevGUI.Label))]
public static class DevGUI_Label
{
	//Cache. Dictionary to store: label <=> length
	private static readonly Dictionary<string, float> LabelWidthCache = new();

	//It will calculate the width only once for each label and then will use these values.
	//Potential issues (not sure if it really is): if there will be dynamic generation of ThingDefs and
	//the list will be updated every frame with new values, then this Dictionary will be getting bigger and bigger...
	//But I doubt, that there is such a thing.

	public static void Postfix(Rect rect, string label)
	{
		float width;
		if (!LabelWidthCache.TryGetValue(label, out width))
		{
			width = Text.CalcSize(label).x;
			LabelWidthCache[label] = width;
		}

		var newRect = rect;
		var uiScaleDivision = Prefs.UIScale / 2f;
		if (Prefs.UIScale > 1f && Math.Abs(uiScaleDivision - Mathf.Floor(uiScaleDivision)) > float.Epsilon)
		{
			newRect = UIScaling.AdjustRectToUIScaling(rect);
		}

		if (newRect.width < width)	//much much faster
		{
			TooltipHandler.TipRegion(newRect, label.Trim());
		}
	}
}