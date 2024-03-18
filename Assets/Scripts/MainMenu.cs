using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject defaultButton; // Assign this in the Inspector
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.UI.Submit.performed += _ => OnSubmit();
        //controls.UI.Move.performed += OnNavigate;
    }

    private void OnSubmit()
    {
        // Check which button is currently selected and call the respective method
        if (EventSystem.current.currentSelectedGameObject == defaultButton)
        {
            PlayGame();
        }

    }

    private void OnEnable()
    {
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.UI.Disable();
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    public void PlayGame()
    {
        //index corresponds to file->buildSettings index
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
