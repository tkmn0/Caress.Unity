using System;
using UnityEngine;

namespace Caress.Examples
{
    public class EncodeDecodePlayback : MonoBehaviour
    {
        [SerializeField] private MicrophoneRecorder _microphoneRecorder = default;
        [SerializeField] private AudioPlayer _originalAudioPlayer = default;
        [SerializeField] private AudioPlayer _decodedAudioPlayer = default;
        [SerializeField] private EncoderHandler _encoderHandler = default;
        [SerializeField] private DecoderHandler _decoderHandler = default;

        private void OnEnable()
        {
            _microphoneRecorder.OnAudioReady += OnRecorded;
            _encoderHandler.OnEncoded += OnEncoded;
            _decoderHandler.OnDecoded += OnDecoded;
        }

        private void OnDisable()
        {
            _microphoneRecorder.OnAudioReady -= OnRecorded;
            _encoderHandler.OnEncoded -= OnEncoded;
            _decoderHandler.OnDecoded -= OnDecoded;
        }


        private void OnRecorded(float[] pcm)
        {
            var original = new float[pcm.Length];
            Array.Copy(pcm, original, pcm.Length);
            _encoderHandler.Encode(pcm);
            _originalAudioPlayer.ProcessBuffer(original, original.Length);
        }

        private void OnEncoded(byte[] buffer, int length)
        {
            _decoderHandler.Decode(buffer, length);
        }

        private void OnDecoded(float[] pcm, int length)
        {
            _decodedAudioPlayer.ProcessBuffer(pcm, length);
        }
    }
}