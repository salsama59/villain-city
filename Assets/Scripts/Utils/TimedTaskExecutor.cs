using System;
using System.Collections.Generic;
using UnityEngine;

public class TimedTaskExecutor
{

    private Action action;
    private float timer;
    private bool isDestroyed;
    private GameObject timedTaskExecutorGameObject;

    private TimedTaskExecutor(Action action, float timer, GameObject timedTaskExecutorGameObject)
    {
        this.action = action;
        this.timer = timer;
        this.isDestroyed = false;
        this.timedTaskExecutorGameObject = timedTaskExecutorGameObject;
    }

    public static TimedTaskExecutor Create(Action action, float timer )
    {
        GameObject gameObject = new GameObject("TimedTaskExecutor", typeof(MonobehaviourHook));
        TimedTaskExecutor timedTaskExecutor = new TimedTaskExecutor(action, timer, gameObject);
        gameObject.GetComponent<MonobehaviourHook>().onUpdate = timedTaskExecutor.Update;
        return timedTaskExecutor;
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                action();
                this.DestroyTimedTask();
            }
        }
    }

    private void DestroyTimedTask()
    {
        this.isDestroyed = true;
        UnityEngine.Object.Destroy(this.timedTaskExecutorGameObject);
    }
}
