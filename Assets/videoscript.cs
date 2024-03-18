using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoscript : MonoBehaviour
{

    VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;


    }


    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);//the scene that you want to load after the video has ended.
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene(6);//the scene that you want to load after the video has ended.
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            SceneManager.LoadScene(0);//the scene that you want to load after the video has ended.
        }


    }
}
