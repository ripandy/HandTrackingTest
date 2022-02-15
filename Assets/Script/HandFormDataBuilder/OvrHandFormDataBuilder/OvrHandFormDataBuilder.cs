using HandGestureDetector.OvrHandFormRecognizer;
using Kadinche.Kassets;
using UniRx;
using UnityEngine;

namespace HandGestureDetector.OvrHandFormDataBuilder
{
    public class OvrHandFormDataBuilder : HandFormDataBuilder
    {
        [SerializeField] private HandFormCollection _runtimeHandFormCollection;
        [SerializeField] private OvrHandSkeleton _ovrHandSkeleton;
        [SerializeField] private KeyCode keyToBuild = KeyCode.Space;

        private readonly Subject<object> _addNewHandFormInputProvider = new Subject<object>();

        private void Awake()
        {
            handForms = _runtimeHandFormCollection.Value;
            handSkeleton = _ovrHandSkeleton;
            addNewHandFormInputObservable = _addNewHandFormInputProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(keyToBuild))
                .Subscribe(_ => _addNewHandFormInputProvider.OnNext(default))
                .AddTo(this);
        }

        protected override void AddNewHandForm()
        {
            Debug.Log($"Adding new hand form.. {_runtimeHandFormCollection.Count}");
            base.AddNewHandForm();
            
            Debug.Log($"Added new hand form.. {_runtimeHandFormCollection.Count}");
            _runtimeHandFormCollection.SaveToJson();
        }
    }
}