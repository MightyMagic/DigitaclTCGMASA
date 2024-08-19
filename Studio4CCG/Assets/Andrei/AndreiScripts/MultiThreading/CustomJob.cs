using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomJob : IJob
{
    private Action jobAction;

    public CustomJob(Action jobAction)
    {
        this.jobAction = jobAction;
    }

    public void Execute()
    {
        if(jobAction != null)
        {
            jobAction.Invoke();
        }
    }
}
