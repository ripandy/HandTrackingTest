using UnityEngine;

namespace HandGestureRecognizer
{
    public class HandDirectionRecognizer : IRecognizer
    {
        private float Evaluate(HandDirection handDirection, Vector3[] comparison, float sensitivity = 10f)
        {
            if (handDirection.data.Length != comparison.Length)
            {
                Debug.LogWarning($"Hand data {handDirection.name} is not compatible with active hand.");
                return Mathf.Infinity;
            }
            
            var sumAngle = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < comparison.Length)
            {
                var angle = Vector3.Angle(handDirection.data[i], comparison[i]);

                discard = angle > sensitivity;
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

        float IRecognizer.Evaluate(HandData handData, Vector3[] comparison, float sensitivity)
        {
            if (handData is HandDirection handDirection)
                return Evaluate(handDirection, comparison, sensitivity);
            
            Debug.LogWarning($"Hand data {handData.name} is not compatible with [{nameof(HandDirectionRecognizer)}].");
            return Mathf.Infinity;
        }
    }
}
