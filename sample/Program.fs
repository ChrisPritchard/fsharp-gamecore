open GameCore.GameLoop
open GameCore.GameModel
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

[<EntryPoint>]
let main _ =
    
    let (width, height) = 640, 480
    let resolution = Windowed (width, height)

    // a list of GameCore.Model.Loadable values
    let assetsToLoad = []

    let updateModel runState model =
        match model with
        // The model will start as none, so this is where the initial model is specified
        | None -> Some 0
        // returnin None from an updateModel function will exit the application
        | _ when wasJustPressed Keys.Escape runState -> None
        // business as usual
        | Some n -> Some <| n + 1

    // should return a list of GameCore.Model.ViewArtifacts
    let getView runState model = 
        let (centrex, centrey) = width/2, height/2
        [
            Colour ((centrex - 100, centrey - 50, 200, 80), Color.Red)
            // just rendering the model (an ever increasing int) centre screen
            SystemText (sprintf "%i" model, (centrex, centrey), Centre, 1., Color.White)
        ]

    let showFPS = true // this will have FPS rendered in the top right, topping out at about 60 if all is well.
    use game = new GameLoop<int>(resolution, assetsToLoad, updateModel, getView, showFPS)
    game.Run ()
    0
