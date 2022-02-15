using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace HandGestureDetector
{
    [CreateAssetMenu(fileName = "HandFormRecognizedGameEvent", menuName = "HandFormRecognizer/HandFormRecognizeGameEvent")]
    public class HandFormRecognizeGameEvent : GameEvent<HandFormData>
    {
    }
}