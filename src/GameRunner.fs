module GameCore.GameRunner

open GameCore.GameModel
open GameCore.GameLoop

let runGame config (advanceModel : RunState -> 'T option -> 'T option) getView =
    use loop = new GameLoop<'T> (config, advanceModel, getView)
    loop.Run ()