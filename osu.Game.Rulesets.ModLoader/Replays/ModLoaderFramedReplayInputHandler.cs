using System.Collections.Generic;
using osu.Framework.Input.StateChanges;
using osu.Framework.Utils;
using osu.Game.Replays;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.ModLoader.Replays;

public class ModLoaderFramedReplayInputHandler(Replay replay) : FramedReplayInputHandler<ModLoaderReplayFrame>(replay)
{
	protected override bool IsImportant(ModLoaderReplayFrame frame) => frame.Actions.Count != 0;

	protected override void CollectReplayInputs(List<IInput> inputs)
	{
		var position = Interpolation.ValueAt(CurrentTime, StartFrame.Position, EndFrame.Position, StartFrame.Time, EndFrame.Time);

		inputs.AddRange(
		[
			new MousePositionAbsoluteInput
			{
				Position = GamefieldToScreenSpace(position),
			},
			new ReplayState<ModLoaderAction>
			{
				PressedActions = CurrentFrame?.Actions ?? [],
			}
		]);
	}
}
