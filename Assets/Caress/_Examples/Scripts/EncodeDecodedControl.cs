using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Caress.Examples
{
    public class EncodeDecodedControl : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer = default;
        [SerializeField] private MixerState _mixerState = MixerState.Original;
        [SerializeField] private Button _audioToggleButton = default;
        [SerializeField] private Text _audioPlayModeText = default;
        [SerializeField] private EncoderHandler _encoderHandler = default;

        private void Start()
        {
            _audioMixer.SetFloat(_mixerState.ToString(), 0);
            _audioPlayModeText.text = _mixerState.ToString();
        }

        private void OnEnable()
        {
            _audioToggleButton.onClick.AddListener(AudioToggleButtonClicked);
        }

        private void OnDisable()
        {
            _audioToggleButton.onClick.RemoveListener(AudioToggleButtonClicked);
        }

        private void Update()
        {
            _encoderHandler.Encoder.SetBitrate(3000);
            Debug.Log("bitrate :" + _encoderHandler.Encoder.GetBitrate());
            _encoderHandler.Encoder.SetComplexity(8);
            Debug.Log("complexity: " + _encoderHandler.Encoder.GetComplexity());
            _encoderHandler.Encoder.SetSignal(EncoderSignal.SignalVoice);
            Debug.Log("signal: " + _encoderHandler.Encoder.GetSignal());
            _encoderHandler.Encoder.SetInBandFEC(!_encoderHandler.Encoder.GetInBandFEC());
            Debug.Log("inband fec: " + _encoderHandler.Encoder.GetInBandFEC());
            _encoderHandler.Encoder.SetPacketLossPercentage(30);
            Debug.Log("packet loss %: " + _encoderHandler.Encoder.GetPacketLossPercentage());
        }

        private void AudioToggleButtonClicked()
        {
            var next = _mixerState == MixerState.Original ? MixerState.EncodeDecoded : MixerState.Original;
            _audioMixer.SetFloat(_mixerState.ToString(), -80);
            _audioMixer.SetFloat(next.ToString(), 0);
            _audioPlayModeText.text = next.ToString();
            _mixerState = next;
        }
    }
}