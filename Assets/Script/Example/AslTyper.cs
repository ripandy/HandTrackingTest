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
                .Subscribe(_ =>
                {
                    Debug.Log($"[{GetType().Name}] clear text");
                    ClearText();
                })
                .AddTo(this);
            
            backspaceHandPose.OnRecognized
                .Subscribe(_ =>
                {
                    Debug.Log($"[{GetType().Name}] backspace");
                    Backspace();
                })
                .AddTo(this);

            spaceHandPose.OnRecognized
                .Subscribe(_ =>
                {
                    Debug.Log($"[{GetType().Name}] space");
                    Space();
                })
                .AddTo(this);
            
            newlineHandPose.OnRecognized
                .Subscribe(_ =>
                {
                    Debug.Log($"[{GetType().Name}] newline");
                    Newline();
                })
                .AddTo(this);
            
            // leftHandPoseDetector.DetectedHandPose
            //     .SkipLatestValueOnSubscribe()
            //     .Subscribe(OnLeftHandPoseDetected)
            //     .AddTo(this);
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
            
            t.Remove(t.Length - 1);
            typedText.text = t;
        }

        private void Space()
        {
            typedText.text += " ";
        }

        private void Newline()
        {
            typedText.text += "\n";
        }
        
        // private void OnLeftHandPoseDetected(HandPoseData handPose)
        // {
        //     var detected = handPose.name;
        //     switch (detected)
        //     {
        //         case "Palm_Open_L" :
        //             typedText.text = "";
        //             break;
        //         case "Palm_Closed_L" :
        //             var str = typedText.text.Remove(typedText.text.Length - 1);
        //             typedText.text = str;
        //             break;
        //         case "Rock_L" :
        //             typedText.text += " ";
        //             break;
        //         case "OK_L" :
        //             typedText.text += "\n";
        //             break;
        //     }
        // }
    }
}