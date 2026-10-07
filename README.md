# Jawi Maze Learning

A Unity maze-learning prototype that adapts Pac-Man gameplay for audio-guided Jawi practice. Players navigate a maze, listen to an answer's audio cue, and collect the matching answer while avoiding ghosts.

Built on **PacManUnity by Quentin Morel (Im-Rises)**. The original author credits, screenshots, and reference material are preserved in the [upstream README](docs/UPSTREAM_README.md); those demos and releases represent the original game.

## Learning adaptation

- Answer groups select a random target and play its attached audio clip.
- Correct answer collection awards 100 points, resets player/ghost positions, and pauses briefly for feedback; wrong answer collection invokes the player-death flow.
- The answer system advances between configured groups and exposes an exit button after the last group.
- Local player profiles support names, switching, and per-player score storage through `PlayerPrefs`.
- Directional UI buttons feed movement into the existing tile-based player controller.
- The inherited framework provides ghost movement, maze navigation, high scores, audio settings, and three maze scenes.

This is a source-reviewed prototype. Answer progression, scene wiring, device controls, and score reporting still need runtime verification. See [validation notes](docs/VALIDATION.md).

## Open in Unity

1. Clone the full repository and open the project root in Unity Hub.
2. Use **Unity 2022.3.62f3**, recorded in [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt).
3. Let Unity import the assets and resolve [packages](Packages/manifest.json), including Input System 1.14.0 and TextMesh Pro 3.0.7.
4. Open [TitleScreen](Assets/Scenes/TitleScreen.unity) and check the profile and level-selection flow in Play Mode using test data.

The six enabled scenes are `TitleScreen`, `Credits`, `LevelSelector`, `CustomLevel1`, `OriginalLevel`, and `CustomLevel2`; their order is recorded in [EditorBuildSettings.asset](ProjectSettings/EditorBuildSettings.asset).

**Score reporting:** `GameStartupController` contains an HTTP POST integration with the external Hanana service. When a current player exists, startup/profile actions can submit the player's name, identifier, collected totals, and device name. The server implementation and service availability were not verified. Review this integration before running with real learner information; the project should not be described as fully offline.

## Source guide

| Area | Source |
| --- | --- |
| Target selection and audio prompts | [AnswerController.cs](Assets/AnswerController.cs) |
| Answer collisions, feedback, and progression | [AnswerCollect.cs](Assets/AnswerCollect.cs) |
| Player creation and reporting | [GameStartupController.cs](Assets/GameStartupController.cs), [PlayerInputController.cs](Assets/PlayerInputController.cs) |
| Player switching UI | [DetailPlayer.cs](Assets/DetailPlayer.cs) |
| On-screen direction buttons | [UIButtonHold.cs](Assets/UIButtonHold.cs) |
| Tile-based movement | [PlayerController.cs](Assets/Scripts/Player/PlayerController.cs) |
| Game flow and score handling | [GameHandler.cs](Assets/Scripts/GameHandler/GameHandler.cs), [ScoreHandler.cs](Assets/Scripts/ScoreHandler/ScoreHandler.cs) |

## Credits and license

The base project is [Im-Rises/PacManUnity](https://github.com/Im-Rises/PacManUnity), by Quentin Morel. Its MIT copyright notice is retained in [LICENSE](LICENSE). Refer to the preserved upstream documentation for original asset/audio references. This documentation does not establish separate licensing rights for every included media asset.
