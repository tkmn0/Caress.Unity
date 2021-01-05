using UnityEngine;

namespace Caress.Examples
{
    [RequireComponent(typeof(LineRenderer), typeof(AudioSource))]
    public class AudioVisualizer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private AudioSource _source = default;
        private float[] _data = new float[1024];

        private void OnEnable()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _source = GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            _lineRenderer = null;
            _source = null;
        }

        public void Update()
        {
            if (_lineRenderer == null) return;
            if (_source.clip == null) return;
            _source.GetSpectrumData(_data, 0, FFTWindow.BlackmanHarris);
            _lineRenderer.positionCount = _data.Length;
            
            var positions = new Vector3[_data.Length];
            const float xStretch = 8.0f;
            var yOffset = transform.position.y;
            for (var i = 0; i < _data.Length; i++)
            {
                positions[i] = new Vector3(
                    xStretch * (2.0f * i / (_lineRenderer.positionCount - 1.0f) - 1.0f),
                    _data[i] * 500.0f + yOffset,
                    0);
            }
            _lineRenderer.SetPositions(positions);
        }
    }
}
