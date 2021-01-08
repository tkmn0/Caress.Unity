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

            return result.Value;
        }

        public int EncodeFloat(float[] pcm, byte[] buffer)
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.EncodeFloat(_ptr, pcm, pcm.Length, buffer, buffer.Length, out var result);
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
            NativeMethods.DestroyEncoder(ref data);
            _ptr = data.Ptr;
        }

        public void SetBitrate(int bitrate)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.EncoderSetBitrate(_ptr, bitrate, out var error);
            if (error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(error.Data.StringValue());
            }
        }

        public int GetBitrate()
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.EncoderGetBitrate(_ptr, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Value;
        }

        public void SetComplexity(int complexity)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.EncoderSetComplexity(_ptr, complexity, out var error);
            if (error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(error.Data.StringValue());
            }
        }

        public int GetComplexity()
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.EncoderGetComplexity(_ptr, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Value;
        }

        public void SetSignal(EncoderSignal signal)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.EncoderSetSignal(_ptr, (int) signal, out var error);
            if (error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(error.Data.StringValue());
            }
        }

        public EncoderSignal GetSignal()
        {
            if (_ptr == IntPtr.Zero) return EncoderSignal.None;
            NativeMethods.EncoderGetSignal(_ptr, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return (EncoderSignal) result.Value;
        }

        public void SetInBandFEC(bool enable)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.EncoderSetInBandFEC(_ptr, enable, out var error);
            if (error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(error.Data.StringValue());
            }
        }

        public bool GetInBandFEC()
        {
            if (_ptr == IntPtr.Zero) return false;
            NativeMethods.EncoderGetInBandFEC(_ptr, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Value;
        }

        public void SetPacketLossPercentage(int percentage)
        {
            if (_ptr == IntPtr.Zero) return;
            NativeMethods.EncoderSetPacketLossPercentage(_ptr, percentage, out var error);
            if (error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(error.Data.StringValue());
            }
        }

        public int GetPacketLossPercentage()
        {
            if (_ptr == IntPtr.Zero) return 0;
            NativeMethods.EncoderGetPacketLossPercentage(_ptr, out var result);
            if (result.Error.Code != (byte) ErrorCode.CaressOk)
            {
                throw new Exception(result.Error.Data.StringValue());
            }

            return result.Value;
        }
    }
}