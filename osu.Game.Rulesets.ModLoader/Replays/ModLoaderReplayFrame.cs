// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Replays;
using osuTK;

namespace osu.Game.Rulesets.ModLoader.Replays
{
    public class ModLoaderReplayFrame : ReplayFrame
    {
        public List<ModLoaderAction> Actions = new List<ModLoaderAction>();
        public Vector2 Position;

        public ModLoaderReplayFrame(ModLoaderAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }

        public override bool IsEquivalentTo(ReplayFrame other)
            => other is ModLoaderReplayFrame freeformFrame && Time == freeformFrame.Time && Position == freeformFrame.Position && Actions.SequenceEqual(freeformFrame.Actions);
    }
}
