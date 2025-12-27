using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Observable<T>
{
    [SerializeField] private T _value;
    public List<Action<T>> _observers = new List<Action<T>>();

    public T Value
    {
        get { return _value; }
        set
        {
            //if (!EqualityComparer<T>.Default.Equals(_value, value))
            //{
            _value = value;
            NotifyObservers();
            //}
        }
    }

    public void Subscribe(Action<T> observer)
    {
        _observers.Add(observer);
    }

    public void Unsubsribe(Action<T> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers.ToList())
        {
            observer(_value);
        }
        /*
                for (int i = 0; i < _observers.Count; i++)
                {
                    _observers[i](_value);
                }*/
    }

}
