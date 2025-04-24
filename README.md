# 🐫 Sundered – The Gilded Curse

## 👥 Team Structure & Branching Model

To keep development smooth and conflict-free, we've structured our GitHub repo with **dedicated branches per team**.

### 🌳 Branching Guide

| Branch         | Purpose                           | Owner / Team       |
|----------------|-----------------------------------|--------------------|
| `main`         | ✅ Production-ready, stable code  | All (protected)    |
| `player-dev`   | 🎮 Player mechanics               | Player team        |
| `rooms-dev`    | 🏜️ Level design, room logic       | Rooms team         |
| `puzzles-dev`  | 🧠 Puzzle interactions            | Puzzle team        |
| `art-dev`      | 🎨 2D art assets                  | Art team           |
| `music-dev`    | 🎵 Music and SFX                  | Audio team         |

---

## 🔐 Branch Protection Rules

### `main`
- 🔒 No direct commits allowed
- ✅ Pull request required
- ✅ 1+ reviewer approval required
- ✅ Status checks (tests/linters) must pass
- 🚫 Force pushes and deletions disabled

### `*-dev` (team dev branches)
- ✅ Pull requests recommended (not required)
- ✅ Status checks (if applicable)
- 🚫 Force pushes and deletions disabled
