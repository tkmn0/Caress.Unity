using System.Globalization;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Caress.Examples
{
    public class NoiseReducerControl : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private Button _playmodeButton = default;
        [SerializeField] private Button _attenuationReduceButton = default;
        [SerializeField] private Button _attenuationAddButton = default;
        [SerializeField] private Button _noiseModelButton = default;
        [SerializeField] private Text _toggleButtonText = default;
        [SerializeField] private Text _attenuationValueText = default;
        [SerializeField] private Text _noiseModelText = default;
        [Header("Value")] [SerializeField] private MixerState _mixerState = MixerState.NoiseReduced;
        [SerializeField] private double _attenuation = 20;
        [SerializeField] private RnNoiseModel _noiseModel = RnNoiseModel.None;
        [Header("Feature")] [SerializeField] private AudioMixer _audioMixer = default;
        [SerializeField] private NoiseReducerHandler _noiseReducerHandler = default;

        private void Start()
        {
            //PlayMode
            _audioMixer.SetFloat(_mixerState.ToString(), 0);
            _toggleButtonText.text = _mixerState.ToString();
            //Attenuation
            _noiseReducerHandler.NoiseReducer?.SetAttenuation(_attenuation);
            _attenuationValueText.text = _attenuation.ToString(CultureInfo.CurrentCulture);
            //NoiseModel
            _noiseReducerHandler.NoiseReducer?.ChangeRnnModel(_noiseModel);
            _noiseModelText.text = _noiseModel.ToString();
        }

        private void OnEnable()
        {
            _playmodeButton.onClick.AddListener(PlayModeButtonClicked);
            _attenuationReduceButton.onClick.AddListener(AttenuationReduceButtonClicked);
            _attenuationAddButton.onClick.AddListener(AttenuationAddButtonClicked);
            _noiseModelButton.onClick.AddListener(NoiseModelButtonClicked);
        }

        private void OnDisable()
        {
            _playmodeButton.onClick.RemoveListener(PlayModeButtonClicked);
            _attenuationReduceButton.onClick.RemoveListener(AttenuationReduceButtonClicked);
            _attenuationAddButton.onClick.RemoveListener(AttenuationAddButtonClicked);
            _noiseModelButton.onClick.AddListener(NoiseModelButtonClicked);
        }

        private void PlayModeButtonClicked()
        {
            var next = _mixerState == MixerState.Original ? MixerState.NoiseReduced : MixerState.Original;
            _audioMixer.SetFloat(_mixerState.ToString(), -80);
            _audioMixer.SetFloat(next.ToString(), 0);
            _mixerState = next;
            _toggleButtonText.text = _mixerState.ToString();
        }

        private void AttenuationReduceButtonClicked()
        {
            _attenuation -= 10;
            _noiseReducerHandler.NoiseReducer?.SetAttenuation(_attenuation);
            _attenuationValueText.text = _attenuation.ToString(CultureInfo.CurrentCulture);
        }

        private void AttenuationAddButtonClicked()
        {
            _attenuation += 10;
            _noiseReducerHandler.NoiseReducer?.SetAttenuation(_attenuation);
            _attenuationValueText.text = _attenuation.ToString(CultureInfo.CurrentCulture);
        }

        private void NoiseModelButtonClicked()
        {
            _noiseModel = _noiseModel == RnNoiseModel.None ? 0 : _noiseModel + 1;
            _noiseReducerHandler.NoiseReducer?.ChangeRnnModel(_noiseModel);
            _noiseModelText.text = _noiseModel.ToString();
        }
    }
}