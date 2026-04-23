using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.ModLoader.Replays;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.ModLoader.Mods;

public class ModLoaderModAutoplay : ModAutoplay
{
	public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods) =>
		new(
			replay: new ModLoaderAutoGenerator(beatmap).Generate(),
			user: new ModCreatedUser { Username = "sample" }
		);
}
