using osu.Framework.Allocation;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.ModLoader.UI;

[Cached]
public partial class ModLoaderPlayfield : Playfield
{
	[BackgroundDependencyLoader]
	private void load() => AddRangeInternal([HitObjectContainer]);
}
