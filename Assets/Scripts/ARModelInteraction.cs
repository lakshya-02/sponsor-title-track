using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARModelInteraction : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private bool enableRotation = true;
    
    [Header("Scale Settings")]
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float scaleSpeed = 0.01f;
    [SerializeField] private bool enableScale = true;
    
    [Header("Auto Rotate")]
    [SerializeField] private bool autoRotate = false;
    [SerializeField] private float autoRotateSpeed = 20f;
    
    private Vector2 previousTouchPosition;
    private float previousPinchDistance;
    private bool isRotating = false;
    private bool isScaling = false;
    
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
        // Auto rotation
        if (autoRotate)
        {
            transform.Rotate(Vector3.up, autoRotateSpeed * Time.deltaTime, Space.World);
        }
        
        HandleTouchInput();
        
        #if UNITY_EDITOR
        HandleMouseInput();
        #endif
    }
    
    void HandleTouchInput()
    {
        if (Touch.activeTouches.Count == 1 && enableRotation)
        {
            // Single finger rotation
            Touch touch = Touch.activeTouches[0];
            
            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                previousTouchPosition = touch.screenPosition;
                isRotating = true;
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved && isRotating)
            {
                Vector2 delta = touch.screenPosition - previousTouchPosition;
                float rotationX = delta.x * rotationSpeed;
                transform.Rotate(Vector3.up, -rotationX, Space.World);
                previousTouchPosition = touch.screenPosition;
            }
            else if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                isRotating = false;
            }
        }
        else if (Touch.activeTouches.Count == 2 && enableScale)
        {
            // Two finger pinch to scale
            Touch touch0 = Touch.activeTouches[0];
            Touch touch1 = Touch.activeTouches[1];
            
            float currentPinchDistance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);
            
            if (touch0.phase == UnityEngine.InputSystem.TouchPhase.Began || touch1.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                previousPinchDistance = currentPinchDistance;
                isScaling = true;
            }
            else if (isScaling)
            {
                float pinchDelta = currentPinchDistance - previousPinchDistance;
                float scaleChange = pinchDelta * scaleSpeed;
                
                Vector3 newScale = transform.localScale + Vector3.one * scaleChange;
                newScale = Vector3.Max(newScale, Vector3.one * minScale);
                newScale = Vector3.Min(newScale, Vector3.one * maxScale);
                
                transform.localScale = newScale;
                previousPinchDistance = currentPinchDistance;
            }
        }
        else
        {
            isRotating = false;
            isScaling = false;
        }
    }
    
    void HandleMouseInput()
    {
        // Mouse drag for rotation (Editor testing)
        if (Mouse.current != null && Mouse.current.leftButton.isPressed && enableRotation)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            float rotationX = mouseDelta.x * rotationSpeed * 0.5f;
            transform.Rotate(Vector3.up, -rotationX, Space.World);
        }
        
        // Scroll wheel for scale (Editor testing)
        if (enableScale && Mouse.current != null)
        {
            float scroll = Mouse.current.scroll.ReadValue().y * 0.001f;
            if (Mathf.Abs(scroll) > 0.0001f)
            {
                Vector3 newScale = transform.localScale + Vector3.one * scroll;
                newScale = Vector3.Max(newScale, Vector3.one * minScale);
                newScale = Vector3.Min(newScale, Vector3.one * maxScale);
                transform.localScale = newScale;
            }
        }
    }
    
    // Public methods for UI buttons
    public void ToggleAutoRotate()
    {
        autoRotate = !autoRotate;
    }
    
    public void SetAutoRotate(bool value)
    {
        autoRotate = value;
    }
    
    public void ResetScale()
    {
        transform.localScale = Vector3.one;
    }
    
    public void ScaleUp()
    {
        Vector3 newScale = transform.localScale * 1.2f;
        newScale = Vector3.Min(newScale, Vector3.one * maxScale);
        transform.localScale = newScale;
    }
    
    public void ScaleDown()
    {
        Vector3 newScale = transform.localScale * 0.8f;
        newScale = Vector3.Max(newScale, Vector3.one * minScale);
        transform.localScale = newScale;
    }
}
