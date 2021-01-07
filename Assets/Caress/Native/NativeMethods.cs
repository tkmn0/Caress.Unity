using System;
using System.Runtime.InteropServices;

namespace Caress.Native
{
    internal static class NativeMethods
    {
        private const string DLLName = "caress";

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
            out EncodeDecodeResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void EncodeFloat(
            IntPtr ptr,
            float[] pcm,
            int pcmLen,
            byte[] buffer,
            int bufferLen,
            out EncodeDecodeResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Decode(
            IntPtr ptr,
            bool fec,
            byte[] buffer,
            int bufferLen,
            short[] pcm,
            int pcmLen,
            out EncodeDecodeResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DecodeFloat(
            IntPtr ptr,
            bool fec,
            byte[] buffer,
            int bufferLen,
            float[] pcm,
            int pcmLen,
            out EncodeDecodeResult result);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyNoiseReducer(ref Data data);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyEncoder(ref Data data);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyDecoder(ref Data data);
    }
}