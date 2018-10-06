# fsharp-gamecore

A fleshed-out game loop from MonoGame with supporting classes, intended to be used as the core loop of larger games.

Designed so that all XNA bits and necessary mutable fields are wrapped inside the GameLoop class, allowing a parent application to remain purely functional and almost platform agnostic.