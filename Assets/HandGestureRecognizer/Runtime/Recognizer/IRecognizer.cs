using UnityEngine;

namespace HandGestureRecognizer
{
    public interface IRecognizer
    {
        float Evaluate(HandData handData, Vector3[] comparison, float sensitivity = 0.02f);
    }
}