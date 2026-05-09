using System.Reflection;
using System.IO;
using System;
using HarmonyLib;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.ModLoader.Beatmaps;
using osu.Game.Rulesets.ModLoader.Mods;
using osu.Game.Rulesets.ModLoader.UI;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.ModLoader;

public partial class ModLoaderRuleset : Ruleset
{
	public override string Description => "ModLoader";

	public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod> mods = null) =>
		new DrawableModLoaderRuleset(this, beatmap, mods);

	public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
		new ModLoaderBeatmapConverter(beatmap, this);

	public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) =>
		new ModLoaderDifficultyCalculator(RulesetInfo, beatmap);

	public override IEnumerable<Mod> GetModsFor(ModType type) => type switch
	{
		ModType.Automation => [new ModLoaderModAutoplay()],
		_ => [],
	};

	public override string ShortName => "ModLoader";

	public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) =>
	[
		new KeyBinding(InputKey.Z, ModLoaderAction.Button1),
		new KeyBinding(InputKey.X, ModLoaderAction.Button2),
	];

	public override Drawable CreateIcon() => new Icon(ShortName[0]);

	public partial class Icon : CompositeDrawable
	{
		public Icon(char c)
		{
			InternalChildren =
			[
				new Circle
				{
					Size = new Vector2(20),
					Colour = Color4.White,
				},
				new SpriteText
				{
					Anchor = Anchor.Centre,
					Origin = Anchor.Centre,
					Text = c.ToString(),
					Font = OsuFont.Default.With(size: 18)
				}
			];
		}
	}

	// Leave this line intact. It will bake the correct version into the ruleset on each build/release.
	public override string RulesetAPIVersionSupported => CURRENT_RULESET_API_VERSION;

	static readonly Harmony harmony;

	static ModLoaderRuleset()
	{
		AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
		{
			if (args == null || args.Name == null)
				return null;

			if (!args.Name.StartsWith("0Harmony", StringComparison.OrdinalIgnoreCase))
				return null;

			try
			{
				// Return the ModLoader assembly itself so requests for 0Harmony
				// are satisfied by the merged assembly (no separate 0Harmony.dll needed).
				return typeof(ModLoaderRuleset).Assembly;
			}
			catch
			{
				return null;
			}
		};

		harmony = new Harmony("ModLoader");
	}

	// The ruleset's constructor is the earliest point in the game's lifecycle where we can run.
	// According to 2026.408.0 source, we are inside the stack frame of OsuGameBase.LoadComplete.
	// We want mods to (optionally) provide Drawables to attach to the game. Postfix OsuGameBase.LoadComplete to do so.
	public ModLoaderRuleset()
	{
		harmony.PatchCategory("ModLoaderStartup");
	}

	[HarmonyPatch(typeof(OsuGameBase), "LoadComplete")]
	[HarmonyPatchCategory("ModLoaderStartup")]
	static class OsuGameBase_load_Patch
	{
		static bool loaded = false;

		static void Postfix(OsuGameBase __instance)
		{
			if (loaded)
				return;
			loaded = true;
			harmony.UnpatchCategory("ModLoaderStartup");

			var game = __instance;
			log($"Retrieved game: {game}#{game.GetHashCode()}");

			if (AccessTools.Property(typeof(OsuGameBase), "Storage").GetValue(game) is not Storage gameStorage)
				throw new InvalidOperationException("OsuGameBase.Storage is null");

			IEnumerable<string> modDlls;
			try
			{
				modDlls = gameStorage.GetFiles(@"mods", @"*.dll");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Failed to open mods folder: {ex}");
				return;
			}

			log("Got mod DLL paths, starting mod loading");

			foreach (var modDllRelative in modDlls)
			{
				var modDll = gameStorage.GetFullPath(modDllRelative);
				log($"modDll='{modDll}'");

				var modName = Path.GetFileNameWithoutExtension(modDll);
				log($"modName='{modName}'");

				var entrypointType = Assembly.LoadFrom(modDll).GetType($"{modName}.Entrypoint");
				if (entrypointType == null)
				{
					log($"Type '{modName}.Entrypoint' does not exist, skipping");
					continue;
				}

				var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
				var method = entrypointType.GetMethods(flags).FirstOrDefault();

				if (method?.Invoke(null, null) is Drawable drawable)
				{
					game.Add(drawable);
					log($"Drawable {drawable}#{drawable.GetHashCode()} added to game");
				}
				else
					log("Null returned from entrypoint, side effects assumed");
			}

			log("Finished loading mods");

			static void log(string message) => Logger.Log($"ModLoader: {message}");
		}
	}
}
