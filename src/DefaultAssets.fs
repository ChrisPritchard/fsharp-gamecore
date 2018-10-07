module GameCore.DefaultAssets

open GameModel
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework

let internal loadDefaultAssets graphicsDevice = 
    let pixel = new Texture2D(graphicsDevice, 1, 1) 
    let colorData = [|Color.White|]
    pixel.SetData<Color> colorData
    [
        ("_white", TextureAsset pixel)
        //("_system", FontAsset <| this.Content.Load<SpriteFont> "./_system")
    ]