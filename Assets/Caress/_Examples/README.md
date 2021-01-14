# Caress.Unity Examples

## 0_MicrophoneLoopBack
This example is just microphone loopback example.

## 1_NoiseReducerExample
This example is for noise reduction.
Run Unity Editor, and control with UI.
Left side is the label and next is value and button.
See NoiseReducerControl.cs for more details.

- PlayMode: toggle original and NoiseReduced. This will switch audio mixer volume, so you can compare original sound to noise reduced sound.  
- Attenuation: set maximum atenuation decibel. if this value is high, more noise reduced.
- Model: toggle RnNoise model. Each model has pros and cons for the real environment.


## 2_EncoderDecoderExample
This example is for audio encoder and decoder.
Run Unity Editor, and control with UI.
Left side is the label and next is value and button.
See EncodeDecodedControl.cs for more details.

- PlayMode: toggle original and Encoded Decoded audio. This will switch audio mixer volume, so you can compare original sound to opus encoded and decoded sound.
- Bitrate: set maximum bitrate to encoder.
- Complexity: set complexity to encoder. if this value is high, the encoded bitrate value will be increased accuracy based on the value you set, but cpu usage will be increased also.
- Signal: set audio signal type.
- InBandFEC: set InBandFEC. This will be useful when you send encoded packet to the others over network.
- PacketLoss%: set packetloss percentage.