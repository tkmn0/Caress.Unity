namespace Caress
{
    public enum Application : int
    {
        Voip = 2048,
        Audio = 2049,
        RestrictedLowDelay = 2051
    }

    public enum SampleRate : int
    {
        _8000 = 8000,
        _12000 = 12000,
        _16000 = 16000,
        _24000 = 24000,
        _48000 = 48000
    }

    public enum RnNoiseModel : byte
    {
        General = 0,
        GeneralRecording = 1,
        Voice = 2,
        VoiceRecording = 3,
        Speech = 4,
        SpeechRecording = 5,
        None = 6,
    }

    public enum NumChannels : int
    {
        Mono = 1,
        Stereo = 2
    }

    public enum EncoderSignal : int
    {
        SignalAuto = -1000,
        SignalVoice = 3001,
        SignalMusic = 3002,
        None = 0,
    }

    public struct NoiseReducerConfig
    {
        public int NumChannels;
        public int SampleRate;
        public double Attenuation;
        public RnNoiseModel Model;
    }

    public struct EncoderConfig
    {
        public uint SampleRate;
        public ushort Channels;
        public Application Application;
    }

    public struct DecoderConfig
    {
        public uint SampleRate;
        public ushort Channels;
    }
}