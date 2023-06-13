using System.Collections.Generic;
using UnityEngine;

public class PauseManager : IPauseHandler
{
    private readonly List<IPauseHandler> handlers = new();
    public bool IsPaused { get; private set; }

    public void Register(IPauseHandler handler)
    {
        handlers.Add(handler);
    }

    public void Unregister(IPauseHandler handler)
    {
        handlers.Remove(handler);
    }

    public void SetPaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
        IsPaused = isPaused;
        foreach (IPauseHandler handler in handlers)
        {
            handler.SetPaused(isPaused);
        }
    }
}
