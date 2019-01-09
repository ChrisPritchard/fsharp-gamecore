# fsharp-gamecore

A fleshed-out game loop from MonoGame with supporting classes, intended to be used as the core loop of larger games.

This is for **2D games only**, i.e. those that use 2d textures like sprites and raw colours. It also supports sounds, music and fonts.

Designed so that all XNA bits and necessary mutable fields are wrapped inside the internal `GameLoop` class, allowing a parent application to remain purely functional and almost platform agnostic. Entry point is the `runGame` method from the GameRunner module.

Available on Nuget at: <https://www.nuget.org/packages/fsharp-gamecore/0.0.4>

## Updates for 0.0.4 vs 0.0.3

I've reworked so the meta config of the game is buried in a record type, and so the entry to the game is via a functional method (no more use of the ugly class).

## Updates for 0.0.5

The way text is drawn has changed in a breaking way, but for the better: instead of specifying position, origin and scale, with the last being basically trial and error based on how the font was specified, you instead specify a destination rect and a alignment within that rect. The scale is dynamically calculated to ensure the text can fit inside that rect, and then adjustments are made based on alignment. 

This makes text drawing more accurate and easy, while also aligning the specification of text view artifacts with the way images and colours are specified (which also just take rects).

This also fixes a bug where drawing centre-aligned text at lower scales would not be centred properly.

## Samples

In this repository (or if you follow the repo url, if using Nuget), there is a samples folder containing a simple game demonstrating the use of the various hooks. For more advanced usage, check other projects on my Github. DungeonRaider (<https://github.com/ChrisPritchard/DungeonRaider>) uses this repo, for example.

## License

Provided under **Unilicense** (except for the font, see below), so go nuts.

## Font and its License

The sample project includes a font that is compiled by the monogame pipeline. The font used is 'Connection', from here: <https://fontlibrary.org/en/font/connection>

This font is provided under the **SIL Open Font License**, a copy of which lies in the root of this repository.
