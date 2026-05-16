using HarmonyLib;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game;
using osu.Game.Online.API;

namespace UnlockMainMenuBackgrounds;

static partial class Entrypoint
{
	static MyDrawable Func() => new();

	partial class MyDrawable : Drawable
	{
		[BackgroundDependencyLoader]
		private void load(OsuGame game)
		{
			var gameApi = AccessTools.Property(typeof(OsuGameBase), "API").GetValue(game) as IAPIProvider;
			gameApi.LocalUser.Value.IsSupporter = true;
		}
	}
}
