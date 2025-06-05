using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text _text;
    private Button _button;

    /// For use with the main menu. If you won't be dealing with this, leave the fields blank.
    [SerializeField] private GameObject _mainMenuReference;
    [SerializeField] private GameObject _creditsMenuReference;
    [SerializeField] private SaveManager _saveManager;

    void OnEnable()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _text.color = Color.white;
    }

    void Start()
    {
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

    // Button click methods
    public void NewGameConfirmed()
    {
        SaveManager.Instance.StartNewGame(true);
        SceneManager.LoadScene("Apartment");
    }

    public void LoadGameConfirmed()
    {
        SaveManager.Instance.StartNewGame(false);
        SceneManager.LoadScene("Apartment");
        Debug.Log("The load button doesn't work as of now.");
    }

    public void SettingsConfirmed()
    {
        Debug.Log("The settings button doesn't work as of now.");
    }

    public void CreditsConfirmed()
    {
        OpenCreditsScreen();
    }

    public void BackButton()
    {
        CloseCreditsScreen();
    }

    public void QuitGameConfirmed()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    // Private methods
    private void OpenCreditsScreen()
    {
        _mainMenuReference.SetActive(false);
        _creditsMenuReference.SetActive(true);
    }

    private void CloseCreditsScreen()
    {
        _mainMenuReference.SetActive(true);
        _creditsMenuReference.SetActive(false);
    }
}
