using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using TMPro;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Manager : MonoBehaviour
{
    [Header("AR Components")]
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private ARPlaneManager arPlaneManager;
    
    [Header("UI Panels")]
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject arCanvas;
    
    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown classDropdown;
    [SerializeField] private TMP_Dropdown modelDropdown;
    
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button spawnButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button removeModelButton;
    
    [Header("Status Text")]
    [SerializeField] private TMP_Text statusText;
    
    [Header("Model Prefabs - Class 6")]
    [SerializeField] private GameObject volcanoModel;      // Volcano - Geography/Science
    
    [Header("Model Prefabs - Class 7")]
    [SerializeField] private GameObject brainModel;        // Brain - Biology
    
    [Header("Model Prefabs - Class 8")]
    [SerializeField] private GameObject skeletonModel;     // Skeleton - Biology/Anatomy
    
    // Model data structure
    [System.Serializable]
    public class ModelData
    {
        public string name;
        public GameObject prefab;
        public int classLevel;
    }
    
    private List<ModelData> allModels = new List<ModelData>();
    private List<ModelData> filteredModels = new List<ModelData>();
    private GameObject currentSpawnedModel;
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    private bool isPlacementMode = false;
    
    void Start()
    {
        InitializeModels();
        SetupUI();
        ShowIntroScreen();
    }
    
    void InitializeModels()
    {
        // Class 6 models - Geography/Earth Science
        if (volcanoModel != null)
            allModels.Add(new ModelData { name = "Volcano", prefab = volcanoModel, classLevel = 6 });
        
        // Class 7 models - Biology
        if (brainModel != null)
            allModels.Add(new ModelData { name = "Human Brain", prefab = brainModel, classLevel = 7 });
        
        // Class 8 models - Anatomy
        if (skeletonModel != null)
            allModels.Add(new ModelData { name = "Human Skeleton", prefab = skeletonModel, classLevel = 8 });
    }
    
    void SetupUI()
    {
        // Setup button listeners
        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);
        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitClicked);
        if (spawnButton != null)
            spawnButton.onClick.AddListener(OnSpawnClicked);
        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);
        if (removeModelButton != null)
            removeModelButton.onClick.AddListener(OnRemoveModelClicked);
        
        // Setup dropdown listeners
        if (classDropdown != null)
        {
            classDropdown.onValueChanged.AddListener(OnClassChanged);
            SetupClassDropdown();
        }
        if (modelDropdown != null)
        {
            modelDropdown.onValueChanged.AddListener(OnModelChanged);
        }
    }
    
    void SetupClassDropdown()
    {
        classDropdown.ClearOptions();
        List<string> classOptions = new List<string> { "Select Class", "Class 6", "Class 7", "Class 8" };
        classDropdown.AddOptions(classOptions);
    }
    
    void OnClassChanged(int index)
    {
        modelDropdown.ClearOptions();
        filteredModels.Clear();
        
        if (index == 0)
        {
            modelDropdown.AddOptions(new List<string> { "Select Model" });
            return;
        }
        
        int selectedClass = index + 5; // index 1 = class 6, index 2 = class 7, etc.
        
        foreach (var model in allModels)
        {
            if (model.classLevel == selectedClass)
            {
                filteredModels.Add(model);
            }
        }
        
        List<string> modelNames = new List<string> { "Select Model" };
        foreach (var model in filteredModels)
        {
            modelNames.Add(model.name);
        }
        
        modelDropdown.AddOptions(modelNames);
        UpdateStatus($"Class {selectedClass} selected. Choose a model.");
    }
    
    void OnModelChanged(int index)
    {
        if (index > 0 && index <= filteredModels.Count)
        {
            var selectedModel = filteredModels[index - 1];
            UpdateStatus($"Selected: {selectedModel.name}. Tap 'View in AR' to place.");
        }
    }
    
    void ShowIntroScreen()
    {
        if (introCanvas != null) introCanvas.SetActive(true);
        if (optionsCanvas != null) optionsCanvas.SetActive(false);
        if (arCanvas != null) arCanvas.SetActive(false);
        SetARPlaneVisibility(false);
    }
    
    void ShowOptionsScreen()
    {
        if (introCanvas != null) introCanvas.SetActive(false);
        if (optionsCanvas != null) optionsCanvas.SetActive(true);
        if (arCanvas != null) arCanvas.SetActive(false);
        SetARPlaneVisibility(false);
        
        // Reset dropdowns
        if (classDropdown != null) classDropdown.value = 0;
        if (modelDropdown != null)
        {
            modelDropdown.ClearOptions();
            modelDropdown.AddOptions(new List<string> { "Select Model" });
        }
        
        UpdateStatus("Select a class and model to view in AR.");
    }
    
    void ShowARScreen()
    {
        if (introCanvas != null) introCanvas.SetActive(false);
        if (optionsCanvas != null) optionsCanvas.SetActive(false);
        if (arCanvas != null) arCanvas.SetActive(true);
        SetARPlaneVisibility(true);
        isPlacementMode = true;
        
        UpdateStatus("Point camera at a flat surface, then tap to place model.");
    }
    
    void SetARPlaneVisibility(bool visible)
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.enabled = visible;
            foreach (var plane in arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(visible);
            }
        }
    }
    
    void OnStartClicked()
    {
        ShowOptionsScreen();
    }
    
    void OnExitClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    void OnSpawnClicked()
    {
        if (modelDropdown.value == 0 || filteredModels.Count == 0)
        {
            UpdateStatus("Please select a model first!");
            return;
        }
        
        ShowARScreen();
    }
    
    void OnBackClicked()
    {
        RemoveCurrentModel();
        isPlacementMode = false;
        ShowOptionsScreen();
    }
    
    void OnRemoveModelClicked()
    {
        RemoveCurrentModel();
        UpdateStatus("Model removed. Tap on a surface to place again.");
    }
    
    void RemoveCurrentModel()
    {
        if (currentSpawnedModel != null)
        {
            Destroy(currentSpawnedModel);
            currentSpawnedModel = null;
        }
    }
    
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    
    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    
    void Update()
    {
        if (!isPlacementMode) return;
        
        // Handle touch input for model placement (New Input System)
        if (Touch.activeTouches.Count > 0)
        {
            Touch touch = Touch.activeTouches[0];
            
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                // Check if touch is over UI
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touch.touchId))
                    return;
                
                TryPlaceModel(touch.screenPosition);
            }
        }
        
        // Mouse input for editor testing (New Input System)
        #if UNITY_EDITOR
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
            
            TryPlaceModel(Mouse.current.position.ReadValue());
        }
        #endif
    }
    
    void TryPlaceModel(Vector2 screenPosition)
    {
        if (arRaycastManager == null) return;
        
        if (arRaycastManager.Raycast(screenPosition, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose;
            
            // Get selected model
            int modelIndex = modelDropdown.value - 1;
            if (modelIndex < 0 || modelIndex >= filteredModels.Count) return;
            
            var selectedModel = filteredModels[modelIndex];
            if (selectedModel.prefab == null)
            {
                UpdateStatus("Model prefab not assigned!");
                return;
            }
            
            // Remove existing model if any
            RemoveCurrentModel();
            
            // Spawn new model
            currentSpawnedModel = Instantiate(selectedModel.prefab, hitPose.position, hitPose.rotation);
            
            // Make model face the camera
            Vector3 lookPos = Camera.main.transform.position;
            lookPos.y = currentSpawnedModel.transform.position.y;
            currentSpawnedModel.transform.LookAt(lookPos);
            currentSpawnedModel.transform.Rotate(0, 180, 0);
            
            UpdateStatus($"{selectedModel.name} placed! Tap elsewhere to reposition.");
        }
    }
    
    void UpdateStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
        Debug.Log($"[NCERT AR] {message}");
    }
}
