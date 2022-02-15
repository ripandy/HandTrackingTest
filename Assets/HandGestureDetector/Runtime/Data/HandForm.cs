using System;
using UnityEngine;

namespace HandGestureDetector
{
    [Serializable]
    public class HandForm
    {
        public static HandForm none = new HandForm() { name = "None", fingerBones = null };
        
        public string name;
        public Vector3[] fingerBones;
    }
}