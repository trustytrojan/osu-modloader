using HarmonyLib;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game;
using osu.Game.Overlays;
using osu.Game.Overlays.Notifications;

namespace TestModWithDrawable;

// There should only be 1 static function inside the Entrypoint class.
// The modloader calls the first one it sees.
// Here we return a Drawable so that the modloader will attach it to the game.
// This gives us the benefit of using dependencies in osu-framework's dependency caching system instead of reflection.
static class Entrypoint
{
	static MyDrawable Func() => new();
}

static class ExtensionMethods
{
	public static NotificationOverlay GetNotificationOverlay(this OsuGame game) =>
		AccessTools.FieldRefAccess<OsuGame, NotificationOverlay>(game, "Notifications");

	public static void PostNotification(this OsuGame game, Notification notification) =>
		game.GetNotificationOverlay().Post(notification);
}

partial class MyDrawable : Drawable
{
	[BackgroundDependencyLoader]
	void load(OsuGame game) => game.PostNotification(new SimpleNotification() { Text = "TestModWithDrawable.MyDrawable.load called!" });
}
