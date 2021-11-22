using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HandPoseDetection
{
    public abstract class HandFormDataBuilder : MonoBehaviour
    {
        protected IHandSkeleton handSkeleton;
        protected IList<HandFormData> handForms;
        protected IObservable<object> addNewHandFormCommandObservable;

        private void Start() => Initialize();

        protected virtual void Initialize()
        {
            addNewHandFormCommandObservable.Subscribe(_ => AddNewHandForm())
                .AddTo(this);
        }

        protected virtual void AddNewHandForm()
        {
            var fingerData = new List<Vector3>();
            var fingerBones = handSkeleton.Bones;
            
            foreach (var bone in fingerBones)
            {
                fingerData.Add(handSkeleton.Root.InverseTransformPoint(bone.position));
            }
         
            var newHandPose = new HandFormData
            {
                name = $"HandForm_{handForms.Count}",
                fingerData = fingerData.ToArray()
            };

            handForms.Add(newHandPose);
        }
    }
}