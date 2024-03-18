using UnityEngine;
public class FPSCapper : MonoBehaviour
{
    [SerializeField] private int targetFPS = 60;
    private void Awake() { Application.targetFrameRate = targetFPS; }
}