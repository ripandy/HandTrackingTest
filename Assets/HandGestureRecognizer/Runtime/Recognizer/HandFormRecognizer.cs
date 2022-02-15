using UnityEngine;

namespace HandGestureRecognizer
{
    public class HandFormRecognizer : IRecognizer
    {
        private float Evaluate(HandForm handForm, Vector3[] comparison, float sensitivity = 0.02f)
        {
            if (handForm.data.Length != comparison.Length)
            {
                Debug.LogWarning($"Hand data {handForm.name} is not compatible with active hand.");
                return Mathf.Infinity;
            }
            
            var sumDistance = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < comparison.Length)
            {
                var distance = Vector3.Distance(handForm.data[i], comparison[i]);

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
            if (handData is HandForm handForm)
                return Evaluate(handForm, comparison, sensitivity);
            
            Debug.LogWarning($"Hand data {handData.name} is not compatible with {nameof(HandFormRecognizer)}.");
            return Mathf.Infinity;
        }
    }
}
