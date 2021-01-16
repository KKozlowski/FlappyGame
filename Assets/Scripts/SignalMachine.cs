using System.Collections;
using System.Collections.Generic;
using System;

namespace Flapper
{
    public static class SignalMachine
    {
        private static Dictionary<Type, List<object>> listeners = new Dictionary<Type, List<object>>();

        public static void AddListener<T>(Action<T> callback) where T : class
        {
            if (callback == null)
                throw new ArgumentNullException();

            var type = typeof(T);

            List<object> listenersForTheType;
            if (listeners.TryGetValue(type, out listenersForTheType))
            {
                listenersForTheType.Add(callback);
            } else
            {
                listenersForTheType = new List<object>();
                listenersForTheType.Add(callback);
                listeners.Add(type, listenersForTheType);
            }
        }

        public static void RemoveListener<T>(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException();

            var type = typeof(T);

            if (listeners.TryGetValue(type, out var listenersForTheType))
            {
                listenersForTheType.Remove(callback);
            }
        }

        public static void Call<T>(T arg)
        {
            var type = typeof(T);
            if (listeners.TryGetValue(type, out var listenersForTheType))
            {
                foreach(var o in listenersForTheType)
                {
                    (o as Action<T>).Invoke(arg);
                }
            }
        }
    }
}