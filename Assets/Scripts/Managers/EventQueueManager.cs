using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    public static EventQueueManager instance;

    private Queue<ICommand> _events = new Queue<ICommand>();
    public Queue<ICommand> Events => _events;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    private void Update()
    {
        while (_events.Count > 0)
        {
            _events.Dequeue().Execute();
        }
    }

    public void AddCommand(ICommand command)
    {
        Events.Enqueue(command);
    }
}