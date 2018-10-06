open GameCore.GameLoop
open GameCore.Model
open Microsoft.Xna.Framework.Input

[<EntryPoint>]
let main _ =
    
    let resolution = Windowed (640, 480)

    let updateModel runState model =
        match model with
        // The model will start as none, so this is where the initial model is specified
        | None -> Some 0
        // returnin None from an updateModel function will exit the application
        | _ when wasJustPressed Keys.Escape runState -> None
        // business as usual
        | Some n -> Some <| n + 1

    let getView model runState = []

    use game = new GameLoop<int>(resolution, [], updateModel, getView, true)
    game.Run ()
    0
