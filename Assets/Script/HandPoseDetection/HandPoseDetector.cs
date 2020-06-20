using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HandPoseDetection
{
    public class HandPoseDetector : MonoBehaviour
    {
        [SerializeField] private OVRSkeleton fingerSkeleton;
        [Range(0.001f, 0.3f)] [SerializeField] private float sensitivity = 0.02f;
        [SerializeField] private List<HandPoseData> handPoses;
        
        private readonly ReactiveProperty<HandPoseData> _detectedHandPose = new ReactiveProperty<HandPoseData>();
        public IReadOnlyReactiveProperty<HandPoseData> DetectedHandPose => _detectedHandPose;

        private void Start()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    var detectedPose = DetectHandPose();
                    if (detectedPose != null)
                        _detectedHandPose.Value = detectedPose;
                })
                .AddTo(this);

            _detectedHandPose
                .Where(handPose => handPose != null)
                .Subscribe(handPose => handPose.Recognized())
                .AddTo(this);
            
            _detectedHandPose
                .Where(handPose => handPose != null)
                .Subscribe(handPose => Debug.Log($"[{GetType().Name}] detected hand pose: {handPose.name}"))
                .AddTo(this);
        }

        private HandPoseData DetectHandPose()
        {
            var idx = -1;
            var currentMin = Mathf.Infinity;

            for (var i = 0; i < handPoses.Count; i++)
            {
                var distance = CalculateFingerDistance(fingerSkeleton, handPoses[i]);

                if (distance < currentMin)
                {
                    currentMin = distance;
                    idx = i;
                }
            }

            if (idx >= 0)
                return handPoses[idx];
            return null;
        }

        private float CalculateFingerDistance(OVRSkeleton skeleton, HandPoseData handPose)
        {
            var sumDistance = 0f;
            var discard = false;
            var i = 0;
            var fingerBones = skeleton.Bones;

            while (!discard && i < fingerBones.Count)
            {
                var currentData =
                    fingerSkeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                var distance = Vector3.Distance(currentData, handPose.fingerDatas[i]);

                discard = (distance > sensitivity);
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
