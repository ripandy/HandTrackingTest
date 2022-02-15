using System;
using UnityEngine;

namespace HandGestureRecognizer
{
    [Serializable]
    public class HandData
    {
        public static HandData none = new HandData() { name = "None", data = null };
        
        public string name;
        public Vector3[] data;
    }

    [Serializable]
    public class HandForm : HandData
    {
    }
    
    [Serializable]
    public class HandMovement : HandData
    {
        public int fps = 15;
    }
    
    [Serializable]
    public class HandDirection : HandMovement
    {
    }
}