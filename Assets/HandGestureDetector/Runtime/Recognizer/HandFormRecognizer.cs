using System;
using System.Collections.Generic;
using HandPoseDetection;
using UniRx;
using UnityEngine;

namespace HandGestureDetector
{
    public abstract class HandFormRecognizer : MonoBehaviour
    {
        protected IHandSkeleton handSkeleton;
        protected IList<HandForm> handForms;
        protected IObserver<HandForm> detectedHandFormObserver;
        
        [Range(0.001f, 0.1f)]
        [SerializeField] private float _sensitivity = 0.02f;

        private void Start() => Initialize();

        protected virtual void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => DetectHandPose())
                .AddTo(this);
        }

        private void DetectHandPose()
        {
            var idx = -1;
            var currentMin = Mathf.Infinity;

            for (var i = 0; i < handForms.Count; i++)
            {
                var distance = CalculateFingerDistance(handForms[i]);

                if (distance < currentMin)
                {
                    currentMin = distance;
                    idx = i;
                }
            }

            if (idx >= 0)
            {
                detectedHandFormObserver.OnNext(handForms[idx]);
            }
        }

        private float CalculateFingerDistance(HandFormData handForm)
        {
            if (handSkeleton.Bones.Length != handForm.fingerBones.Length)
            {
                Debug.LogError($"Hand Form {handForm.name} is not compatible with active hand skeleton.");
                return Mathf.Infinity;
            }
            
            var sumDistance = 0f;
            var discard = false;
            var i = 0;

            while (!discard && i < handSkeleton.Bones.Length)
            {
                var currentData =
                    handSkeleton.Root.InverseTransformPoint(handSkeleton.Bones[i].position);
                var distance = Vector3.Distance(currentData, handForm.fingerBones[i]);

                discard = distance > _sensitivity;
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
