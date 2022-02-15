using System;
using UnityEngine;

namespace HandGestureDetector
{
    [Serializable]
    public class HandDirection
    {
        public static HandDirection none = new HandDirection() { name = "None", forwards = null };
        
        public string name;
        public int fps = 15;
        public Vector3[] forwards;
    }
}