diff --git a/README.md b/README.md
new file mode 100644
index 0000000000000000000000000000000000000000..408e6476582678f662e6a30c22dca2a555500d62
--- /dev/null
+++ b/README.md
@@ -0,0 +1,41 @@
+# Pink Panic — 3D Horror Game Starter
+
+You said you want a **3D horror game where a fat pink monster chases the player**.  
+This starter gives you a clear concept and ready-to-use Unity C# scripts.
+
+## Core game loop
+1. Explore a dark environment to collect 5 key items.
+2. The pink monster roams and listens for noise.
+3. Once it detects the player, it enters chase mode.
+4. If the monster catches you, game over.
+5. Escape by unlocking the exit after collecting all keys.
+
+## Vibe and art direction
+- **Tone:** unsettling but slightly surreal.
+- **Monster design:** oversized, bright pink, rubbery skin, heavy footsteps, cartoon-smile face with uncanny eyes.
+- **Audio:** distant breathing, wet footsteps, bass rumble during chase.
+- **Lighting:** dark corridors, flickering lights, occasional pink glow hints.
+
+## Quick Unity setup (2022+)
+1. Create a 3D Unity project.
+2. Add a `CharacterController` to your player object.
+3. Add a `NavMeshAgent` to your monster object.
+4. Bake a NavMesh (`Window > AI > Navigation`).
+5. Create an empty object called `GameManager`.
+6. Add the scripts from `UnityScripts/` to matching objects.
+7. Assign references in the Inspector.
+
+## Suggested scene setup
+- Player spawn in center hall.
+- 3–4 looping corridors + dead ends.
+- 5 collectible key objects.
+- Exit door that opens only when all keys are collected.
+- Monster starts in a distant room.
+
+## Next features to add
+- Hiding spots (lockers, beds).
+- Noise system (running increases detect chance).
+- Jumpscare camera cut-in on capture.
+- Difficulty levels (monster speed / hearing radius).
+
+Good luck — this is a strong horror concept.
