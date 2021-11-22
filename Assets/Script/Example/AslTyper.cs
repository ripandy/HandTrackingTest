using System;
using HandPoseDetection;
using Kadinche.Kassets.EventSystem;
using TMPro;
using UniRx;
using UnityEngine;

namespace Script.Example
{
    public class AslTyper : MonoBehaviour
    {
        [SerializeField] private GameEvent<HandFormData> _handFormRecognizedGameEvent;
        [SerializeField] private string _clearHandFormName;
        [SerializeField] private string _backspaceHandFormName;
        [SerializeField] private string _spaceHandFormName;
        [SerializeField] private string _newlineHandFormName;
        [SerializeField] private TMP_Text typedText;

        private const string AslPrefix = "ASL";

        private void Start()
        {
            (_handFormRecognizedGameEvent as IObservable<HandFormData>)
                .Skip(1)
                .Subscribe(OnHandFormRecognized)
                .AddTo(this);
        }

        private void OnHandFormRecognized(HandFormData handForm)
        {
            var handFormName = handForm.name;
            if (handFormName.Equals(_clearHandFormName))
            {
                ClearText();
            }
            else if (handFormName.Equals(_backspaceHandFormName))
            {
                Backspace();
            }
            else if (handFormName.Equals(_spaceHandFormName))
            {
                Space();
            }
            else if (handFormName.Equals(_newlineHandFormName))
            {
                Newline();
            }
            else
            {
                OnRightHandFormRecognized(handFormName);
            }
        }

        private void OnRightHandFormRecognized(string handFormName)
        {
            var split = handFormName.Split('_');
            if (split.Length == 0 || !split[0].Equals(AslPrefix)) return;

            var letter = split[1];
            
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