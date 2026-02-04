# NCERT AR Education App - Complete Setup Guide

## Prerequisites
- Unity 2022.3+ with AR Foundation 5.x
- Android Build Support installed
- Project already has AR Foundation, ARCore, URP, TextMesh Pro

---

## STEP 1: Open Your Scene

1. In Project window: `Assets → Scenes → SampleScene`
2. Double-click to open

---

## STEP 2: Add AR Components to XR Origin

1. In Hierarchy, click on **XR Origin**
2. In Inspector, click **Add Component**
3. Search and add: `AR Plane Manager`
4. Click **Add Component** again
5. Search and add: `AR Raycast Manager`

**Your XR Origin should now have:**
- XR Origin (script)
- AR Plane Manager ✓
- AR Raycast Manager ✓

---

## STEP 3: Create the Intro Canvas (Main Menu)

### 3.1 Create Canvas
1. Right-click in Hierarchy → **UI → Canvas**
2. Rename it to `IntroCanvas`
3. In Inspector, set:
   - Render Mode: `Screen Space - Overlay`
   - UI Scale Mode: `Scale With Screen Size`
   - Reference Resolution: `1080 x 1920`
   - Match: `0.5`

### 3.2 Add Background Panel
1. Right-click on `IntroCanvas` → **UI → Image**
2. Rename to `Background`
3. In Inspector:
   - Anchor: Stretch-Stretch (hold Alt + click bottom-right preset)
   - Left/Right/Top/Bottom: `0`
   - Color: Pick a nice gradient or solid color (e.g., `#1A237E` dark blue)

### 3.3 Add App Title
1. Right-click on `IntroCanvas` → **UI → Text - TextMeshPro**
2. If prompted, click "Import TMP Essentials"
3. Rename to `TitleText`
4. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-200`
   - Width: `800`, Height: `150`
   - Text: `NCERT AR`
   - Font Size: `72`
   - Alignment: Center
   - Color: White

### 3.4 Add Subtitle
1. Right-click on `IntroCanvas` → **UI → Text - TextMeshPro**
2. Rename to `SubtitleText`
3. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-320`
   - Width: `800`, Height: `80`
   - Text: `Interactive Learning in AR`
   - Font Size: `32`
   - Alignment: Center
   - Color: Light gray `#B0BEC5`

### 3.5 Add Start Button
1. Right-click on `IntroCanvas` → **UI → Button - TextMeshPro**
2. Rename to `StartButton`
3. In Inspector:
   - Anchor: Middle-Center
   - Pos Y: `0`
   - Width: `400`, Height: `80`
4. Expand StartButton, select the child `Text (TMP)`:
   - Text: `START`
   - Font Size: `36`
   - Color: White
5. Select `StartButton` again:
   - Image Color: `#4CAF50` (green)

### 3.6 Add Exit Button
1. Right-click on `IntroCanvas` → **UI → Button - TextMeshPro**
2. Rename to `ExitButton`
3. In Inspector:
   - Anchor: Middle-Center
   - Pos Y: `-120`
   - Width: `400`, Height: `80`
4. Expand ExitButton, select `Text (TMP)`:
   - Text: `EXIT`
   - Font Size: `36`
5. Select `ExitButton`:
   - Image Color: `#F44336` (red)

---

## STEP 4: Create the Options Canvas (Class/Model Selection)

### 4.1 Create Canvas
1. Right-click in Hierarchy → **UI → Canvas**
2. Rename to `OptionsCanvas`
3. Same settings as IntroCanvas:
   - Render Mode: `Screen Space - Overlay`
   - UI Scale Mode: `Scale With Screen Size`
   - Reference Resolution: `1080 x 1920`

### 4.2 Add Background
1. Right-click on `OptionsCanvas` → **UI → Image**
2. Rename to `Background`
3. Anchor: Stretch-Stretch, all offsets `0`
4. Color: `#1A237E` (same as intro)

### 4.3 Add Header Title
1. Right-click on `OptionsCanvas` → **UI → Text - TextMeshPro**
2. Rename to `HeaderText`
3. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-100`
   - Width: `800`, Height: `80`
   - Text: `Select Class & Model`
   - Font Size: `48`
   - Alignment: Center
   - Color: White

### 4.4 Add "Select Class" Label
1. Right-click on `OptionsCanvas` → **UI → Text - TextMeshPro**
2. Rename to `ClassLabel`
3. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-250`
   - Width: `600`, Height: `50`
   - Text: `Choose Your Class:`
   - Font Size: `28`
   - Alignment: Left
   - Color: `#B0BEC5`

### 4.5 Add Class Dropdown
1. Right-click on `OptionsCanvas` → **UI → Dropdown - TextMeshPro**
2. Rename to `ClassDropdown`
3. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-330`
   - Width: `600`, Height: `70`
4. In the Dropdown component:
   - Clear existing options
   - Add options: `Select Class`, `Class 6`, `Class 7`, `Class 8`

### 4.6 Add "Select Model" Label
1. Right-click on `OptionsCanvas` → **UI → Text - TextMeshPro**
2. Rename to `ModelLabel`
3. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-450`
   - Width: `600`, Height: `50`
   - Text: `Choose Model:`
   - Font Size: `28`
   - Alignment: Left
   - Color: `#B0BEC5`

### 4.7 Add Model Dropdown
1. Right-click on `OptionsCanvas` → **UI → Dropdown - TextMeshPro**
2. Rename to `ModelDropdown`
3. In Inspector:
   - Anchor: Top-Center
   - Pos Y: `-530`
   - Width: `600`, Height: `70`
4. In Dropdown component:
   - Clear options, add: `Select Model`

### 4.8 Add "View in AR" Button
1. Right-click on `OptionsCanvas` → **UI → Button - TextMeshPro**
2. Rename to `SpawnButton`
3. In Inspector:
   - Anchor: Middle-Center
   - Pos Y: `-100`
   - Width: `500`, Height: `90`
4. Child `Text (TMP)`:
   - Text: `VIEW IN AR`
   - Font Size: `36`
5. Button Image Color: `#2196F3` (blue)

### 4.9 Add Back Button
1. Right-click on `OptionsCanvas` → **UI → Button - TextMeshPro**
2. Rename to `BackToIntroButton`
3. In Inspector:
   - Anchor: Bottom-Center
   - Pos Y: `100`
   - Width: `300`, Height: `60`
4. Child `Text (TMP)`:
   - Text: `← BACK`
   - Font Size: `28`
5. Button Image Color: `#757575` (gray)

### 4.10 Add Status Text
1. Right-click on `OptionsCanvas` → **UI → Text - TextMeshPro**
2. Rename to `StatusText`
3. In Inspector:
   - Anchor: Bottom-Center
   - Pos Y: `200`
   - Width: `800`, Height: `60`
   - Text: `Select a class to see available models`
   - Font Size: `24`
   - Alignment: Center
   - Color: `#FFD54F` (amber)

---

## STEP 5: Create the AR Canvas (In-AR UI)

### 5.1 Create Canvas
1. Right-click in Hierarchy → **UI → Canvas**
2. Rename to `ARCanvas`
3. Settings:
   - Render Mode: `Screen Space - Overlay`
   - UI Scale Mode: `Scale With Screen Size`
   - Reference Resolution: `1080 x 1920`

### 5.2 Add Back Button (Top-Left)
1. Right-click on `ARCanvas` → **UI → Button - TextMeshPro**
2. Rename to `ARBackButton`
3. In Inspector:
   - Anchor: Top-Left
   - Pos X: `100`, Pos Y: `-80`
   - Width: `150`, Height: `60`
4. Child `Text (TMP)`:
   - Text: `← Back`
   - Font Size: `24`
5. Button Color: `#424242` with alpha `200`

### 5.3 Add Remove Model Button (Top-Right)
1. Right-click on `ARCanvas` → **UI → Button - TextMeshPro**
2. Rename to `RemoveModelButton`
3. In Inspector:
   - Anchor: Top-Right
   - Pos X: `-100`, Pos Y: `-80`
   - Width: `150`, Height: `60`
4. Child `Text (TMP)`:
   - Text: `Remove`
   - Font Size: `24`
5. Button Color: `#D32F2F` (red)

### 5.4 Add AR Status Text (Bottom)
1. Right-click on `ARCanvas` → **UI → Text - TextMeshPro**
2. Rename to `ARStatusText`
3. In Inspector:
   - Anchor: Bottom-Center
   - Pos Y: `150`
   - Width: `900`, Height: `100`
   - Text: `Point camera at a flat surface and tap to place model`
   - Font Size: `28`
   - Alignment: Center
   - Color: White
   - Add Outline (Material Preset or shadow for readability)

### 5.5 Add Instruction Panel (Optional but helpful)
1. Right-click on `ARCanvas` → **UI → Image**
2. Rename to `InstructionPanel`
3. In Inspector:
   - Anchor: Bottom-Center
   - Pos Y: `150`
   - Width: `950`, Height: `120`
   - Color: Black with Alpha `150` (semi-transparent)
4. Make `ARStatusText` a child of this panel

---

## STEP 6: Disable Extra Canvases Initially

1. Select `OptionsCanvas` in Hierarchy
2. In Inspector, **uncheck the checkbox** next to the name (disables it)
3. Select `ARCanvas` in Hierarchy
4. **Uncheck** to disable it too
5. Only `IntroCanvas` should be active (checked)

---

## STEP 7: Create GameManager

1. Right-click in Hierarchy → **Create Empty**
2. Rename to `GameManager`
3. Reset Transform (Right-click Transform → Reset)
4. Click **Add Component** → search `Manager` → add it

---

## STEP 8: Wire Up the Manager Script

With `GameManager` selected, drag these references into the Manager script Inspector slots:

### AR Components
| Field | Drag From |
|-------|-----------|
| AR Raycast Manager | XR Origin (the component) |
| AR Plane Manager | XR Origin (the component) |

### UI Panels
| Field | Drag From |
|-------|-----------|
| Intro Canvas | IntroCanvas (the GameObject) |
| Options Canvas | OptionsCanvas (the GameObject) |
| AR Canvas | ARCanvas (the GameObject) |

### Dropdowns
| Field | Drag From |
|-------|-----------|
| Class Dropdown | ClassDropdown |
| Model Dropdown | ModelDropdown |

### Buttons
| Field | Drag From |
|-------|-----------|
| Start Button | StartButton |
| Exit Button | ExitButton |
| Spawn Button | SpawnButton |
| Back Button | ARBackButton |
| Remove Model Button | RemoveModelButton |

### Status Text
| Field | Drag From |
|-------|-----------|
| Status Text | StatusText (from OptionsCanvas) OR ARStatusText |

---

## STEP 9: Generate Placeholder Models

1. Right-click in Hierarchy → **Create Empty**
2. Rename to `ModelGenerator`
3. Add Component → `PlaceholderModelGenerator`
4. In Inspector, **right-click** the script header → **Generate All Placeholder Models**
5. You'll see 5 new GameObjects created in the scene

### Save as Prefabs
1. In Project window, go to `Assets/Prefabs` folder
2. Drag each generated model from Hierarchy into the Prefabs folder:
   - `SolarSystem_Placeholder`
   - `PlantCell_Placeholder`
   - `DigestiveSystem_Placeholder`
   - `WaterCycle_Placeholder`
   - `Atom_Placeholder`
3. Delete the objects from the scene (they're now saved as prefabs)
4. Delete the `ModelGenerator` object too

---

## STEP 10: Assign Model Prefabs to Manager

1. Select `GameManager`
2. In Manager script, find the Model Prefabs sections
3. Drag from `Assets/Prefabs`:

| Field | Prefab |
|-------|--------|
| Solar System Model | SolarSystem_Placeholder |
| Plant Cell Model | PlantCell_Placeholder |
| Digestive System Model | DigestiveSystem_Placeholder |
| Water Cycle Model | WaterCycle_Placeholder |
| Atom Model | Atom_Placeholder |

---

## STEP 11: Add Back-to-Intro Button Functionality

The `BackToIntroButton` in OptionsCanvas needs a listener:

1. Select `BackToIntroButton` in OptionsCanvas
2. In Button component, find **On Click ()**
3. Click **+** to add
4. Drag `GameManager` to the object slot
5. Select Function: `Manager → ShowIntroScreen` (if available) 

**OR** add this to Manager.cs (I'll update it):
- For now, you can skip this - the main flow works without it

---

## STEP 12: Test in Editor

1. Press **Play**
2. You should see:
   - Intro screen with START and EXIT buttons
   - Click START → Options screen appears
   - Select Class 6 → Model dropdown populates
   - Select Solar System → Click "View in AR"
   - AR view appears (use XR Simulation to test placement)

### Enable XR Simulation (Editor Testing)
1. Window → XR → AR Foundation → **XR Environment**
2. Select a simulation environment
3. Use WASD to move, mouse to look around
4. Click on detected planes to place models

---

## STEP 13: Build for Android

### Build Settings
1. File → Build Settings
2. Switch Platform to **Android**
3. Add Open Scenes (SampleScene)

### Player Settings
1. Click **Player Settings**
2. **Other Settings:**
   - Scripting Backend: `IL2CPP`
   - Target Architectures: ✓ ARM64 (uncheck ARMv7 if only targeting newer devices)
   - Minimum API Level: `Android 7.0 (API 24)`
   - Graphics APIs: Remove Vulkan, keep only `OpenGLES3`

3. **XR Plug-in Management:**
   - ✓ ARCore

### Build
1. Click **Build** or **Build and Run**
2. Save APK
3. Install on Android device with ARCore support

---

## Troubleshooting

### "Select Model" dropdown doesn't update
- Ensure ClassDropdown has the `OnValueChanged` listener connected (Manager.cs does this automatically in `SetupUI()`)

### Models don't appear in AR
- Ensure AR Plane Manager and AR Raycast Manager are on XR Origin
- Check that model prefabs are assigned in Manager
- Look at Console for any error messages

### Touch doesn't work
- Make sure EventSystem exists in scene (should be auto-created with Canvas)
- Check that UI buttons aren't blocking the entire screen

### App crashes on Android
- Check minimum API level is 24+
- Ensure ARCore is enabled in XR settings
- Check adb logcat for specific errors

---

## Hierarchy Should Look Like This

```
SampleScene
├── AR Session
├── XR Origin
│   ├── Camera Offset
│   │   └── Main Camera
│   ├── AR Plane Manager (component)
│   └── AR Raycast Manager (component)
├── IntroCanvas ✓ (active)
│   ├── Background
│   ├── TitleText
│   ├── SubtitleText
│   ├── StartButton
│   └── ExitButton
├── OptionsCanvas ✗ (inactive)
│   ├── Background
│   ├── HeaderText
│   ├── ClassLabel
│   ├── ClassDropdown
│   ├── ModelLabel
│   ├── ModelDropdown
│   ├── SpawnButton
│   ├── BackToIntroButton
│   └── StatusText
├── ARCanvas ✗ (inactive)
│   ├── ARBackButton
│   ├── RemoveModelButton
│   └── InstructionPanel
│       └── ARStatusText
├── GameManager
│   └── Manager (script)
├── Directional Light
├── Global Volume
└── EventSystem
```

---

## Time Estimate

| Task | Time |
|------|------|
| Steps 1-6 (UI Setup) | 2-3 hours |
| Steps 7-10 (Wiring) | 30 min |
| Step 11-12 (Testing) | 1 hour |
| Step 13 (Android Build) | 1-2 hours |
| **Total** | **4-6 hours** |

This leaves you 24+ hours for:
- Replacing placeholder models with real 3D models
- UI polish and animations
- Testing on multiple devices
- Adding more models/classes
