# Mouse Driver

Fullscreenn transparent window, and direct windows input calling from Unity. 
Windows only.

## Fullscreen Transparent Window
New Input System.
- Update Mode: Process Events in Dynamic Update
- Background Behaviour: Ignore Focus

Player Settings
- Disable 'Use DXGI flip model swapchain for D3D11'
- Enable Run In Background
- Fullscreen Window (or borderless)

Setup
See 'Windowless.cs'
Basically, in start, (if not in editor)... call various functions in User32.dll.

## Mouse Input to Desktop
See 'MouseControl.cs'
Basically, call various functions in User32.dll


