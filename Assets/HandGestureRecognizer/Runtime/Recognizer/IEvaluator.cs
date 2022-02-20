using UnityEngine;

namespace HandGestureRecognizer
{
    public interface IEvaluator
    {
        float Evaluate(Vector3[] data, Vector3[] inputs, float tolerance = 0.02f);
    }
}