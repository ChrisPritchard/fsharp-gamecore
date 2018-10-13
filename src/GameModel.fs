module GameCore.GameModel

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

/// <summary>
/// Fullscreen or windowed, at a given width and height
/// </summary>
type Resolution =
| Windowed of int * int
| FullScreen of int * int

/// <summary>
/// Definitions of assets to load on start, e.g. named texture files.
/// IMPORTANT: all paths are relative paths to content files, e.g. /Content/Sprite.png, 
/// except for fonts, which MUST be relative paths (without extensions) to spritefonts built using the content pipeline.
/// This is because fonts cannot be direct loaded, and must be processed via the pipeline.
/// </summary>
type Loadable =
| Texture of key:string * path:string
| TextureMap of key:string * texturePath:string * keyPath:string
| Font of key:string * path:string
| Sound of key:string * path:string
| Song of key:string * path:string

/// <summary>
/// Config settings for the game to run. Things like assets to load, 
/// the resolution, whether or not to clear each frame and with what colour etc
/// </summary>
type GameConfig = {
    /// <summary>
    /// If specified, each draw will be blanked by the colour specified
    /// </summary>
    clearColour: Color option
    /// <summary>
    /// Resolution to render the game (in future this will be changable post init)
    /// </summary>
    resolution: Resolution
    /// <summary>
    /// All assets (like images, sounds etc) that the game will use
    /// </summary>
    assetsToLoad: Loadable list
    /// <summary>
    /// Whether to render an FPS counter in the top right. 
    /// The string is the asset key of a font asset, specified 
    /// under assetsToLoad (it will not work without a font loaded)
    /// </summary>
    fpsFont: string option
}

/// <summary>
/// Where a given piece of text should be drawn from, given its x,y
/// </summary>
type Origin = | TopLeft | Centre

/// <summary>
/// Definitions of things to be drawn (or played) in the main draw method
/// </summary>
type ViewArtifact = 
| Colour of destRect: (int*int*int*int) * color:Color
| Image of assetKey:string * destRect: (int*int*int*int) * color:Color
| MappedImage of assetKey:string * mapKey:string * destRect: (int*int*int*int) * color:Color
| Text of assetKey:string * text:string * position:(int*int) * origin:Origin * scale:float * color:Color
| SoundEffect of string
| Music of string

/// <summary>
/// The current state of the game. Basically elapsed time and the state of the keyboard or mouse
/// </summary>
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