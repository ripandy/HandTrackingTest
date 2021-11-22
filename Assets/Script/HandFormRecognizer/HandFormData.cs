using System;
using UnityEngine;

namespace HandPoseDetection
{
    [Serializable]
    public class HandFormData
    {
        public static HandFormData none = new() { name = "None", fingerData = null };
        
        public string name;
        public Vector3[] fingerData;
    }
}