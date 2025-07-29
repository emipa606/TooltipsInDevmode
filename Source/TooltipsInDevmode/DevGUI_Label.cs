using HarmonyLib;
using LudeonTK;
using System;
using System.Collections.Generic;
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
		if (!LabelCache.TryGetValue(label, out var entry))
		{
			float width = Text.CalcSize(label).x;
			string trimmed = label.Trim();
			entry = (width, trimmed);
			LabelCache[label] = entry;
		}

		var newRect = rect;
		var uiScaleDivision = Prefs.UIScale / 2f;
		if (Prefs.UIScale > 1f && Math.Abs(uiScaleDivision - Mathf.Floor(uiScaleDivision)) > float.Epsilon)
		{
			newRect = UIScaling.AdjustRectToUIScaling(rect);
		}

		if (newRect.width < entry.width)	//much much faster
		{
			TooltipHandler.TipRegion(newRect, entry.trimmed);	//faster only if trimming really happens
		}
	}
}

//Clear the cache every time the debug window is closed.
[HarmonyPatch(typeof(Window), "Close")]
public static class DevTooltipCache_ClearOnClose
{
	public static void Postfix(Window __instance)
	{
		if (__instance is Dialog_Debug)
		{
			int countBefore = DevGUI_Label.LabelCache.Count;
			DevGUI_Label.LabelCache.Clear();
			Log.Message($"[TooltipsInDevmode] Cleared cache after closing dev menu. Removed {countBefore} entries.");
		}
	}
}

//In-game Debug. Show the current count of entries. Check for leaks if not in debug menu
//Log Count by pressing <ctrl>+<F7>
public class TooltipCacheDebugger : GameComponent
{
	public TooltipCacheDebugger(Game game) { }

	public override void GameComponentUpdate()
	{
		if ((UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl))
		&& UnityEngine.Input.GetKeyDown(KeyCode.F7))
		{
			Log.Message($"[TooltipsInDevmode] Cache currently holds {DevGUI_Label.LabelCache.Count} entries.");
		}
	}
}