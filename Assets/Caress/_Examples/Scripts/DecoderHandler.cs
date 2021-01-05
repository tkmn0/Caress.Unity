using System;
using UnityEngine;

namespace Caress.Examples
{
    public class DecoderHandler : MonoBehaviour
    {
        public Decoder Decoder { get; private set; }
        public event Action<float[], int> OnDecoded; 
        private const int BufferSize = 1024;
        private float[] _pcmBuffer = new float[BufferSize];

        private void OnEnable()
        {
            Decoder = new Decoder(new DecoderConfig()
            {
                Channels = (ushort) NumChannels.Mono,
                SampleRate = (uint) SampleRate._48000
            });
        }

        private void OnDisable()
        {
            if (Decoder == null) return;
            Decoder.Destroy();
            Decoder = null;
        }

        public void Decode(byte[] buffer, int bufferLength)
        {
            if (Decoder == null) return;
            var length = Decoder.DecodeFloat(buffer, bufferLength, _pcmBuffer);
            OnDecoded?.Invoke(_pcmBuffer, length);
        }
    }
}