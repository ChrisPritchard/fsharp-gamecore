module Model

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Audio
open Microsoft.Xna.Framework.Media
open Microsoft.Xna.Framework.Input

type Loadable =
| Texture of key:string * path:string
| TextureMap of key:string * texturePath:string * keyPath:string
| Font of key:string * path:string
| Sound of key:string * path:string
| Song of key:string * path:string

type Origin = | TopLeft | Centre

type ViewArtifact = 
| Image of assetKey:string * destRect: (int*int*int*int) * color:Color
| MappedImage of assetKey:string * mapKey:string * destRect: (int*int*int*int) * color:Color
| Text of assetKey:string * text:string * position:(int*int) * origin:Origin * scale:float * color:Color
| SoundEffect of string
| Music of string

type Resolution =
| Windowed of int * int
| FullScreen of int * int

type Content =
| TextureAsset of Texture2D
| TextureMapAsset of Texture2D * Map<string, Rectangle>
| FontAsset of SpriteFont
| SoundAsset of SoundEffect
| MusicAsset of Song

type RunState = {
    elapsed: float
    keyboard: KeyboardInfo
    mouse: MouseInfo
} and KeyboardInfo = {
    pressed: Keys list;
    keysDown: Keys list;
    keysUp: Keys list
} and MouseInfo = {
    position: int * int
    pressed: bool * bool
}
    
let wasJustPressed key runState = List.contains key runState.keyboard.keysDown
let wasAnyJustPressed keyList runState = keyList |> List.exists (fun k -> wasJustPressed k runState)
let isPressed key runState = List.contains key runState.keyboard.pressed
let isAnyPressed keyList runState = keyList |> List.exists (fun k -> isPressed k runState)
let isMousePressed (left, right) runState = 
    let (ml, mr) = runState.mouse.pressed
    ((ml && left) || (mr && right))