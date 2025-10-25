using System;
using System.Collections.Generic;

public class CustomizationEventQueue
{
    private readonly Queue<Action<Action>> eventQueue = new Queue<Action<Action>>();
    private bool isProcessing = false;

    public void Enqueue(Action<Action> newEvent) => eventQueue.Enqueue(newEvent);

    public void Start()
    {
        if (!isProcessing) { RunNext(); }
    }

    private void RunNext()
    {
        if (eventQueue.Count <= 0)
        {
            isProcessing = false;
            return;
        }

        isProcessing = true;
        var next = eventQueue.Dequeue();
        next?.Invoke(RunNext);
    }
}
