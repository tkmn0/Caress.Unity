using System;
using Caress.Native;

namespace Caress
{
    public class NoiseReducer
    {
        private IntPtr _ptr;

        public NoiseReducer(NoiseReducerConfig config)
        {
            NativeMethods.CreateNoiseReducer(ref config, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            _ptr = result.Ptr;
        }

        ~NoiseReducer()
        {
            Destroy();
        }

        public void ReduceNoise(short[] pcm, int channel)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.ReduceNoise(_ptr, pcm, pcm.Length, channel);
        }

        public void ReduceNoiseFloat(float[] pcm, int channel)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.ReduceNoiseFloat(_ptr, pcm, pcm.Length, channel);
        }

        public void SetAttenuation(double value)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.SetMaxAttenuation(_ptr, value);
        }

        public void ChangeRnnModel(RnNoiseModel model)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.ChangeRnnModel(_ptr, model);
        }

        public void Destroy()
        {
            if (_ptr == IntPtr.Zero) return;
            var data = new Data()
            {
                Ptr = _ptr
            };
            NativeMethods.DestroyNoiseReducer(ref data);
            _ptr = data.Ptr;
        }
    }
}