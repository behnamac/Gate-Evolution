using System;
using Elementary.Scripts.Extensions;
using UnityEngine;

namespace Elementary.Scripts.Data.Management
{
    public static class DataManager
    {
        #region SET

        public static void SaveWithJson<T>(T data)
        {
            string jsonStringData = data.ToJsonString();
            PlayerPrefs.SetString(typeof(T).Name, jsonStringData);
        }

        public static void SaveWithJson<T>(string key, T data)
        {
            string jsonStringData = data.ToJsonString();
            PlayerPrefs.SetString(key, jsonStringData);
        }

        public static void Save<T>(string key, T data)
        {
            PlayerPrefs.SetString(key, data.ToString());
        }

        #endregion

        #region GET

        public static T GetWithJson<T>()
        {
            string jsonData = PlayerPrefs.GetString(typeof(T).Name);
            return jsonData.ToJsonObject<T>();
        }

        public static T GetWithJson<T>(string key)
        {
            string jsonData = PlayerPrefs.GetString(key);
            return jsonData.ToJsonObject<T>();
        }

        public static T Get<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key)) return default;

            string data = PlayerPrefs.GetString(key);

            if (typeof(T) == typeof(int))
            {
                return (T)Convert.ChangeType(data, typeof(int));
            }

            if (typeof(T) == typeof(bool))
            {
                return (T)Convert.ChangeType(data, typeof(bool));
            }

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(data, typeof(string));
            }

            if (typeof(T) == typeof(float))
            {
                return (T)Convert.ChangeType(data, typeof(float));
            }

            throw new Exception("Convert Type Exception");
        }

        #endregion
    }
}