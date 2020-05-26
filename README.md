# Timespawn.Core

Timespawn's Unity common libraries. Some of them are specific to DOTS/ECS/entities.

## Features

- DOTS/ECS/entities compatible tween library
- Ease functions
- `SingletonBehaviour` as the singleton `MonoBehaviour` base class
- `ECSTestFixture` as the base class for unit tests of DOTS

## Importing

The recommended way is [adding package from git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html) with Unity package manager:

```
https://gitlab.com/NagaChiang/timespawn-core.git
```

Unity package manager also supports branches and tags if you need certain version:

```
https://gitlab.com/NagaChiang/timespawn-core.git#develop
```

```
https://gitlab.com/NagaChiang/timespawn-core.git#v0.2.0
```

## Dependencies

- Unity 2019.3
- `com.unity.mathematics` 1.1.0
- `com.unity.entities` 0.11.0-preview.7

## Documentation

### DOTS Tween

Timespawn.Core provides a tween library which is DOTS/ECS/entities compatible. So far it only supports limited usages since it's still under development:

- Translation, rotation and scale
- Loop, pingpong
- Pause, resume and stop

To tween an entity, use static functions of `TweenUtils`:

- `MoveEntity()`
- `RotateEntity()`
- `ScaleEntity()`
- `PauseEntity()`
- `ResumeEntity()`
- `StopEntity()`

When a tween finished, there will be a corresponding tag on the entity only existing during the next frame to notify other systems:

- `TweenMovementCompleteTag`
- `TweenRotationCompleteTag`
- `TweenScaleCompleteTag`
