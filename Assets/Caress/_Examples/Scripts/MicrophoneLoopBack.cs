using System;
using UnityEngine;

namespace Caress.Examples
{
    public class MicrophoneLoopBack : MonoBehaviour
    {
        [SerializeField] private MicrophoneRecorder _microphoneRecorder = default;
        [SerializeField] private AudioPlayer _audioPlayer = default;

        private void OnEnable() => _microphoneRecorder.OnAudioReady += OnRecorded;

        private void OnDisable() => _microphoneRecorder.OnAudioReady -= OnRecorded;

        private void OnRecorded(float[] pcm)
        {
            _audioPlayer.ProcessBuffer(pcm, pcm.Length);
        }
    }
}
