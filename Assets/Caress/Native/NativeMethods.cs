using System;
using System.Runtime.InteropServices;

namespace Caress.Native
{
    internal static class NativeMethods
    {
        private const string DLLName = "libcaress";

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateNoiseReducer(ref NoiseReducerConfig config, out PointerResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateEncoder(ref EncoderConfig config, out PointerResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateDecoder(ref DecoderConfig config, out PointerResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReduceNoise(IntPtr ptr, short[] pcm, int pcmLength, int channel);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReduceNoiseFloat(IntPtr ptr, float[] pcm, int pcmLength, int channel);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMaxAttenuation(IntPtr ptr, double attenuation);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ChangeRnnModel(IntPtr ptr, RnNoiseModel model);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Encode(
            IntPtr ptr,
            short[] pcm,
            int pcmLen,
            byte[] buffer,
            int bufferLen,
            out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncodeFloat(
            IntPtr ptr,
            float[] pcm,
            int pcmLen,
            byte[] buffer,
            int bufferLen,
            out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderSetBitrate(IntPtr ptr, int bitrate, out ApiError error);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderGetBitrate(IntPtr ptr, out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderSetComplexity(IntPtr ptr, int complexity, out ApiError error);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderGetComplexity(IntPtr ptr, out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderSetSignal(IntPtr ptr, int signal, out ApiError error);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderGetSignal(IntPtr ptr, out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderSetInBandFEC(IntPtr ptr, bool enable, out ApiError error);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderGetInBandFEC(IntPtr ptr, out BoolResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderSetPacketLossPercentage(IntPtr ptr, int percentage, out ApiError error);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncoderGetPacketLossPercentage(IntPtr ptr, out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Decode(
            IntPtr ptr,
            bool fec,
            byte[] buffer,
            int bufferLen,
            short[] pcm,
            int pcmLen,
            out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DecodeFloat(
            IntPtr ptr,
            bool fec,
            byte[] buffer,
            int bufferLen,
            float[] pcm,
            int pcmLen,
            out IntResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyNoiseReducer(ref Data data);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyEncoder(ref Data data);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyDecoder(ref Data data);
    }
}