using System;
using UnityEngine;

namespace Caress.Examples
{
    public class EncoderHandler : MonoBehaviour
    {
        public event Action<byte[], int> OnEncoded;
        public Encoder Encoder { get; private set; }
        private const int BufferSize = 1024;
        private readonly byte[] _buffer = new byte[BufferSize];

        private void OnEnable()
        {
            Encoder = new Encoder(new EncoderConfig()
            {
                Application = Application.Voip,
                Channels = (ushort) NumChannels.Mono,
                SampleRate = (uint) SampleRate._48000
            });
        }

        private void OnDisable()
        {
            if (Encoder == null) return;
            Encoder.Destroy();
            Encoder = null;
        }

        public void Encode(float[] pcm)
        {
            if (Encoder == null) return;
            var length = Encoder.EncodeFloat(pcm, _buffer);
            OnEncoded?.Invoke(_buffer, length);
        }
    }
}