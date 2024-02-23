using System;
using UnityEngine;

namespace Elementary.Scripts.Data.DataModel
{
    public abstract class BaseDataModel : ScriptableObject
    {
        #if UNITY_EDITOR
        [Attributes.ReadOnly]
        #endif
        public string id;

        // protected void OnValidate()
        // {
        //     #if UNITY_EDITOR
        //
        //     if (!string.IsNullOrEmpty(id)) return;
        //     id = Guid.NewGuid().ToString();
        //
        //     #endif
        // }


        [ContextMenu("Set New Id")]
        public void SetNewId()
        {
            id = Guid.NewGuid().ToString();
        }
    }
}