using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelMenu : MonoBehaviour
{
    public CanvasGroup previousPanelCanvasGroup; // Assign the Canvas Group of the previous panel
    public GameObject levelsPanel;
    public GameObject firstSelectedButtonInLevels;

    public void OpenLevelSelect()
    {
        // Disable interaction with the previous panel
        if (previousPanelCanvasGroup != null)
        {
            previousPanelCanvasGroup.interactable = false;
            previousPanelCanvasGroup.blocksRaycasts = false;
        }

        // Activate the levels panel
        if (levelsPanel != null)
        {
            levelsPanel.SetActive(true);
        }

        // Set the first selected button in the levels panel
        SetInitialButtonFocus();
    }

    private void SetInitialButtonFocus()
    {
        if (firstSelectedButtonInLevels != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButtonInLevels);
        }
    }

    public void OpenLevel(int levelId)
    {
        //string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelId);
    }
}
