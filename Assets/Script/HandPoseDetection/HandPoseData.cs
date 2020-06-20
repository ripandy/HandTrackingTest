using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HandPoseDetection
{
    [CreateAssetMenu(fileName = "New Hand Pose", menuName = "GestureDetection/HandPose", order = 0)]
    public class HandPoseData : ScriptableObject
    {
        public List<Vector3> fingerDatas = new List<Vector3>();
        private readonly ReactiveProperty<HandPoseData> _onRecognized = new ReactiveProperty<HandPoseData>();
        
        public IReadOnlyReactiveProperty<HandPoseData> OnRecognized => _onRecognized;
        public void Recognized() => _onRecognized.SetValueAndForceNotify(this);
    }
}