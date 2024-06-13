using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController
{
    private int duration = 0;
    private int count = 0;
    private float time = 0;
    public Action Oninterrupt;
    
    private int i = 0;
    
    public TimeController(int duration, int count)
    {
        this.duration = duration;
        this.count = count;
        time = duration;
    }
    
    public void UpdateTime()
    {
        time += Time.deltaTime;
        
        if (time >= duration)
        {
            time = 0;
            if (i < count)
            {
                i++;
                Oninterrupt.Invoke();
            }
        }
    }
}
