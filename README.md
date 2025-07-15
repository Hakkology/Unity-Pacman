# Language Options
- [EN](#pacman-game)
- [TR](#pacman-oyunu)

---

# Pacman Game

This project is a simplified recreation of the classic Pacman game, built in Unity for educational purposes. It demonstrates core gameplay architecture, movement control, collision detection, and basic AI behavior using Unity components and C#.

## Features

### 1. Game Flow (FSM)
- The game uses a finite state machine with states like NewGame, NewRound, and GameOver.
- Each state manages its own logic via `Enter`, `Update`, and `Exit` methods.

### 2. Pacman Mechanics
- The player controls Pacman using arrow keys or WASD.
- Movement direction is updated based on available paths using raycasting.

### 3. Ghost Behavior
- Ghosts change their behavior depending on the mode: Chase, Scatter, or Frightened.
- When frightened, ghosts move away from Pacman and slow down.
- On collision, Pacman loses a life or eats the ghost based on ghost state.

### 4. Pellet System
- Standard pellets and PowerPellets are placed across the map.
- Eating all pellets triggers a new round.
- PowerPellets temporarily change ghost behavior.

### 5. Collision and Movement
- Movement and collisions are handled using Unity's physics and raycasting.
- Ghosts and Pacman rely on `MapNode` detection for directional decisions.

### 6. Score and Progression
- The score increases based on pellet type and number of ghosts eaten.
- The ghost score multiplier resets after PowerPellet duration ends.

---

# Pacman Oyunu

Bu proje, klasik Pacman oyununun Unity ile sadeleştirilmiş bir yeniden yapımıdır. Eğitim amaçlı hazırlanmıştır ve temel oyun mimarisi, hareket kontrolü, çarpışma algılama ve basit AI davranışlarını içermektedir.

## Özellikler

### 1. Oyun Akışı (FSM)
- Oyun, NewGame, NewRound ve GameOver gibi durumlardan oluşan bir finite state machine (FSM) yapısı kullanır.
- Her durum kendi `Enter`, `Update` ve `Exit` metodlarıyla kontrol edilir.

### 2. Pacman Mekanikleri
- Oyuncu Pacman’i yön tuşları veya WASD ile kontrol eder.
- Hareket yönü, raycast ile kontrol edilen uygun yollara göre güncellenir.

### 3. Hayalet Davranışı
- Hayaletler kovalama, dağılma ve korkmuş gibi modlara göre davranış değiştirir.
- Korkmuş modda hayaletler Pacman’den kaçar ve yavaşlar.
- Çarpışma durumuna göre Pacman can kaybeder veya hayaleti yer.

### 4. Pellet Sistemi
- Harita üzerinde normal pellet ve PowerPellet nesneleri bulunur.
- Tüm pelletler yendiğinde yeni tur başlar.
- PowerPellet, hayaletlerin davranışını geçici olarak değiştirir.

### 5. Çarpışma ve Hareket
- Hareket ve çarpışmalar Unity'nin fizik sistemi ve raycast kullanılarak yapılır.
- Hayaletler ve Pacman, yön değiştirme kararlarını `MapNode` üzerinden verir.

### 6. Skor ve İlerleme
- Skor, yenen pellet türüne ve yenilen hayalet sayısına göre artar.
- PowerPellet süresi bitince hayalet skor çarpanı sıfırlanır.
