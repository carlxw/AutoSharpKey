# AutoSharpKey
> .NET console application to mimic keyboard and mouse features from AutoHotkey

## Features
* Send and detect keyboard and mouse inputs
* Move and obtain mouse position
* Identify if a particular window is active or not
* View and modify clipboard content
* Manage, maximize, and minimize windows given it's title
* Low-level keyboard hook implementation to globally detect keystrokes

## Installation
1. Download all the files in the repository
1. Add `System.Windows.Forms` as a program reference

## How to Use
1. Go to `Script.cs`
1. Modify functions `setup()` and `loop()` to meet automative requirements
1. Within loop, return `keyDefault` or `keyBlock` with every key action

## Docs
### `pasteClipboard()`
Returns the clipboard contents. Only applies to clipboard text.

### `copyClipboard(string str)`
Sets the clipboard to the input `str`.

### `send(string str, uint repeat = 0)`
Send keyboard input(s) as stated by `str`, repeating `repeat` many times.
For keyboard modifiers such as `Ctrl` or `Alt`, etc. Refer to the [SendKeys documentation](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=windowsdesktop-8.0#system-windows-forms-sendkeys-send(system-string)).

### `mouseMove(int x, int y)`
Moves the mouse to specified `x` and `y` coordinates.

### `mouseGetPos(bool debug = false)`
Returns the current mouse position as a size-2 integer array. `debug = true` will print the mouse position to terminal.

### `click(string button = "left", int times = 1)`
Click the `left` or `right` (possible button string values) button `times` many times. `times = 2` mimics a double click.

### `sleep(int ms)`
Suspends the process for `ms` milliseconds.

### `winActivate(string windowName)`
Activates the given title window should it exist.

### `msgBox(string message)`
Opens a message box with argument message.

### `exitApp()`
Terminate the application.

### `winGetActiveTitle()`
Returns string of the currently activate window.

### `winActive(string windowName)`
Activates the window given its window title.

### `winExist(string windowName)`
Verifies if a window exists or not given its window title.

### `winMaximize(IntPtr hwnd), winMinimize(IntPtr hwnd)`
Maximizes or minimizes the window given its Window Handle.

## Roadmap
- [ ] Optimize window detection for more optimal time execution
- [ ] Convert into library for abstraction
- [ ] Auto-detect keyboard inputs used, and which will be blocked
- [ ] Implement assert to always include emergency program exit
- [ ] Implement middle mouse click
- [ ] Revisit implementations of `Windows32.cs`

## Technologies Used
* .NET 4.7.2
* Win32 API

## Attributions
All code sourced from external sources are approporiately credited within the respective functions
