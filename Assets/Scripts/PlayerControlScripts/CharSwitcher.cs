using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// This class is responsible for switching between player characters in the game and toggling their AI behavior.
/// </summary>
public class CharSwitcher : MonoBehaviour
{
    [SerializeField]
    private PlayerController smart, roid, sticky;
    [SerializeField]
    private CameraFollow cameraFollow;
    [SerializeField] 
    private bool roidEnabled = true;
    [SerializeField] 
    private bool stickyEnabled = true;
    [SerializeField] 
    private FriendController roidAI, stickyAI;
    [SerializeField] 
    private float leftOffset = 0.5f, rightOffset = 0.5f;
    [SerializeField] 
    private bool roidFollowEnabled = true, stickyFollowEnabled = true;
    [SerializeField] 
    private float teleportAfter = 8f, teleportDistance = 7f;
    private int currentRat = 1;

    private PlayerControls playerControls;

    /// <summary>
    /// Returns the current rat based on the value of currentRat.
    /// </summary>
    /// <returns>The current rat.</returns>
    public int GetCurrentRat() => currentRat;

    /// <summary>
    /// Toggles the AI behavior of a given FriendController based on the input character.
    /// </summary>
    /// <param name="ai">The FriendController whose AI behavior will be toggled.</param>
    /// <param name="sr">The character representing the AI behavior to toggle ('s' for sticky, 'r' for roid).</param>
    private void ToggleAI(FriendController ai, char sr)
    {
        if (currentRat != 1)
            return;
        switch (sr)
        {
            case 's':
                if (!stickyEnabled)
                    return;
                ai.SetFollowEnabled(stickyFollowEnabled = !stickyFollowEnabled);
                //ai.SetTeleportEnabled(stickyFollowEnabled);
                ai.SetTargetOffset(new Vector2(-leftOffset, 0));
                break;
            case 'r':
                if (!roidEnabled)
                    return;
                ai.SetFollowEnabled(roidFollowEnabled = !roidFollowEnabled);
                //ai.SetTeleportEnabled(roidFollowEnabled);
                ai.SetTargetOffset(new Vector2(rightOffset, 0));
                break;
            default: break;
        }
        ai.SetTarget(smart.transform);
        ai.SetTeleportDistance(teleportDistance);
        ai.SetTeleportTime(teleportAfter);
    }

    /// <summary>
    /// Toggles both ai.
    /// </summary>
    private void ToggleAI()
    {
        ToggleAI(roidAI, 'r');
        ToggleAI(stickyAI, 's');
    }

    /// <summary>
    /// Swaps the player character to the smart rat.
    /// </summary>
    private void SwapToSmart()
    {

        currentRat = 1;
        smart.enabled = true;

        roid.enabled = false;
        roidAI.SetFollowEnabled(roidFollowEnabled);
        //roidAI.SetTeleportEnabled(roidFollowEnabled);

        sticky.enabled = false;
        stickyAI.SetFollowEnabled(stickyFollowEnabled);
        //stickyAI.SetTeleportEnabled(stickyFollowEnabled);

        cameraFollow.SetAciveTarget(0);
    }

    /// <summary>
    /// Toggles the roidDisabled boolean and disables the roidAI component.
    /// </summary>
    public void ToggleRoidEnabled()
    {
        roidEnabled = !roidEnabled;
        roidAI.enabled = roidEnabled;
    }

    /// <summary>
    /// Swaps the player character to the roid rat.
    /// </summary>
    private void SwapToRoid()
    {
        if (!roidEnabled)
            return;

        currentRat = 2;

        smart.enabled = false;

        roid.enabled = true;
        roidAI.SetFollowEnabled(roidFollowEnabled = false);
        //roidAI.SetTeleportEnabled(false);

        stickyAI.SetFollowEnabled(stickyFollowEnabled = false);
        //stickyAI.SetTeleportEnabled(false);
        sticky.enabled = false;

        cameraFollow.SetAciveTarget(1);
    }

    /// <summary>
    /// Toggles the stickyEnabled boolean and disables the stickyAI component.
    /// </summary>
    public void ToggleStickyEnabled()
    {
        stickyEnabled = !stickyEnabled;
        stickyAI.enabled = stickyEnabled;
    }

    /// <summary>
    /// Swaps the player character to the sticky rat.
    /// </summary>
    private void SwapToSticky()
    {
        // Cancel swap if sticky rat is disabled
        if (!stickyEnabled)
            return;

        // Set current rat
        currentRat = 3;

        // Disable smart rat
        smart.enabled = false;

        // Disable roid rat
        roid.enabled = false;
        roidAI.SetFollowEnabled(roidFollowEnabled = false);
        //roidAI.SetTeleportEnabled(false);
        
        // Enable player controls for sticky rat
        sticky.enabled = true;
        stickyAI.SetFollowEnabled(stickyFollowEnabled = false);
        //stickyAI.SetTeleportEnabled(stickyFollowEnabled);

        // Have camera follow stick rat
        cameraFollow.SetAciveTarget(2);
    }

    /// <summary>
    /// Swaps the player character to the given target rat.
    /// </summary>
    /// <param name="target">The target rat to swap to (1 for smart, 2 for roid, 3 for sticky).</param>
    private void SwapRat(int target)
    {
        if (target == 1)
        {
            SwapToSmart();
        }
        else if (target == 2)
        {
            SwapToRoid();
        }
        else if (target == 3)
        {
            SwapToSticky();
        }
    }

    /// <summary>
    /// Swaps the player character to the left adjacent rat.
    /// </summary>
    private void SwapLeft()
    {
        if (!roidEnabled && currentRat == 3)
        {
            return;
        }

        currentRat -= 1;
        if (currentRat < 1)
            currentRat = 3;

        //Debug.Log("Current Rat: " + currentRat + " Sticky Enabled: " + stickyEnabled + " Roid Enabled: " + roidEnabled + "");
        SwapRat(currentRat);
    }

    /// <summary>
    /// Swaps the player character to the right adjaent rat.
    /// </summary>
    private void SwapRight()
    {
        if (!roidEnabled && currentRat == 1)
        {
            return;
        }

        currentRat += 1;
        if (currentRat > 3)
            currentRat = 1;

        //Debug.Log("Current Rat: " + currentRat + " Sticky Enabled: " + stickyEnabled + " Roid Enabled: " + roidEnabled + "");
        SwapRat(currentRat);
    }

    private void Awake() => playerControls = new PlayerControls();

    private void OnEnable() => playerControls.Enable();

    private void OnDisable() => playerControls.Disable();

    void Start()
    {
        // Swap to smart rat
        SwapRat(1);

        // Set AI behavior
        stickyAI.enabled = stickyEnabled;
        roidAI.enabled = roidEnabled;


        // Set up input actions
        //playerControls.Grounded.SwapLeft.performed += _ => SwapLeft();
        //playerControls.Grounded.SwapRight.performed += _ => SwapRight();

        playerControls.Grounded.SwapSmart.performed += _ => SwapRat(1);
        playerControls.Grounded.SwapRoid.performed += _ => SwapRat(2);
        playerControls.Grounded.SwapSticky.performed += _ => SwapRat(3);

        playerControls.Grounded.ToggleAI.performed += _ => ToggleAI();

        //playerControls.Grounded.ToggleLeftFollow.performed += _ => ToggleAI(stickyAI, 's');
        //playerControls.Grounded.ToggleRightFollow.performed += _ => ToggleAI(roidAI, 'r');
    }
}
