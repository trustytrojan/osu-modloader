using System.Collections.Generic;
using System.Threading;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.ModLoader.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osuTK;

namespace osu.Game.Rulesets.ModLoader.Beatmaps;

public class ModLoaderBeatmapConverter(IBeatmap beatmap, Ruleset ruleset) : BeatmapConverter<ModLoaderHitObject>(beatmap, ruleset)
{
	// todo: Check for conversion types that should be supported (ie. Beatmap.HitObjects.Any(h => h is IHasXPosition))
	// https://github.com/ppy/osu/tree/master/osu.Game/Rulesets/Objects/Types
	public override bool CanConvert() => true;

	protected override IEnumerable<ModLoaderHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap, CancellationToken cancellationToken)
	{
		yield return new ModLoaderHitObject
		{
			Samples = original.Samples,
			StartTime = original.StartTime,
			Position = (original as IHasPosition)?.Position ?? Vector2.Zero,
		};
	}
}
