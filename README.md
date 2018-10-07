# fsharp-gamecore

A fleshed-out game loop from MonoGame with supporting classes, intended to be used as the core loop of larger games.

Designed so that all XNA bits and necessary mutable fields are wrapped inside the GameLoop class, allowing a parent application to remain purely functional and almost platform agnostic.

Available on Nuget at: <https://www.nuget.org/packages/fsharp-gamecore/0.0.2>

## Samples

In this repository (or if you follow the repo url, if using Nuget), there is a samples folder containing a simple game demonstrating the use of the various hooks. For more advanced usage, check other projects on my Github. DungeonRaider (<https://github.com/ChrisPritchard/DungeonRaider>) uses this repo, for example.

## License

Provided under **Unilicense** (except for the font, see below), so go nuts.

## Font and its License

The game core comes with a default font, used for displaying things like FPS. The font used is 'Connection', from here: <https://fontlibrary.org/en/font/connection>

This font is provided under the **SIL Open Font License**, a copy of which lies in the root of this repository.