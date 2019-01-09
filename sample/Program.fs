open GameCore.GameLoop
open GameCore.GameModel
open GameCore.GameRunner
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

[<EntryPoint>]
let main _ =
    
    let (width, height) = 640, 480

    let advanceModel runState model =
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
            // setting a red square in the middle of the screen
            yield Colour ((centrex - 100, centrey - 50, 200, 80), Color.Red)

            // rendering the model (an ever increasing int) centre screen
            let textRect = centrex - 80, centrey - 40, 160, 60
            yield Text ("connection", sprintf "%i" model, textRect, Centre, Color.White)

            // rendering some multiline text in the top left of the screen
            let sampleParagraph = [
                "this"
                "is"
                "some"
                "sample"
                "text"
            ]
            let paragraphRect = 20, 20, 100, 300
            yield Paragraph ("connection", sampleParagraph, paragraphRect, TopLeft, Color.White)
        ]
    
    let config = {
        clearColour = Some Color.Black
        resolution = Windowed (width, height)
        // a list of GameCore.Model.Loadable values
        assetsToLoad = [
            // the first part is the assetKey, referenced in getView and the fps counter
            Font ("connection", "./connection")
        ]
        // this will have FPS rendered in the top right, topping out at about 60 if all is well.
        fpsFont = Some "connection"
    }

    // this starts the game. a simplified approach is below
    runGame config advanceModel getView

    // if you don't need an fps counter, or care about the clear colour, then the above could be:
    // let assets = [ Font ("connection", "./connection") ]
    // runWindowedGame (width, height) assets advanceModel getView

    0
