# NCERT AR Education App - Setup Guide

## Quick Setup (30-Hour Sprint)

### Step 1: Scene Setup (In Unity Editor)

1. Open `Assets/Scenes/SampleScene.unity`

2. **Add AR Components to XR Origin:**
   - Select `XR Origin` in the hierarchy
   - Add Component → `AR Plane Manager`
   - Add Component → `AR Raycast Manager`

3. **Create AR Canvas (for in-AR UI):**
   - Right-click Hierarchy → UI → Canvas
   - Rename to "AR Canvas"
   - Set Render Mode: Screen Space - Overlay
   - Add these UI elements:
     - Button: "Back" (top-left corner)
     - Button: "Remove Model" (top-right corner)  
     - Text (TMP): Status text (bottom)

4. **Update Options Canvas:**
   - Add TMP Dropdown → name it "ClassDropdown"
   - Add TMP Dropdown → name it "ModelDropdown"
   - Add Button → "View in AR"
   - Add TMP Text → Status text

### Step 2: Create Manager GameObject

1. Create Empty GameObject → name it "GameManager"
2. Add `Manager.cs` script
3. Drag references:
   - AR Raycast Manager (from XR Origin)
   - AR Plane Manager (from XR Origin)
   - All Canvas references
   - All Dropdown references
   - All Button references

### Step 3: Generate Placeholder Models

1. Create Empty GameObject → name it "ModelGenerator"
2. Add `PlaceholderModelGenerator.cs`
3. Right-click the script → "Generate All Placeholder Models"
4. Drag generated models to `Assets/Prefabs/` folder
5. Assign prefabs to Manager script

### Step 4: Test in Editor

1. Press Play
2. Use XR Simulation (Window → XR → AR Foundation → XR Environment)
3. Test dropdown flow and model spawning

### Step 5: Build for Android

1. File → Build Settings → Android
2. Player Settings:
   - Other Settings → Graphics API: Remove Vulkan (keep OpenGLES3)
   - XR Settings → ARCore supported: ✓
   - Minimum API Level: 24
3. Build and Run

## Script Reference

### Manager.cs
Main controller handling:
- UI screen flow (Intro → Options → AR)
- Dropdown population based on class selection
- AR model spawning via touch/tap

### ARModelInteraction.cs
Attached to each model prefab for:
- Single finger rotation
- Pinch to scale
- Optional auto-rotate

### PlaceholderModelGenerator.cs
Editor utility to create simple 3D placeholder models for testing

### UIManager.cs
Optional smooth panel transitions with fade effects

## Model Prefab Structure

Each model prefab should have:
```
ModelName_Prefab
├── ARModelInteraction (script)
├── Child objects (3D geometry)
└── Collider (for interaction)
```

## Dropdown Hierarchy

```
Class Dropdown
├── Class 6
│   ├── Solar System
│   └── Plant Cell
├── Class 7
│   ├── Digestive System
│   └── Water Cycle
└── Class 8
    └── Atom Structure
```

## Tips for 30-Hour Deadline

1. **Use placeholders first** - Get the full flow working with simple shapes
2. **Test on device early** - Don't wait until the end
3. **Keep models low-poly** - Under 10k triangles each
4. **Use single scene** - No scene loading complexity
5. **Skip animations initially** - Add if time permits

## Free 3D Model Sources

- [Sketchfab](https://sketchfab.com/search?features=downloadable&type=models&q=educational)
- [TurboSquid Free](https://www.turbosquid.com/Search/3D-Models/free/educational)
- [Unity Asset Store](https://assetstore.unity.com/?category=3d&free=true)
- [Poly Haven](https://polyhaven.com/)
