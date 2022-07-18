using System;
using GXPEngine;
//------------------------------------------------
//                  Timer Class   
//------------------------------------------------
public class Timer : GameObject
{

    private int _time;
    private Action _onTimeout;
    //------------------------------------------------
    //                  Timer()   
    //------------------------------------------------
    public Timer(int time, Action onTimeout)
    {
        _time = time;
        _onTimeout = onTimeout;
    }
    //------------------------------------------------
    //                  Update()   
    //------------------------------------------------
    public void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _onTimeout();
            Destroy();
        }
    }
}