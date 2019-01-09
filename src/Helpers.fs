module GameCore.Helpers

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open GameModel
open System.Text
open Microsoft.Xna.Framework.Graphics

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

let internal lineSpacingRatio = 1.f/4.f

let internal measureText (font: SpriteFont) (text: string) =
    let mutable asMeasured = font.MeasureString text
    let lineGap = float32 font.LineSpacing * lineSpacingRatio
    asMeasured.X, asMeasured.Y - lineGap

let internal measureParagraph (font: SpriteFont) (sb: StringBuilder) =
    let mutable asMeasured = font.MeasureString sb
    let lineGap = float32 font.LineSpacing * lineSpacingRatio
    asMeasured.X, asMeasured.Y - lineGap

let internal stringBuilder lines =
    let rec addLines (sb: StringBuilder) =
        function
        | [] -> sb
        | [s: string] -> sb.Append s
        | (s: string)::rest -> addLines (sb.AppendLine s) rest
    addLines (new StringBuilder ()) lines

let internal getScaleAndPosition (mx, my) destRect align =
    let x, y, w, h = asFloatRect destRect
    let scale = min (w / mx) (h / my)
    let fw, fh = mx * scale, my * scale

    let fx, fy =
        match align with
        | TopLeft -> x, y
        | Left -> x, y + (h - fh) / 2.f
        | Centre -> x + (w - fw) / 2.f, y + ((h - fh) / 2.f)
        | Right -> x + (w - fw), y + (h - fh) / 2.f
        | BottomRight -> x + (w - fh), y + (h - fh)
    
    scale, fx, fy