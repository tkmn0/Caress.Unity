using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Caress.Examples
{
    public class EncodeDecodedControl : MonoBehaviour
    {
        [SerializeField] private Toggle _showUIToggle = default;
        [SerializeField] private GameObject _container = default;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private MixerState _mixerState = MixerState.Original;
        [SerializeField] private Button _audioToggleButton;
        [SerializeField] private Text _audioPlayModeText;
        [SerializeField] private EncoderHandler _encoderHandler;
        [SerializeField] private Text _bitrateText;
        [SerializeField] private Button _bitrateAddButton;
        [SerializeField] private Button _bitrateReduceButton;
        [SerializeField] private Text _complexityText;
        [SerializeField] private Button _complexityAddButton;
        [SerializeField] private Button _complexityReduceButton;
        [SerializeField] private Text _signalText;
        [SerializeField] private Button _signalUpdateButton;
        [SerializeField] private Text _inbandFecEnableText;
        [SerializeField] private Button _inbandFecUpdateButton;
        [SerializeField] private Text _packetLossPercentageText;
        [SerializeField] private Button _packetLossPercentageAddButton;
        [SerializeField] private Button _packetLossPercentageReduceButton;

        private void Start()
        {
            _audioMixer.SetFloat(_mixerState.ToString(), 0);
            _audioPlayModeText.text = _mixerState.ToString();

            var enc = _encoderHandler.Encoder;
            enc.SetBitrate(enc.GetBitrate());
            _bitrateText.text = enc.GetBitrate().ToString();
            _complexityText.text = enc.GetComplexity().ToString();
            _signalText.text = enc.GetSignal().ToString();
            _inbandFecEnableText.text = enc.GetInBandFEC().ToString();
            _packetLossPercentageText.text = enc.GetPacketLossPercentage().ToString();
        }

        private void OnEnable()
        {
            _audioToggleButton.onClick.AddListener(AudioToggleButtonClicked);
            _showUIToggle.onValueChanged.AddListener(ToggleControlVisibility);
            _bitrateAddButton.onClick.AddListener(AddBitrate);
            _bitrateReduceButton.onClick.AddListener(ReduceBitrate);
            _complexityAddButton.onClick.AddListener(AddComplexity);
            _complexityReduceButton.onClick.AddListener(ReduceComplexity);
            _signalUpdateButton.onClick.AddListener(UpdateSignal);
            _inbandFecUpdateButton.onClick.AddListener(UpdateInBandFec);
            _packetLossPercentageAddButton.onClick.AddListener(AddPacketLossPercentage);
            _packetLossPercentageReduceButton.onClick.AddListener(ReducePacketLossPercentage);
        }

        private void OnDisable()
        {
            _audioToggleButton.onClick.RemoveListener(AudioToggleButtonClicked);
            _showUIToggle.onValueChanged.RemoveListener(ToggleControlVisibility);
            _bitrateAddButton.onClick.RemoveListener(AddBitrate);
            _bitrateReduceButton.onClick.RemoveListener(ReduceBitrate);
            _complexityAddButton.onClick.RemoveListener(AddComplexity);
            _complexityReduceButton.onClick.RemoveListener(ReduceComplexity);
            _signalUpdateButton.onClick.RemoveListener(UpdateSignal);
            _inbandFecUpdateButton.onClick.RemoveListener(UpdateInBandFec);
            _packetLossPercentageAddButton.onClick.RemoveListener(AddPacketLossPercentage);
            _packetLossPercentageReduceButton.onClick.RemoveListener(ReducePacketLossPercentage);
        }
        
        private void ToggleControlVisibility(bool show) => _container.SetActive(show);

        private void AudioToggleButtonClicked()
        {
            var next = _mixerState == MixerState.Original ? MixerState.EncodeDecoded : MixerState.Original;
            _audioMixer.SetFloat(_mixerState.ToString(), -80);
            _audioMixer.SetFloat(next.ToString(), 0);
            _audioPlayModeText.text = next.ToString();
            _mixerState = next;
        }

        #region BitrateControl
        private void AddBitrate() => UpdateBitrate(500);
        private void ReduceBitrate() => UpdateBitrate(-500);
        private void UpdateBitrate(int value)
        {
            var enc = _encoderHandler.Encoder;
            enc.SetBitrate(enc.GetBitrate() + value);
            _bitrateText.text = enc.GetBitrate().ToString();
        }
        #endregion
        #region ComplexityControl
        private void AddComplexity() => UpdateComplexity(1);
        private void ReduceComplexity() => UpdateComplexity(-1);
        private void UpdateComplexity(int value)
        {
            var enc = _encoderHandler.Encoder;
            enc.SetComplexity(enc.GetComplexity() + value);
            _complexityText.text = enc.GetComplexity().ToString();
        }
        #endregion
        #region SignalControl
        private void UpdateSignal()
        {
            var enc = _encoderHandler.Encoder;
            var next = enc.GetSignal() == EncoderSignal.SignalAuto ? EncoderSignal.SignalMusic : enc.GetSignal() == EncoderSignal.SignalMusic ? EncoderSignal.SignalVoice : EncoderSignal.SignalAuto;
            Debug.Log("next: " + enc.GetSignal());
            enc.SetSignal(next);
            _signalText.text = next.ToString();
        }
        #endregion
        #region InBandFECControl
        private void UpdateInBandFec()
        {
            var enc = _encoderHandler.Encoder;
            enc.SetInBandFEC(!enc.GetInBandFEC());
            _inbandFecEnableText.text = enc.GetInBandFEC() ? "enabled" : "disabled";
        }
        #endregion
        #region PacketLossPercentageControl
        private void AddPacketLossPercentage() => UpdatePacketLossPercentage(10);
        private void ReducePacketLossPercentage() => UpdatePacketLossPercentage(-10);
        private void UpdatePacketLossPercentage(int value)
        {
            var enc = _encoderHandler.Encoder;
            enc.SetPacketLossPercentage(enc.GetPacketLossPercentage() + value);
            _packetLossPercentageText.text = enc.GetPacketLossPercentage().ToString();
        }
        #endregion
    }
}