using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Bindings;
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
}
