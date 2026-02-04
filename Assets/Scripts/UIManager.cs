using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles UI animations and transitions for a polished look
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private CanvasGroup introPanel;
    [SerializeField] private CanvasGroup optionsPanel;
    [SerializeField] private CanvasGroup arPanel;
    
    [Header("Animation Settings")]
    [SerializeField] private float fadeSpeed = 2f;
    
    [Header("Loading Indicator")]
    [SerializeField] private GameObject loadingIndicator;
    [SerializeField] private TMP_Text loadingText;
    
    private CanvasGroup currentPanel;
    private CanvasGroup targetPanel;
    private bool isTransitioning = false;
    
    void Start()
    {
        // Initialize panels
        if (introPanel != null)
        {
            introPanel.alpha = 1;
            introPanel.interactable = true;
            introPanel.blocksRaycasts = true;
            currentPanel = introPanel;
        }
        
        if (optionsPanel != null)
        {
            optionsPanel.alpha = 0;
            optionsPanel.interactable = false;
            optionsPanel.blocksRaycasts = false;
        }
        
        if (arPanel != null)
        {
            arPanel.alpha = 0;
            arPanel.interactable = false;
            arPanel.blocksRaycasts = false;
        }
        
        HideLoading();
    }
    
    void Update()
    {
        if (isTransitioning)
        {
            AnimatePanelTransition();
        }
    }
    
    void AnimatePanelTransition()
    {
        if (currentPanel != null && currentPanel.alpha > 0)
        {
            currentPanel.alpha -= fadeSpeed * Time.deltaTime;
            if (currentPanel.alpha <= 0)
            {
                currentPanel.alpha = 0;
                currentPanel.interactable = false;
                currentPanel.blocksRaycasts = false;
            }
        }
        
        if (targetPanel != null && (currentPanel == null || currentPanel.alpha <= 0))
        {
            targetPanel.alpha += fadeSpeed * Time.deltaTime;
            if (targetPanel.alpha >= 1)
            {
                targetPanel.alpha = 1;
                targetPanel.interactable = true;
                targetPanel.blocksRaycasts = true;
                currentPanel = targetPanel;
                targetPanel = null;
                isTransitioning = false;
            }
        }
    }
    
    public void TransitionToPanel(CanvasGroup panel)
    {
        if (panel == currentPanel || isTransitioning) return;
        
        targetPanel = panel;
        isTransitioning = true;
    }
    
    public void ShowIntro()
    {
        TransitionToPanel(introPanel);
    }
    
    public void ShowOptions()
    {
        TransitionToPanel(optionsPanel);
    }
    
    public void ShowAR()
    {
        TransitionToPanel(arPanel);
    }
    
    public void ShowLoading(string message = "Loading...")
    {
        if (loadingIndicator != null)
        {
            loadingIndicator.SetActive(true);
        }
        if (loadingText != null)
        {
            loadingText.text = message;
        }
    }
    
    public void HideLoading()
    {
        if (loadingIndicator != null)
        {
            loadingIndicator.SetActive(false);
        }
    }
    
    public void UpdateLoadingText(string message)
    {
        if (loadingText != null)
        {
            loadingText.text = message;
        }
    }
}
