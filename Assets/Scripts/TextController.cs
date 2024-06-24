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
    private bool isStart = false;

    private void Awake()
    {
        timeController = new TimeController(Duration, dotTexts.Length);
        timeController.Oninterrupt += TextStart;
        Oninterrupt += TextStart;
    }
    
    void Update()
    {
        if (!isStart)
        {
            return;
        }

        timeController.UpdateTime();
    }

    public void TextStart()
    {
        text.text = dotTexts[i].text;
        i++;
    }
    
    public void StartText()
    {
        isStart = true;
    }
}
