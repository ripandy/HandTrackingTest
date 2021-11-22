using System.Linq;
using UnityEngine;

namespace HandPoseDetection.OvrHandFormRecognizer
{
    [RequireComponent(typeof(OVRSkeleton))]
    public class OvrHandSkeleton : MonoBehaviour, IHandSkeleton
    {
        private OVRSkeleton _ovrSkeleton;
        private Transform _root;
        private Transform[] _boneTransforms;

        private void Awake()
        {
            TryGetComponent(out _ovrSkeleton);
            _root = _ovrSkeleton.transform;
            _boneTransforms = _ovrSkeleton.Bones.Select(bone => bone.Transform).ToArray();
        }

        public Transform Root => _root;
        public Transform[] Bones => _boneTransforms;
    }
}