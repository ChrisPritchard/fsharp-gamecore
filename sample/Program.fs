open GameCore.GameLoop
open GameCore.GameModel
open GameCore.GameRunner
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

[<EntryPoint>]
let main _ =
    
    let (width, height) = 640, 480

    let updateModel runState model =
        match model with
        // The model will start as none, so this is where the initial model is specified
        | None -> Some 0
        // if None is returned from an updateModel function, this will exit the application
        | _ when wasJustPressed Keys.Escape runState -> None
        // business as usual, update the model due to state changes or time etc.
        | Some n -> Some <| n + 1

    // should return a list of GameCore.Model.ViewArtifacts
    let getView runState model = 
        let (centrex, centrey) = width/2, height/2
        [
            Colour ((centrex - 100, centrey - 50, 200, 80), Color.Red)
            // just rendering the model (an ever increasing int) centre screen
            Text ("connection", sprintf "%i" model, (centrex, centrey), Centre, 1., Color.White)
        ]
    
    let config = {
        clearColour = None
        resolution = Windowed (width, height)
        // a list of GameCore.Model.Loadable values
        assetsToLoad = [
            // the first part is the assetKey, referenced in getView and the fps counter
            Font ("connection", "./connection")
        ]
        // this will have FPS rendered in the top right, topping out at about 60 if all is well.
        fpsFont = Some "connection"
    }

    runGame config updateModel getView
    0
