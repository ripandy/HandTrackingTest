using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace HandPoseDetection
{
    public class HandPoseBuilder : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private OVRSkeleton fingerSkeleton;
        [SerializeField] private KeyCode keyToBuild = KeyCode.Space;
        [SerializeField] private string savePath = "Assets/Media/ScriptableObject/HandPose/";
        private static int assetCount;

        private void Awake()
        {
            assetCount = AssetDatabase.FindAssets("t:"+ nameof(HandPoseData)).Length;  //FindAssets uses tags check documentation for more info
        }

        private void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(keyToBuild))
                .Subscribe(_ => AddNewPose())
                .AddTo(this);
        }

        private void AddNewPose()
        {
            var newName = $"New Hand Pose {++assetCount}";
            var newHandPose = ScriptableObject.CreateInstance<HandPoseData>();
            
            AssetDatabase.CreateAsset(newHandPose, $"{savePath}{newName}.asset");
            
            var data = new List<Vector3>();
            var fingerBones = fingerSkeleton.Bones;
            
            foreach (var bone in fingerBones)
            {
                data.Add(fingerSkeleton.transform.InverseTransformPoint(bone.Transform.position));
            }
            
            newHandPose.name = newName;
            newHandPose.fingerDatas = data;

            EditorUtility.SetDirty(newHandPose);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}