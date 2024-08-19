using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountMultiTest : MonoBehaviour
{
    public JobSystem jobSystem;

    void Start()
    {
        jobSystem.ScheduleJob(new CountDownJob("Timer #1"));
        jobSystem.ScheduleJob(new CountUpJob("Timer #2"));

        jobSystem.WaitForAllJobs();
        Debug.Log("All jobs completed.");
    }
}
