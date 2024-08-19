using System.Threading;
using System;
using UnityEngine;

public class CountDownJob : IJob
{
    private string name;
    public CountDownJob(string name)
    {
        this.name = name;
    }

    public void Execute()
    {
        CountDown(name);
    }

    public void CountDown(string name)
    {
        for (int i = 10; i >= 0; i--)
        {
            Debug.LogError("Timer #1 : " + i + " seconds");
            Thread.Sleep(1000);
        }
        Debug.LogError("Timer #1 is complete!");
    }
}
