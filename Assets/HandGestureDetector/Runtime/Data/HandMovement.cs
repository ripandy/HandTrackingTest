using System;
using Kadinche.Kassets.Variable;
using UnityEngine;

namespace HandGestureDetector
{
    [Serializable]
    public class HandMovement
    {
        public static HandMovement none = new HandMovement() { name = "None", points = null };
        
        public string name;
        public int fps = 15;
        public Vector3[] points;
    }
}