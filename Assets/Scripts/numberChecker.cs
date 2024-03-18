using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberChecker : MonoBehaviour
{

    public Button myButton;
    public Sprite zero;
    public Sprite one;
    private Boolean n = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void changeNumber()
    {
        Image buttonImage = myButton.GetComponent<Image>();
        if (!n) buttonImage.sprite = one;
        else buttonImage.sprite = zero;

        n = !n;
    }
}
