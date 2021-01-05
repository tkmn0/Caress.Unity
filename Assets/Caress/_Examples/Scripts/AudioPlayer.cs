using System;
using UnityEngine;

namespace Caress.Examples
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private const NumChannels Channels = NumChannels.Mono;
        private const SampleRate SampleRate = Caress.SampleRate._48000;
        private const int AudioClipLength = 1024 * 6;
        private AudioSource _source;
        private int _clipHead;
        private float[] _audioClipData;

        private void OnEnable()
        {
            _source = GetComponent<AudioSource>();
            _source.clip = AudioClip.Create("buffer", AudioClipLength, (int) Channels, (int) SampleRate, false);
            _source.loop = true;
        }

        private void OnDisable()
        {
            _source.Stop();
            _source.clip = null;
        }

        public void ProcessBuffer(float[] pcm, int pcmLength)
        {
            if (_audioClipData == null || _audioClipData.Length != pcmLength)
            {
                _audioClipData = new float[pcmLength];
            }
            Array.Copy(pcm, _audioClipData, pcmLength);
            _source.clip.SetData(_audioClipData, _clipHead);
            _clipHead += pcmLength;
            if (!_source.isPlaying && _clipHead > AudioClipLength / 2)
            {
                _source.Play();
            }

            _clipHead %= AudioClipLength;
        }
    }
}
