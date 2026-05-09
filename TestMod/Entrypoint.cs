using osu.Framework.Logging;

namespace TestMod;

// There should only be 1 static function inside the Entrypoint class.
// The modloader calls the first one it sees.
static class Entrypoint
{
	static void Func() => Logger.Log("TestMod.Entrypoint.Func called!");
}
