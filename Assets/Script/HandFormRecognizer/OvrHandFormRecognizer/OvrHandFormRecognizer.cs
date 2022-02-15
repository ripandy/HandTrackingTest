using Kadinche.Kassets;
using UnityEngine;

namespace HandGestureDetector.OvrHandFormRecognizer
{
    public class OvrHandFormRecognizer : HandFormRecognizer
    {
        [SerializeField] private OvrHandSkeleton _ovrHandSkeleton;
        [SerializeField] private HandFormCollection _handFormCollection;
        [SerializeField] private HandFormRecognizeGameEvent _detectedHandFormGameEvent;

        private void Awake()
        {
            handSkeleton = _ovrHandSkeleton;
            handForms = _handFormCollection;
            detectedHandFormObserver = _detectedHandFormGameEvent;
        }

        protected override void Initialize()
        {
            _handFormCollection.LoadFromJson();
            base.Initialize();
        }
    }
}