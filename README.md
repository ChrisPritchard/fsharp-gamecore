# fsharp-gamecore

A fleshed-out game loop from MonoGame with supporting classes, intended to be used as the core loop of larger games.

Designed so that all XNA bits and necessary mutable fields are wrapped inside the GameLoop class, allowing a parent application to remain purely functional and almost platform agnostic.

## Font and its License

The game core comes with a default font, used for displaying things like FPS. The font used is 'Connection', from here: <https://fontlibrary.org/en/font/connection>

This font is provided under the **SIL Open Font License**, a copy of which lies in the root of this repository.