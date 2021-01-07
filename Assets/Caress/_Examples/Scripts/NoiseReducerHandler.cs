using UnityEngine;

namespace Caress.Examples
{
    public class NoiseReducerHandler : MonoBehaviour
    {
        private const SampleRate SampleRate = Caress.SampleRate._48000;
        private const NumChannels NumChannels = Caress.NumChannels.Mono;
        private NoiseReducer _noiseReducer;
        public NoiseReducer NoiseReducer => _noiseReducer;

        private void OnEnable()
        {
            _noiseReducer = new NoiseReducer(new NoiseReducerConfig()
            {
                SampleRate = (int) SampleRate,
                NumChannels = (int) NumChannels,
                Attenuation = 20,
                Model = RnNoiseModel.Voice
            });
        }

        private void OnDisable()
        {
            _noiseReducer.Destroy();
            _noiseReducer = null;
        }

        public void ProcessPcm(float[] pcm)
        {
            _noiseReducer?.ReduceNoiseFloat(pcm, 0);
        }
    }
}