using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManger : MonoBehaviour
{
    public static InGameTime clock = new InGameTime();
    float counter = 1;
    private void Update()
    {
        counter -= Time.deltaTime * 60;
        if (counter <= 0)
        {
            counter = 1;
            clock.addSec();
        }
    }
}
public class InGameTime
{
    public float sec, min, hour, days;
    public void addSec()
    {
        sec++;
        calculateTime();
    }
    private void calculateTime()
    {
        if (sec == 60) 
        {
            sec = 0;
            min++;
        }
        if(min == 60)
        {
            min = 0;
            hour++;
        }
        if(hour == 24)
        {
            hour = 0;
            days++;
        }
    }
    public string debugString()
    {
        return $"sec:{sec}, mins:{min},hour:{hour},day:{days}";
    }
}
