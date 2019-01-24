module GameCore.GameRunner

open GameCore.GameModel
open GameCore.GameLoop

open Microsoft.Xna.Framework

/// Entry point to start the game. Takes a config and two 
/// methods: one for advancing the model and one to get a view.
let runGame config (advanceModel : RunState -> 'T option -> 'T option) getView =
    use loop = new GameLoop<'T> (config, advanceModel, getView)
    loop.Run ()

/// A simplified version of runGame
/// with a specified window size and assets list instead of a full config model
let runWindowedGame windowSize assetsToLoad advanceModel getView =
    let config = {
        clearColour = Some Color.AliceBlue
        resolution = Windowed windowSize
        assetsToLoad = assetsToLoad
        fpsFont = None
        mouseVisible = true
    }
    runGame config advanceModel getView