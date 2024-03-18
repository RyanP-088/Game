using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalActivation : MonoBehaviour
{
    [SerializeField] GameObject smart, stickyButton, roidButton;
    [SerializeField] CharSwitcher cs;

    private int stickyAcivated = 0, roidActivated = 0;
    private Collider2D smartCol, stickyButtonCol, roidButtonCol;

    private void collidingWithSticky()
    {
        if (smartCol.IsTouching(stickyButtonCol) && stickyAcivated == 0)
        {
            stickyAcivated = 1;
            cs.ToggleStickyEnabled();
        }
    }

    private void CollidingWithRoid()
    {
            if (smartCol.IsTouching(roidButtonCol) && roidActivated == 0)
            {
                roidActivated = 1;
                cs.ToggleRoidEnabled();
            }
    }

    void Start()
    {
        smartCol = smart.GetComponent<Collider2D>();
        stickyButtonCol = stickyButton.GetComponent<Collider2D>();
        roidButtonCol = roidButton.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        collidingWithSticky();
        CollidingWithRoid();
    }
}
