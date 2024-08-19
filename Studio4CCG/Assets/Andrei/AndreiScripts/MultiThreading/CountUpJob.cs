using System.Threading;
using System;
using UnityEngine;


public class CountUpJob : IJob
{
    private string name;
    public CountUpJob(string name)
    {
        this.name = name;
    }

    public void Execute()
    {
        CountUp(name);
    }

    public void CountUp(string name)
    {
        for (int i = 0; i <= 10; i++)
        {
            Debug.LogError("Timer #2 : " + i + " seconds");
            Thread.Sleep(1000);
        }
        Debug.LogError("Timer #2 is complete!");
    }
}
