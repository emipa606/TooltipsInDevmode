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
		(float width, string trimmed) entry;

		// Only cache if the top window is the debug menu
		if ((Find.WindowStack?.WindowOfType<Dialog_Debug>() is Dialog_Debug))
		{
			if (!LabelCache.TryGetValue(label, out entry))
			{
				float width = Text.CalcSize(label).x;
				string trimmed = label.Trim();
				entry = (width, trimmed);
				LabelCache[label] = entry;
			}
		}
		//ignore for other windows. Reason (example): every log entry in Rimworld is drawn with Label too, so Dictionary will be growing indefinitely
		else
		{
			entry.width = Text.CalcSize(label).x;
			entry.trimmed = label.Trim();
		}

		var newRect = rect;
		var uiScaleDivision = Prefs.UIScale / 2f;
		if (Prefs.UIScale > 1f && Math.Abs(uiScaleDivision - Mathf.Floor(uiScaleDivision)) > float.Epsilon)
		{
			newRect = UIScaling.AdjustRectToUIScaling(rect);
		}

		if (newRect.width < entry.width)    //cached width (if in debug)
		{
			TooltipHandler.TipRegion(newRect, entry.trimmed);   //cached trim (if in debug)
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

//In-game Debug. Show the current count of entries. Check for leaks if not in debug menu, etc.
//Logs Count by pressing <ctrl>+<F7>
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