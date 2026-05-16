#!/bin/bash
set -e
OSU_PATH=~/.local/share/osu

# Build the ruleset & test mods
dotnet build -c Release

# Clean up data directory, copy ruleset & mods
rm $OSU_PATH/{logs/*,rulesets/*,mods/*} || true
cp osu.Game.Rulesets.ModLoader/bin/Release/net8.0/osu.Game.Rulesets.ModLoader.dll $OSU_PATH/rulesets/
mkdir -p $OSU_PATH/mods/
cp TestMod*/bin/Release/net8.0/TestMod*.dll $OSU_PATH/mods/
cp ReplayEncoder/bin/Release/net8.0/ReplayEncoder.dll $OSU_PATH/mods/

# Run game, follow runtime log
{
	until ls $OSU_PATH/logs/*.runtime.log &>/dev/null; do sleep 1; done
	tail -f $OSU_PATH/logs/*.runtime.log & echo $! >/tmp/tail_pid
} &
trap 'kill "$(</tmp/tail_pid)"' SIGINT
osu-lazer
wait
