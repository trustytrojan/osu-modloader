// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.ModLoader.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.ModLoader.Replays
{
    public class ModLoaderAutoGenerator : AutoGenerator<ModLoaderReplayFrame>
    {
        public new Beatmap<ModLoaderHitObject> Beatmap => (Beatmap<ModLoaderHitObject>)base.Beatmap;

        public ModLoaderAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override void GenerateFrames()
        {
            Frames.Add(new ModLoaderReplayFrame());

            foreach (ModLoaderHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new ModLoaderReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                    // todo: add required inputs and extra frames.
                });
            }
        }
    }
}
