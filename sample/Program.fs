open GameCore
open Model

[<EntryPoint>]
let main _ =
    
    let resolution = Windowed (640, 480)
    let updateModel model runState = Some 0
    let getView model runState = []

    use game = new GameLoop<int>(resolution, [], updateModel, getView, true)
    game.Run ()
    0
