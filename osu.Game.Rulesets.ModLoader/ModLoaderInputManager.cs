using System.ComponentModel;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.ModLoader
{
    public partial class ModLoaderInputManager : RulesetInputManager<ModLoaderAction>
    {
        public ModLoaderInputManager(RulesetInfo ruleset)
            : base(ruleset, 0, SimultaneousBindingMode.Unique)
        {
        }
    }

    public enum ModLoaderAction
    {
        [Description("Button 1")]
        Button1,

        [Description("Button 2")]
        Button2,
    }
}
