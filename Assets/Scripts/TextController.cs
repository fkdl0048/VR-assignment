using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public DotText[] dotTexts;
    public Action Oninterrupt;
    
    public int Duration;
    
    private int i = 0;
    private TimeController timeController;

    private void Awake()
    {
        timeController = new TimeController(Duration, dotTexts.Length);
        timeController.Oninterrupt += TextStart;
        Oninterrupt += TextStart;
    }
    
    void Update()
    {
        timeController.UpdateTime();
    }

    public void TextStart()
    {
        text.text = dotTexts[i].text;
        i++;
    }
}
