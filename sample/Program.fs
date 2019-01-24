
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
    let getView (runState: RunState) model = 
        let (centrex, centrey) = width/2, height/2
        [
            // setting a red square in the middle of the screen
            yield Colour ((centrex - 100, centrey - 40, 200, 80), Color.Red)

            // rendering the model (an ever increasing int) centre screen
            yield Text ("connection", sprintf "%i" model, (centrex, centrey), 40, Centre, Color.White)

            // rendering some multiline text in the top left of the screen
            let sampleParagraph = [
                "this"
                "is"
                "some"
                "sample"
                "text"
            ]
            yield Paragraph ("connection", sampleParagraph, (20, 20), 20, TopLeft, Color.White)

            // rendering text in all different alignments
            let px, py = 100, 300
            yield! [
                -50,-50,"TL",TopLeft
                -50,0,"L",Left
                -50,50,"BL",BottomLeft
                0,-50,"T",Top
                0,0,"C",Centre
                0,50,"B",Bottom
                50,-50,"TR",TopRight
                50,0,"R",Right
                50,50,"BR",BottomRight
            ] |> List.collect (fun (dx, dy, text, origin) -> [
                yield Text ("connection", text, (px + dx, py + dy), 18, origin, Color.White)
                yield Colour ((px + dx, py + dy, 2, 2), Color.Red)
            ])

            // rendering a moving image at the bottom of the screen
            let x = centrex + (int runState.elapsed / 10 % (centrex - 50))
            yield Image ("sample", (x, centrey + 80, 80, 80), Color.White)
        ]
    
    let config = {
        clearColour = Some Color.Black
        resolution = Windowed (width, height)
        // a list of GameCore.Model.Loadable values
        assetsToLoad = [
            // the first part is the assetKey of the font, referenced in getView and the fps counter
            Font ("connection", "./connection")
            // this texture is used for the image rendered at the bottom of the sample
            Texture ("sample", "image.png")
        ]
        // this will have FPS rendered in the top right, topping out at about 60 if all is well.
        fpsFont = Some "connection"
        mouseVisible = true
    }

    // this starts the game. a simplified approach is below
    runGame config advanceModel getView

    // if you don't need an fps counter, or care about the clear colour, then the above could be:
    // let assets = [ Font ("connection", "./connection"); Texture ("sample", "image.png") ]
    // runWindowedGame (width, height) assets advanceModel getView

    0
