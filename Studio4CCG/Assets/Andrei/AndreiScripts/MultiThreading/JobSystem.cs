using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class JobSystem : MonoBehaviour
{
    public Queue<IJob> jobQueue = new Queue<IJob>();
    public List<Task> runningJobs = new List<Task>();
    public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    void Start()
    {
        Task.Run(() => ProcessJobs(), cancellationTokenSource.Token);
    }

    public void ScheduleJob(IJob job)
    {
        lock (jobQueue)
        {
            jobQueue.Enqueue(job);
        }
    }

    private void ProcessJobs()
    {
        while (!cancellationTokenSource.Token.IsCancellationRequested)
        {
            IJob jobToExecute = null;

            lock (jobQueue)
            {
                if (jobQueue.Count > 0)
                {
                    jobToExecute = jobQueue.Dequeue();
                }
            }

            if (jobToExecute != null)
            {
                var task = Task.Run(() => jobToExecute.Execute());
                lock (runningJobs)
                {
                    runningJobs.Add(task);
                }

                task.ContinueWith(t =>
                {
                    lock (runningJobs)
                    {
                        runningJobs.Remove(t);
                    }
                });
            }
        }
    }

    void OnDestroy()
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
    }

    public void WaitForAllJobs()
    {
        Task[] tasksToWait;
        lock (runningJobs)
        {
            tasksToWait = runningJobs.ToArray();
        }

        Task.WaitAll(tasksToWait);
    }
}
