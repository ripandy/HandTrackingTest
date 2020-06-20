using HandPoseDetection;
using TMPro;
using UniRx;
using UnityEngine;

namespace Script.Example
{
    public class AslTyper : MonoBehaviour
    {
        [SerializeField] private HandPoseDetector rightHandPoseDetector;
        [SerializeField] private HandPoseData clearHandPose;
        [SerializeField] private HandPoseData backspaceHandPose;
        [SerializeField] private HandPoseData spaceHandPose;
        [SerializeField] private HandPoseData newlineHandPose;
        [SerializeField] private TMP_Text typedText;

        private const string AslPrefix = "ASL";

        private void Start()
        {
            rightHandPoseDetector.DetectedHandPose
                .SkipLatestValueOnSubscribe()
                .Subscribe(OnRightHandPoseDetected)
                .AddTo(this);

            clearHandPose.OnRecognized
                .Subscribe(_ =>ClearText())
                .AddTo(this);
            
            backspaceHandPose.OnRecognized
                .Subscribe(_ => Backspace())
                .AddTo(this);

            spaceHandPose.OnRecognized
                .Subscribe(_ => Space())
                .AddTo(this);
            
            newlineHandPose.OnRecognized
                .Subscribe(_ => Newline())
                .AddTo(this);
        }

        private void OnRightHandPoseDetected(HandPoseData handPose)
        {
            var splitted = handPose.name.Split('_');
            if (splitted.Length == 0 || !splitted[0].Equals(AslPrefix)) return;

            var letter = splitted[1];
            
            Debug.Log($"[{GetType().Name}] letter: {letter}");
            typedText.text += letter;
        }

        private void ClearText()
        {
            typedText.text = "";
        }

        private void Backspace()
        {
            var t = typedText.text;
            if (t.Length == 0) return;
            
            typedText.text = t.Remove(t.Length - 1, 1);;
        }

        private void Space()
        {
            typedText.text += " ";
        }

        private void Newline()
        {
            typedText.text += "\n";
        }
    }
}