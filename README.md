# osu-modloader

[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/trustytrojan/osu-modloader)

osu-modloader is an [osu!](https://github.com/ppy/osu) custom ruleset that simply loads other DLLs provides an entrypoint to modify the game. This repository has "mod" implementations that provide various tweaks and features.

One of the prominent mods is Replay Encoder, which was brought in from [a dedicated repository](https://github.com/trustytrojan/replay-encoder-ruleset). I plan on making more interesting mods like this one in the future.

## Building

Install a .NET SDK supported by the latest version of the [`ppy.osu.Game` NuGet package](https://www.nuget.org/packages/ppy.osu.Game) (currently .NET 10) and run `dotnet build -c Release`. The resulting mod DLLs will be in `<mod>/bin/Release/net10.0/<mod>.dll`.
