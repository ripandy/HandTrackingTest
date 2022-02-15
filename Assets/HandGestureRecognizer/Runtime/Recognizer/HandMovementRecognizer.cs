using UnityEngine;

namespace HandGestureRecognizer
{
    public class HandMovementRecognizer : IRecognizer
    {
        private float Evaluate(HandMovement handMovement, Vector3[] comparison, float sensitivity = 0.05f)
        {
            if (handMovement.data.Length != comparison.Length)
            {
                Debug.LogWarning($"Hand data {handMovement.name} is not compatible with active hand.");
                return Mathf.Infinity;
            }
            
            var sumDistance = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < comparison.Length)
            {
                var distance = Vector3.Distance(handMovement.data[i], comparison[i]);

                discard = distance > sensitivity;
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

        float IRecognizer.Evaluate(HandData handData, Vector3[] comparison, float sensitivity)
        {
            if (handData is HandMovement handMovement)
                return Evaluate(handMovement, comparison, sensitivity);
            
            Debug.LogWarning($"Hand data {handData.name} is not compatible with [{nameof(HandMovementRecognizer)}].");
            return Mathf.Infinity;
        }
    }
}
