module GameCore.GameModel

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Audio
open Microsoft.Xna.Framework.Media

/// Fullscreen or windowed, at a given width and height
type Resolution =
| Windowed of int * int
| FullScreen of int * int

/// Definitions of assets to load on start, e.g. named texture files.
/// IMPORTANT: all paths are relative paths to content files, e.g. /Content/Sprite.png, 
/// except for fonts, which MUST be relative paths (without extensions) to spritefonts built using the content pipeline.
/// This is because fonts cannot be direct loaded, and must be processed via the pipeline.
type Loadable =
/// key (how it is referenced) and path (full relative path to file)
| Texture of key:string * path:string
/// key (how it is referenced), texturePath (full relative path to file), 
/// keyPath (full relativePath to csv that maps keys to dims in image)
| TextureMap of key:string * texturePath:string * keyPath:string
/// key (how it is referenced) and path (full relative path (without extension) to spriteFont)
| Font of key:string * path:string
/// key (how it is referenced) and path (full relative path to file)
| Sound of key:string * path:string
/// key (how it is referenced) and path (full relative path to file)
| Song of key:string * path:string

/// Config settings for the game to run. Things like assets to load, 
/// the resolution, whether or not to clear each frame and with what colour etc
type GameConfig = {
    /// If specified, each draw will be blanked by the colour specified
    clearColour: Color option
    /// Resolution to render the game (in future this will be changable post init)
    resolution: Resolution
    /// All assets (like images, sounds etc) that the game will use
    assetsToLoad: Loadable list
    /// Whether to render an FPS counter in the top right. 
    /// The string is the asset key of a font asset, specified 
    /// under assetsToLoad (it will not work without a font loaded)
    fpsFont: string option
}

/// Where the position is relative to the text drawn
and Origin = 
    | TopLeft | Left | BottomLeft 
    | Top | Centre | Bottom
    | TopRight | Right | BottomRight

/// Definitions of things to be drawn (or played) in the main draw method
type ViewArtifact = 
/// destRect (position on screen) and colour
| Colour of destRect: (int*int*int*int) * colour:Color
/// assetKey (loaded image to use), destRect (position on screen) and colour (effectively shading)
| Image of assetKey:string * destRect: (int*int*int*int) * colour:Color
/// assetKey (loaded image to use), mapKey (which portion of the image to use), destRect (position on screen) and colour (effectively shading)
| MappedImage of assetKey:string * mapKey:string * destRect: (int*int*int*int) * colour:Color
/// assetKey (loaded font to use), position on screen, fontSize (in pixels), origin of position vs text and colour
| Text of assetKey:string * text:string * position: (int*int) * fontSize:int * origin:Origin * colour:Color
/// assetKey (loaded font to use), position on screen, fontSize (in pixels), origin of position vs text and colour
| Paragraph of assetKey:string * lines:string list * position: (int*int) * fontSize:int * origin:Origin * colour:Color
/// assetKey of loaded sound to use
| SoundEffect of string
/// assetKey of loaded song to use
| Music of string

/// The current state of the game. Basically elapsed time and the state of the keyboard or mouse
type RunState = {
    elapsed: float
    keyboard: KeyboardInfo
    mouse: MouseInfo
} 
/// The current state of the keyboard
and KeyboardInfo = {
    pressed: Keys list;
    keysDown: Keys list;
    keysUp: Keys list
} 
/// The current state of the mouse
and MouseInfo = {
    position: int * int
    pressed: bool * bool
}

/// Returns whether the specified key was pressed in the last update
let wasJustPressed key runState = List.contains key runState.keyboard.keysDown
/// Returns whether any of the keys specified were pressed in the last update
let wasAnyJustPressed keyList runState = keyList |> List.exists (fun k -> wasJustPressed k runState)
/// Returns whether the specified key is currently pressed as of the last update
let isPressed key runState = List.contains key runState.keyboard.pressed
/// Returns whether any of the keys specified are pressed as of the last update
let isAnyPressed keyList runState = keyList |> List.exists (fun k -> isPressed k runState)
/// Returns wether the left/right keys of the mouse are pressed as of the last update
let isMousePressed (left, right) runState = 
    let (ml, mr) = runState.mouse.pressed
    ((ml && left) || (mr && right))

type internal Content =
    | TextureAsset of Texture2D
    | TextureMapAsset of Texture2D * Map<string, Rectangle>
    | FontAsset of SpriteFont
    | SoundAsset of SoundEffect
    | MusicAsset of Song