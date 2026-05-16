using HarmonyLib;
using osu.Game.Screens;
using osu.Game.Screens.Menu;

namespace SkipLoadingScreen;

static class Entrypoint
{
	static void Func() => new Harmony("SkipLoadingScreen").PatchCategory("SkipLoadingScreen");

	[HarmonyPatch(typeof(Loader), "CreateLoadableScreen")]
	[HarmonyPatchCategory("SkipLoadingScreen")]
	static class Loader_CreateLoadableScreen_Patch
	{
		static bool Prefix(ref OsuScreen __result)
		{
			__result = new MainMenu();
			return false; // don't let original method run
		}
	}
}
