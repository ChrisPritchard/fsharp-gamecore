module GameCore.GameRunner

open GameCore.GameModel
open GameCore.GameLoop

/// <summary>
/// Entry point to start the game. Takes a config and two 
/// methods: one for advancing the model and one to get a view.
/// </summary>
let runGame config (advanceModel : RunState -> 'T option -> 'T option) getView =
    use loop = new GameLoop<'T> (config, advanceModel, getView)
    loop.Run ()