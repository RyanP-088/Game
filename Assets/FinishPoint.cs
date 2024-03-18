    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private HashSet<string> playersInFinishPoint = new HashSet<string>();
    [SerializeField] Canvas transitionCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add this player to the set
            playersInFinishPoint.Add(collision.gameObject.name);

           
            // Check if all three players are at the finish point
            if (playersInFinishPoint.Count == 3)
            {
                // Go to next level
                SceneController.instance.NextLevel();
            }

            if (SceneManager.GetActiveScene().buildIndex == 4 && playersInFinishPoint.Count == 1)
            {
                SceneController.instance.NextLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Remove this player from the set if they leave the finish point
            playersInFinishPoint.Remove(collision.gameObject.name);
        }
    }
}
