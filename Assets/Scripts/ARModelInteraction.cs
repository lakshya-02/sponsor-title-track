using UnityEngine;

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
        if (Input.touchCount == 1 && enableRotation)
        {
            // Single finger rotation
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
                isRotating = true;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                Vector2 delta = touch.position - previousTouchPosition;
                float rotationX = delta.x * rotationSpeed;
                transform.Rotate(Vector3.up, -rotationX, Space.World);
                previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isRotating = false;
            }
        }
        else if (Input.touchCount == 2 && enableScale)
        {
            // Two finger pinch to scale
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            
            float currentPinchDistance = Vector2.Distance(touch0.position, touch1.position);
            
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
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
        if (Input.GetMouseButton(0) && enableRotation)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * 10f;
            transform.Rotate(Vector3.up, -rotationX, Space.World);
        }
        
        // Scroll wheel for scale (Editor testing)
        if (enableScale)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
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
