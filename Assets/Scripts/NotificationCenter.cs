using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class NotificationCenter
    {
        private static NotificationCenter _instance;
#pragma warning disable IDE0044
        private Dictionary<string, List<Action<object[]>>> _observers;//修改string -》 enum   List<Action<object[]>>修改为接口
#pragma warning restore IDE0044

        private NotificationCenter()
        {
            _observers = new Dictionary<string, List<Action<object[]>>>();
        }
        public static NotificationCenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationCenter();
                }

                return _instance;
            }
        }

        public void AddObserver(string notificationName,Action<object[]> observer)
        {
            if (!_observers.ContainsKey(notificationName))
            {
                _observers[notificationName] = new List<Action<object[]>>();
               // Debug.Log("add success"+ notificationName);
            }
            //防止添加重复的回调
            if (!_observers[notificationName].Contains(observer))
            {
                _observers[notificationName].Add(observer);
              //  Debug.Log("add success" + notificationName);
            }
            else
            {
                Debug.Log("already have recall fun");
            }
        }

        public void RemoveObserver(string notificationName, Action<object[]> observer)
        {
            if (_observers[notificationName].Contains(observer))
            {
                _observers[notificationName].Remove(observer);
            }
            else
            {
                Debug.Log(notificationName+"do not have observer");
            }
        }

        public void PostNotification(string notificationName,object[] data=null)
        {
            if (_observers.ContainsKey(notificationName))
            {
                foreach (var observerAction in _observers[notificationName])
                {
                    //Debug.Log("call success" + notificationName);
                    observerAction(data);
                }
            }
        }
    }
}
