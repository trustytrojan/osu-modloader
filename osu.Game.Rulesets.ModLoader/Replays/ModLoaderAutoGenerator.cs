using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.ModLoader.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.ModLoader.Replays;

public class ModLoaderAutoGenerator(IBeatmap beatmap) : AutoGenerator<ModLoaderReplayFrame>(beatmap)
{
	public new Beatmap<ModLoaderHitObject> Beatmap => (Beatmap<ModLoaderHitObject>)base.Beatmap;

	protected override void GenerateFrames() =>
		Frames.AddRange(
			Beatmap.HitObjects.Select(hitObject => new ModLoaderReplayFrame
			{
				Time = hitObject.StartTime,
				Position = hitObject.Position,
				// todo: add required inputs and extra frames.
			})
		);
}
