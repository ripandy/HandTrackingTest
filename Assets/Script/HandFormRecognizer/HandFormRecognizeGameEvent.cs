using Kadinche.Kassets.EventSystem;
using UnityEngine;

namespace HandPoseDetection
{
    [CreateAssetMenu(fileName = "HandFormRecognizedGameEvent", menuName = "HandFormRecognizer/HandFormRecognizeGameEvent")]
    public class HandFormRecognizeGameEvent : GameEvent<HandFormData>
    {
    }
}