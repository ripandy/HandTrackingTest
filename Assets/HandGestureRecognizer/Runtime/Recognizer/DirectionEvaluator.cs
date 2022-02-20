using UnityEngine;

namespace HandGestureRecognizer
{
    public class DirectionE00000000000000001valuator : IEvaluator
    {
       private float Evaluate(HandDirection handDirection, Vector3[] inputs, float tolerance = 10f)
        {
            if (handDirection.data.Length != inputs.Length)
            {
                Debug.LogWarning($"Hand data {handDirection.name} is not compatible with active hand.");
                return Mathf.Infinity;
            }
            
            var sumAngle = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < inputs.Length)
            {
                var angle = Vector3.Angle(handDirection.data[i], inputs[i]);

                discard = angle > tolerance;
                if (discard)
                {
                    sumAngle = Mathf.Infinity;
                }
                else
                {
                    i++;
                    sumAngle += angle;
                }
            }

            return sumAngle;
        }

        float IEvaluator.Evaluate(HandData handData, Vector3[] inputs, float tolerance)
        {
            if (handData is HandDirection handDirection)
                return Evaluate(handDirection, inputs, tolerance);
            
            Debug.LogWarning($"Hand data {handData.name} is not compatible with [{nameof(DirectionEvaluator)}].");
            return Mathf.Infinity;
        }
    }
}
