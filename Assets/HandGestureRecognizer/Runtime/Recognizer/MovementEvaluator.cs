using UnityEngine;

namespace HandGestureRecognizer
{
    public class MovementEvaluator : IEvaluator
    {
        public float Evaluate(HandData handData, Vector3[] inputs, float tolerance = 0.05f)
        {
            if (handData.data.Length != inputs.Length)
            {
                Debug.LogWarning($"Hand data {handData.name} is not compatible with current inputs.");
                return Mathf.Infinity;
            }
            
            var sumDistance = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < inputs.Length)
            {
                var distance = Vector3.Distance(handData.data[i], inputs[i]);

                discard = distance > tolerance;
                if (discard)
                {
                    sumDistance = Mathf.Infinity;
                }
                else
                {
                    i++;
                    sumDistance += distance;
                }
            }

            return sumDistance;
        }
    }
}
