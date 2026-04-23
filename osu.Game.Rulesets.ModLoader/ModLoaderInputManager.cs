using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.ModLoader;

public partial class ModLoaderInputManager(RulesetInfo ruleset) :
	RulesetInputManager<ModLoaderAction>(ruleset, 0, SimultaneousBindingMode.Unique)
{
}

public enum ModLoaderAction
{
	[Description("Button 1")]
	Button1,

	[Description("Button 2")]
	Button2,
}
