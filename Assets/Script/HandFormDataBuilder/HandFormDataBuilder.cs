using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HandGestureDetector
{
    public abstract class HandFormDataBuilder : MonoBehaviour
    {
        protected IHandSkeleton handSkeleton;
        protected IList<HandFormData> handForms;
        protected IObservable<object> addNewHandFormInputObservable;

        private void Start() => Initialize();

        protected virtual void Initialize()
        {
            addNewHandFormInputObservable
                .Subscribe(_ => AddNewHandForm())
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
                fingerBones = fingerData.ToArray()
            };

            handForms.Add(newHandPose);
        }
    }
}