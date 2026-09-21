# Flappy Bird

A Unity 2D Flappy Bird project with physics-based flying, procedurally positioned pipe pairs, collision-based game over, and an experience system for future upgrades.

## Features

- Tap or press the configured `Player/Jump` input action to flap.
- Physics-based vertical movement with a capped fall speed.
- Randomized pipe heights and configurable pipe spacing.
- Pipes move from right to left and are removed after leaving the play area.
- Passing a pipe grants experience.
- Experience progress is displayed with a slider and text counter.
- Collisions stop the run and show the restart UI.
- Menu buttons can be assigned to different scenes through the `gameStart` button-to-scene list.

## Requirements

- Unity `6000.5.2f1` or a compatible Unity 6 version.
- Unity Input System package.
- TextMesh Pro package.

## Getting Started

1. Open the project in Unity Hub.
2. Select the Unity version listed above, or allow Unity to upgrade the project if necessary.
3. Open the desired scene from `Assets/Scenes`.
4. Confirm the scene is included in Build Settings if it needs to be loaded by name.
5. Press Play.

## Controls

The player responds to the `Player/Jump` action in the Unity Input System. Bind that action to a keyboard key, mouse button, controller button, or touch input in the project's input actions.

## Main Scripts

| Script | Purpose |
| --- | --- |
| `Player.cs` | Handles flap input, player physics, rotation, collisions, and game-over state. |
| `Pipe_Spawner.cs` | Creates pipe pairs at timed, randomized heights. |
| `Pipe.cs` | Moves pipes, awards EXP when passed, and destroys off-screen pipes. |
| `ExpManager.cs` | Tracks EXP and updates the EXP slider and text display. |
| `gameStart.cs` | Connects UI buttons to scene names configured in the Inspector. |

## Button-to-Scene Setup

To configure `gameStart`:

1. Add the `gameStart` component to a menu object.
2. Increase the `Button Scenes` list size in the Inspector.
3. For each entry, assign a UI `Button` and enter the target scene's exact name.
4. Add the target scene to Build Settings so Unity can load it.

## Planned Upgrades

The project notes currently include these possible cards and abilities:

- Player size
- Pipe frequency
- Lives
- Pipe gap
- EXP gain
- Constant EXP
- Lucky pipes
- Dash
- Jetpack
- Air brake

## Project Status

This is an active work in progress. Upgrade selection and several planned abilities are not implemented yet.
