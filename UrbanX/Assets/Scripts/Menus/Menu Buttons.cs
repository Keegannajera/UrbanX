using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private TMP_Text _text;
    private Button _button;

    void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();
    }

    // Pointer Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = Color.black;
        if (_button.colors.normalColor == _button.colors.pressedColor)
        {
            _text.color = Color.white;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _text.color = Color.white;
    }

    // Button click methods
    public void NewGameConfirmed()
    {
        
        Debug.Log("Creating new game...");
    }

    public void LoadGameConfirmed()
    {
        Debug.Log("The load button doesn't work as of now.");
    }

    public void CreditsConfirmed()
    {
        Debug.Log("The credits button doesn't work as of now.");
    }

    public void QuitGameConfirmed()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
