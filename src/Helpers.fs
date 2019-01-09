module GameCore.Helpers

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open GameModel

let internal asVector2 (x, y) = new Vector2(float32 x, float32 y)
    
let internal asRectangle (x, y, width, height) = 
    new Rectangle (x, y, width, height)
    
let internal asFloatRect (x, y, width, height) =
    float32 x, float32 y, float32 width, float32 height

let internal updateKeyboardInfo (keyboard: KeyboardState) (existing: KeyboardInfo) =
    let pressed = keyboard.GetPressedKeys() |> Set.ofArray
    {
        pressed = pressed |> Set.toList
        keysDown = Set.difference pressed (existing.pressed |> Set.ofList) |> Set.toList
        keysUp = Set.difference (existing.pressed |> Set.ofList) pressed |> Set.toList
    }

let internal getMouseInfo (mouse: MouseState) =
    {
        position = mouse.X, mouse.Y
        pressed = mouse.LeftButton = ButtonState.Pressed, mouse.RightButton = ButtonState.Pressed
    }