using System;
using System.Collections.Generic;
using SashUnity.EventSystem.Interface;
using UnityEngine;

namespace SashUnity.EventSystem.Service
{
    /// <summary>
    /// for better usage it's better to define a static class with int const variables for handling ids or create a enum for that
    /// Each event need a specific class or struct for adding any type of data to event 
    /// </summary>
    public class EventService : IEventService
    {
        private readonly Dictionary<int, List<Delegate>> _events = new();
        private Delegate _lastFunc;

        public void Subscribe<T>(int eventId, Action<T> eventClass)
        {
            if (!_events.ContainsKey(eventId))
            {
                _events.Add(eventId, new List<Delegate>());
            }

            _events[eventId].Add(eventClass);
        }

        public void RemoveEvent<T>(int eventId, Action<T> eventClass)
        {
            if (_events.ContainsKey(eventId))
            {
                _events[eventId].Remove(eventClass);
            }
        }

        public void Fire<T>(int eventId, T eventClass)
        {
            if (_events.ContainsKey(eventId))
            {
                var events = _events[eventId];
                foreach (var func in events)
                {
                    _lastFunc = func;
                    try
                    {
                        func.DynamicInvoke(eventClass);
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"Bug in EventId:{eventId} Event : {e.Message} \n " +
                                  $"{_lastFunc.Method.Name} \n " +
                                  $"{_lastFunc.Method.Module} \n " +
                                  $"{_lastFunc.GetType()} \n " +
                                  $"{_lastFunc.Target}");
                    }
                }
            }
        }
    }
}