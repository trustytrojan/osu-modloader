// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.ModLoader.Objects;
using osu.Game.Rulesets.ModLoader.Objects.Drawables;
using osu.Game.Rulesets.ModLoader.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.ModLoader.UI
{
    [Cached]
    public partial class DrawableModLoaderRuleset : DrawableRuleset<ModLoaderHitObject>
    {
        public DrawableModLoaderRuleset(ModLoaderRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        protected override Playfield CreatePlayfield() => new ModLoaderPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new ModLoaderFramedReplayInputHandler(replay);

        public override DrawableHitObject<ModLoaderHitObject> CreateDrawableRepresentation(ModLoaderHitObject h) => new DrawableModLoaderHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new ModLoaderInputManager(Ruleset?.RulesetInfo);
    }
}
