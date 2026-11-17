# Seal of Spirits

**A 3D arena defense game built with Unity, where elemental magic is your tool for protecting a seal and completing a ritual.**

Hold back enemies emerging from rifts, defeat them to charge the seal, and keep chaos under control. Learn abilities from elemental idols, combine crowd control with meteor strikes, and choose upgrades as the battle intensifies. Complete the seal before chaos overwhelms it or time runs out.

## Core Mechanics & Code

| System | Implementation | Key code |
| --- | --- | --- |
| Player controller | Movement, sprinting and smooth rotation using `CharacterController` and the Input System. | [PlayerMovement](Assets/Project/Components/Player/PlayerMovement.cs), [PlayerInputController](Assets/Project/Components/Player/PlayerInputController.cs) |
| Elemental abilities | Stun nearby enemies with earth magic, push them back with a tornado, and target an area with a meteor shower. Includes aiming and individual cooldowns. | [PlayerSkills](Assets/Project/Components/Skills/PlayerSkills.cs), [SkillAimingComponent](Assets/Project/Components/Skills/SkillAimingComponent.cs) |
| Seal & chaos | Defeated enemies advance the ritual. Enemies near the seal increase chaos; clearing the area allows it to recover. | [SealComponent](Assets/Project/Components/Seal/SealComponent.cs), [ChaosComponent](Assets/Project/Components/Chaos/ChaosComponent.cs) |
| Enemy waves | Time-based difficulty progression increases the number of active rifts and enemies spawned in staggered groups. | [WaveSystem](Assets/Project/Components/Wave/WaveSystem.cs) |
| Enemy behavior | Enemies approach the seal, avoid crowd overlap and respond to stun, knockback and slowing effects. | [EnemyMovement](Assets/Project/Components/Enemy/EnemyMovement.cs), [EnemyStatus](Assets/Project/Components/Enemy/EnemyStatus.cs) |
| Ability upgrades | Periodic card choices modify cooldowns, duration, meteor radius and elemental effects. | [UpgradeController](Assets/Project/Components/Upgrades/UpgradeController.cs), [UpgradeManager](Assets/Project/Components/Upgrades/UpgradeManager.cs) |

## Technical Highlights

- **Component-based design:** separate responsibilities for input, movement, abilities, enemy behavior and UI.
- **C# events:** connect combat, seal progress, upgrades and interface updates.
- **ScriptableObject configurations:** define enemies, idols, abilities and upgrades outside gameplay logic.
- **Shared gameplay interfaces:** support damage, interaction and stun behavior across components.
- **Game flow controllers:** manage tutorial, ritual, upgrade selection, victory, defeat and pause.
- **DOTween & coroutines:** drive visual feedback, soul-orb effects and timed status effects.

**Stack:** Unity 6 · C# · 3D Physics · Input System · URP · DOTween

## Run

Clone or download the repository, open it in **Unity 6000.3.21f1**, then open `Assets/Scenes/Menu.unity` and press **Play**.

**Controls:**
- `W/A/S/D` — move.
- `Left Shift` — sprint.
- `E` — interact with elemental idols.
- `1 / 2 / 3` — use learned abilities.
- `Left Mouse Button` — confirm targeted ability.
- `Right Mouse Button` — cancel aiming.
- `Esc` — pause.
