using System;
using UnityEngine;

namespace HandGestureRecognizer
{
    [Serializable]
    public class HandData
    {
        public static HandData none = new HandData() { name = "None", data = null };
        
        public string name;
        public RecognizionType type;
        public Vector3[] data;

        private Vector3[] _lowRate;
        private Vector3[] _highRate;

        public Vector3[] GetData(RecognitionRate rate)
        {
            switch (rate)
            {
                case RecognitionRate.Low:
                    return LowRate;
                case RecognitionRate.High:
                    return HighRate;
                default:
                    return data;
            }
        }

        public Vector3[] LowRate
        {
            get
            {
                if (_lowRate == null)
                {
                    var len = Mathf.FloorToInt(data.Length * 0.5f);
                    _lowRate = new Vector3[len];
                    for (var i = 0; i < data.Length; i++)
                    {
                        if (i % 2 != 0) continue;
                        _lowRate[i] = data[i];
                    }
                }

                return _lowRate;
            }
        }
        
        public Vector3[] HighRate
        {
            get
            {
                if (_highRate == null)
                {
                    _highRate = new Vector3[data.Length * 2];
                    for (var i = 1; i <= data.Length; i++)
                    {
                        var hrIdx = (i - 1) * 2;
                        var prevData = data[i - 1];
                        var midVal = i < data.Length ? prevData + (data[i] - prevData) * 0.5f : prevData;
                        _highRate[hrIdx] = prevData;
                        _highRate[hrIdx + 1] = midVal;
                    }
                }

                return _highRate;
            }
        }
    }

    public enum RecognizionType
    {
        Pose,
        Movement,
        Direction
    }

    public enum RecognitionRate
    {
        Low,
        Normal,
        High
    }
}