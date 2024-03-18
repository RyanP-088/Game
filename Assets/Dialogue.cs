using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    public int index = 0;

    public Transform characterTransform;

    public GameObject contButton;
    private bool isTyping;

    public float wordspeed;
    public bool playerIsClose;

    private PlayerControls controls;
    private Coroutine typingCoroutine;

    private void UpdateDialoguePanelPosition()
    {
        if (characterTransform != null)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(characterTransform.position);

            // Adjust screenPosition as needed (e.g., add an offset)
            screenPosition.y += 200; // Example offset

            // Set the position of the dialogue panel (assuming it's using Screen Space - Overlay)
            RectTransform dialoguePanelRect = dialoguePanel.GetComponent<RectTransform>();
            dialoguePanelRect.position = screenPosition;
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.UI.OpenDialogue.performed += _ => HandleDialogueButton();
    }

    private void OnDisable()
    {
        controls.UI.OpenDialogue.performed -= _ => HandleDialogueButton();
        controls.Disable();
    }

    private void HandleDialogueButton()
    {
        if (!playerIsClose) return;

        if (dialoguePanel.activeInHierarchy)
        {
            NextLine();
        }
        else
        {
            OpenDialogue();
        }
    }

    private void OpenDialogue()
    {
        UpdateDialoguePanelPosition();
        dialoguePanel.SetActive(true);
        StartTypingCoroutine();
    }

    public void NextLine()
    {
        if (isTyping)
        {
            FinishTypingInstantly();
            return;
        }

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = ""; // Clear the text for the next line
            StartTypingCoroutine();
        }
        else
        {
            zeroText(); // This will hide the dialogue box
        }
    }

    private void StartTypingCoroutine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the existing coroutine if it's running
        }
        typingCoroutine = StartCoroutine(Typing());
    }

    private void FinishTypingInstantly()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueText.text = dialogue[index];
        contButton.SetActive(true);
        isTyping = false;
    }

    IEnumerator Typing()
    {
        isTyping = true;

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordspeed);
        }

        FinishTypingInstantly();
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        isTyping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
