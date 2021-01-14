using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Caress.Examples
{
    public class BenchControl : MonoBehaviour
    {
        private class BitRateHandler
        {
            private const int BitSize = 8;
            private readonly int _bytesSize;
            private int _bufferSize;
            private int _lastBufferSize;

            public BitRateHandler(int byteSize)
            {
                _bytesSize = byteSize;
            }
            
            public void AddBufferLength(int length)
            {
                _bufferSize += length;
            }

            public int GetBufferSize()
            {
                var length = _bufferSize * BitSize * _bytesSize;
                _bufferSize = 0;
                _lastBufferSize = length;
                return length;
            }

            public int GetLastBufferSize()
            {
                return _lastBufferSize;
            }
        }

        private enum Unit 
        {
            bps =  1,
            Kbps = 1000,
            Mbps = 1000 * 1000
        }
        
        [SerializeField] private MicrophoneRecorder _microphoneRecorder;
        [SerializeField] private EncoderHandler _encoderHandler;
        [SerializeField] private DecoderHandler _decoderHandler;
        [SerializeField] private Text _rawBitRateText;
        [SerializeField] private Text _encodedBitRateText;
        [SerializeField] private Text _decodedBitRateText;
        [SerializeField] private Button _unitButton;
        [SerializeField] private Unit _unit = Unit.bps;
        private readonly BitRateHandler _rawBitRateHandler = new BitRateHandler(sizeof(float));
        private readonly BitRateHandler _encodedBitRateHandler = new BitRateHandler(sizeof(byte));
        private readonly BitRateHandler _decodedBitRateHandler = new BitRateHandler(sizeof(float));

        private void OnEnable()
        {
            _microphoneRecorder.OnAudioReady += OnRecorded;
            _encoderHandler.OnEncoded += OnEncoded;
            _decoderHandler.OnDecoded += OnDecoded;
            _unitButton.onClick.AddListener(UpdateUnit);
        }

        private void OnDisable()
        {
            _microphoneRecorder.OnAudioReady -= OnRecorded;
            _encoderHandler.OnEncoded -= OnEncoded;
            _decoderHandler.OnDecoded -= OnDecoded;            
            _unitButton.onClick.AddListener(UpdateUnit);
        }

        private void Start()
        {
            StartCoroutine(UpdateUI());
        }

        private IEnumerator UpdateUI()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1.0f);
                var rawBitRate = _rawBitRateHandler.GetBufferSize() / (float)_unit;
                var encodedBitRate = _encodedBitRateHandler.GetBufferSize() / (float)_unit;
                var decodedBitrate = _decodedBitRateHandler.GetBufferSize() / (float)_unit;
                _rawBitRateText.text = rawBitRate.ToString("f2") + _unit;
                _encodedBitRateText.text = encodedBitRate.ToString("f2") + _unit;
                _decodedBitRateText.text = decodedBitrate.ToString("f2") + _unit;
            }
        }

        private void UpdateUnit()
        {
            _unit = _unit == Unit.bps ? Unit.Kbps : _unit == Unit.Kbps ? Unit.Mbps : Unit.bps;
            var rawBitRate = _rawBitRateHandler.GetLastBufferSize() / (float)_unit;
            var encodedBitRate = _encodedBitRateHandler.GetLastBufferSize() / (float)_unit;
            var decodedBitrate = _decodedBitRateHandler.GetLastBufferSize() / (float)_unit;
            _rawBitRateText.text = rawBitRate.ToString("f2") + _unit;
            _encodedBitRateText.text = encodedBitRate.ToString("f2") + _unit;
            _decodedBitRateText.text = decodedBitrate.ToString("f2") + _unit;
        }

        private void OnRecorded(float[] pcm)
        {
            _rawBitRateHandler.AddBufferLength(pcm.Length);
        }

        private void OnEncoded(byte[] buffer, int length)
        {
            _encodedBitRateHandler.AddBufferLength(length);
        }

        private void OnDecoded(float[] pcm, int length)
        {
            _decodedBitRateHandler.AddBufferLength(length);
        }
    }
}