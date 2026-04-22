#!/bin/sh
# Script to rename the class name of your ruleset in file/directory names.

[ $2 ] || {
	echo "old and new ruleset class name required, make sure it's capitalized camel case"
	exit 1
}

OLD_CLASS_NAME="$1"
NEW_CLASS_NAME="$2"

# This may need to be repeated for a folder structure like 1_$OLD_CLASS_NAME/2_$OLD_CLASS_NAME/3_$OLD_CLASS_NAME/...
# In this case we only need to run twice: once for osu.Game.Rulesets.$OLD_CLASS_NAME, and again for the files inside.
find -name "*$OLD_CLASS_NAME*" -exec bash -c 'mv "$1" "${1//$2/$3}"' -- {} "$OLD_CLASS_NAME" "$NEW_CLASS_NAME" \;
find -name "*$OLD_CLASS_NAME*" -exec bash -c 'mv "$1" "${1//$2/$3}"' -- {} "$OLD_CLASS_NAME" "$NEW_CLASS_NAME" \;

# As for renaming the class name *inside* text files, use another tool for that like VSCode's global find and replace.
