using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Eventbus : Singleton<Eventbus>

{
    private Dictionary<SystemStatus, UnityEvent> eventDictionary;
    private SystemStatusEvent onSystemStateChangedEvent = new SystemStatusEvent();
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        
        Instance.Init();
    }

    private void Init()
    {
        eventDictionary = new Dictionary<SystemStatus, UnityEvent>();
    }
    /// <summary>
    /// add a method as a listener if the method want to get notified each time when the status changes
    /// </summary>
    /// <param name="systemStateListeningMethod"></param>
    public void StartListenSystemStatusChange (UnityAction<SystemStatus> systemStateListeningMethod)
    {
        onSystemStateChangedEvent.AddListener(systemStateListeningMethod);
    }

    public void StopListeningStatusChange (UnityAction<SystemStatus> systemStateListeningMethod)
    {
        onSystemStateChangedEvent.RemoveListener(systemStateListeningMethod);
    }
    /// <summary>
    /// Add a method as a listener to the particualar event
    /// </summary>
    /// <param name="eventName"> this is the enumerator value</param>
    /// <param name="method"> method you want to add </param>
    public void StartListening (SystemStatus eventName, UnityAction method)
    {
        UnityEvent testEvent = null;

      if(  eventDictionary.TryGetValue(eventName, out testEvent))
        {
            //then the dictionary has specific event now our job is to add unity action as a listenr to the
            //event instance in dictionary
            testEvent.AddListener(method);
        }
      else
        {
            //if paticular event is not (initialised or added to dicitionary before) initialize new event add to dictiionary ands add unity action as listener to the event
            testEvent = new UnityEvent();
            eventDictionary.Add(eventName, testEvent);
            testEvent.AddListener(method);
        }
    }

    public void StopListening(SystemStatus eventName, UnityAction method)
    {
        UnityEvent testEvent = null;
      if(eventDictionary.TryGetValue(eventName, out testEvent))
        {
            testEvent.RemoveListener(method);
        }
    }
    /// <summary>
    /// To trigger event based on event name 
    /// </summary>
    /// <param name="EventName"></param>
    public void TriggerEvent(SystemStatus EventName)
    {
        //inform the method that listening all the status changes with current status name
        onSystemStateChangedEvent.Invoke(EventName);

        UnityEvent testEvent = null;
        if (eventDictionary.TryGetValue(EventName, out testEvent))
            {
            testEvent.Invoke();

        }
    }
   
}
