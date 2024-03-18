using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Grounded.Pause.performed += _ => TogglePause();
    }

    private void OnEnable()
    {
        controls.Grounded.Enable();
    }

    private void OnDisable()
    {
        controls.Grounded.Disable();
    }

    private void TogglePause()
    {
        if (pauseMenu.activeSelf)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pauses the game physics and time-related actions.

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(default(GameObject));
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resumes normal time flow.
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}
