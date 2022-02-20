using UnityEngine;

namespace HandGestureRecognizer
{
    public class PoseEvaluator : IEvaluator
    {
        public float Evaluate(Vector3[] data, Vector3[] inputs, float tolerance = 0.02f)
        {
            if (data.Length != inputs.Length)
            {
                Debug.LogWarning("data is not compatible with current input.");
                return Mathf.Infinity;
            }
            
            var sumDistance = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < inputs.Length)
            {
                var distance = Vector3.Distance(data[i], inputs[i]);

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
