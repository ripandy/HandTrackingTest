using System;
using Cysharp.Threading.Tasks;
using HandGestureDetector;
using UnityEngine;
using UniRx;

public class KeybladeSpawner : MonoBehaviour
{
    [SerializeField] private HandFormRecognizeGameEvent _handFormRecognizedGameEvent;
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform keybladeBase;
    [SerializeField] private GameObject keybladeKey;
    [SerializeField] private ParticleSystem spawnEffect;

    private const string Paper = "Paper_R";
    private const string Rock = "Rock_R";

    private Pose _poseOnHand;
    private Vector3 _scaleOnHand;
    private const float SpawnDelay = 0.5f;

    private HandFormData _currentHandForm = HandFormData.none;

    private void Awake()
    {
        _poseOnHand = new Pose(keybladeBase.localPosition, keybladeBase.localRotation);
        _scaleOnHand = keybladeBase.localScale;
    }

    private void Start()
    {
        _handFormRecognizedGameEvent
            .Subscribe(OnPoseDetected)
            .AddTo(this);
    }

    private void OnPoseDetected(HandFormData detected)
    {
        if (detected.name == Rock && _currentHandForm.name != Rock)
        {
            SpawnKeyblade(true);
        }
        else if (detected.name == Paper && _currentHandForm.name != Paper)
        {
            SpawnKeyblade(false);
        }
        
        Debug.Log($"[{GetType().Name}] detected hand form: {detected.name}, previous hand form: {_currentHandForm.name}");
        _currentHandForm = detected;
    }

    private async void SpawnKeyblade(bool spawn)
    {
        Debug.Log($"[{GetType().Name}] {(spawn ? "de" : "")}spawning keyblade..");
        spawnEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        spawnEffect.Play(true);
        
        if (spawn)
        {
            keybladeBase.SetPositionAndRotation(_poseOnHand.position, _poseOnHand.rotation);
            keybladeBase.localScale = _scaleOnHand;
        }
        keybladeBase.SetParent(spawn ? handTransform : transform, !spawn);

        await UniTask.Delay(TimeSpan.FromSeconds(SpawnDelay));
        
        Debug.Log($"[{GetType().Name}] delayed spawn?");
        keybladeKey.SetActive(spawn);
    }
}
