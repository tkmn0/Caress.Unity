using System;
using Caress.Native;

namespace Caress
{
    public class Decoder
    {
        private IntPtr _ptr;

        public Decoder(DecoderConfig config)
        {
            NativeMethods.CreateDecoder(ref config, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            _ptr = result.Ptr;
        }

        ~Decoder()
        {
            Destroy();
        }

        public int Decode(byte[] buffer, int bufferLength, short[] pcm, bool fec = false)
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.Decode(_ptr, fec, buffer, bufferLength, pcm, pcm.Length, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Value;
        }

        public int DecodeFloat(byte[] buffer, int bufferLength, float[] pcm, bool fec = false)
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.DecodeFloat(_ptr, fec, buffer, bufferLength, pcm, pcm.Length, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Value;
        }

        public void Destroy()
        {
            if (_ptr == IntPtr.Zero) return;
            var data = new Data()
            {
                Ptr = _ptr
            };
            NativeMethods.DestroyDecoder(ref data);
            _ptr = data.Ptr;
        }
    }
}