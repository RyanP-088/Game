using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyLogic : MonoBehaviour
{
    [SerializeField] GameObject checkpoint;

    [SerializeField] TimerScript timer;

    [SerializeField] bool scene1;
    [SerializeField] bool scene2;
    [SerializeField] bool scene3;
    [SerializeField] bool testLevel;


    public void respawn(GameObject player)
    {
        player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, checkpoint.transform.position.z);
        timer.penalty();
    }


    public void updateCheckpoint(GameObject cPoint) 
    {
        checkpoint = cPoint;
    }

    public void gameOver()
    {
        if (scene1)
        {
            SceneManager.LoadScene("LevelOne");
        }
        else if (scene2)
        {
            SceneManager.LoadScene("LevelTwo");
        } else if (scene3)
        {
            SceneManager.LoadScene("LevelThree");
        } else if (testLevel)
        {
            SceneManager.LoadScene("TestScene");
        }
    }

}
