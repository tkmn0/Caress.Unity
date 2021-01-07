using System;
using UnityEngine;

namespace Caress.Examples
{
    public class MicrophoneRecorder : MonoBehaviour
    {
        public event Action<float[]> OnAudioReady;
        private const int SampleRate = 48000;
        private const int RecordLengthSec = 1;
        private AudioClip _microphoneClip;
        private int _clipHead;
        private readonly float[] _processBuffer = new float[480];
        private readonly float[] _microphoneBuffer = new float[RecordLengthSec * SampleRate];

        void Start()
        {
            _microphoneClip = Microphone.Start(null, true, RecordLengthSec, SampleRate);
        }

        void Update()
        {
            var position = Microphone.GetPosition(null);
            if (position < 0 || _clipHead == position) return;

            _microphoneClip.GetData(_microphoneBuffer, 0);

            int GetDataLength(int bufferLength, int head, int tail) =>
                head < tail ? tail - head : bufferLength - head + tail;

            while (GetDataLength(_microphoneBuffer.Length, _clipHead, position) > _processBuffer.Length)
            {
                var remain = _microphoneBuffer.Length - _clipHead;
                if (remain < _processBuffer.Length)
                {
                    Array.Copy(_microphoneBuffer, _clipHead, _processBuffer, 0, remain);
                    Array.Copy(_microphoneBuffer, 0, _processBuffer, 0, _processBuffer.Length);
                }
                else
                {
                    Array.Copy(_microphoneBuffer, _clipHead, _processBuffer, 0, _processBuffer.Length);
                }

                OnAudioReady?.Invoke(_processBuffer);
                _clipHead += _processBuffer.Length;
                if (_clipHead > _microphoneBuffer.Length)
                {
                    _clipHead -= _microphoneBuffer.Length;
                }
            }
        }
    }
}