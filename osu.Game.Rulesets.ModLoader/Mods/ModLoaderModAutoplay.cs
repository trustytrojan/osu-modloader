using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.ModLoader.Replays;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.ModLoader.Mods
{
    public class ModLoaderModAutoplay : ModAutoplay
    {
        public override ModReplayData CreateReplayData(IBeatmap beatmap, IReadOnlyList<Mod> mods)
            => new ModReplayData(new ModLoaderAutoGenerator(beatmap).Generate(), new ModCreatedUser { Username = "sample" });
    }
}
