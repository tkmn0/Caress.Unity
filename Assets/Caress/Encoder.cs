using System;
using Caress.Native;

namespace Caress
{
    public class Encoder
    {
        private IntPtr _ptr;

        public Encoder(EncoderConfig config)
        {
            NativeMethods.CreateEncoder(ref config, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            _ptr = result.Ptr;
        }

        ~Encoder()
        {
            Destroy();
        }

        public int Encode(short[] pcm, byte[] buffer)
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.Encode(_ptr, pcm, pcm.Length, buffer, buffer.Length, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Length;
        }

        public int EncodeFloat(float[] pcm, byte[] buffer)
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.EncodeFloat(_ptr, pcm, pcm.Length, buffer, buffer.Length, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Length;
        }

        public void Destroy()
        {
            if (_ptr == IntPtr.Zero) return;
            var data = new Data()
            {
                Ptr = _ptr
            };
            NativeMethods.DestroyEncoder(ref data);
            _ptr = data.Ptr;
        }
    }
}