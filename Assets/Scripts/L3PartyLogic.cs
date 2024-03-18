using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L3PartyLogic : MonoBehaviour
{
    GameObject stickPoint;
    [SerializeField] GameObject sticky;

    GameObject roidPoint;
    [SerializeField] GameObject roid;

    GameObject smartPoint;
    [SerializeField] GameObject smart;

    [SerializeField] TimerScript timer;

    public void updateStickyCpoint(GameObject stickPoint)
    {
        this.stickPoint = stickPoint;
    }

    public void updateRoidCpoint(GameObject roidPoint)
    {
        this.roidPoint = roidPoint;
    }

    public void updateSmartCpoint(GameObject smartPoint)
    {
        this.smartPoint = smartPoint;
    }

    public void respawn(GameObject player)
    {
        if(player.name == "dude (sticky)")
        {
            sticky.transform.position = new Vector3(stickPoint.transform.position.x, stickPoint.transform.position.y, sticky.transform.position.z);
        } else if(player.name == "dude (roid)")
        {
            roid.transform.position = new Vector3(roidPoint.transform.position.x, roidPoint.transform.position.y, roid.transform.position.z);
        } else if(player.name == "dude(smart)")
        {
            smart.transform.position = new Vector3(smartPoint.transform.position.x, stickPoint.transform.position.y, sticky.transform.position.z);
        }

        timer.penalty();
    }

    public void gameOver()
    {
        SceneManager.LoadScene("LevelThree");
    }

}
