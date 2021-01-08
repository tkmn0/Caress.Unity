using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Caress.Native
{
    public struct Data
    {
        public IntPtr Ptr;
        public uint Length;

        public string StringValue()
        {
            if (Ptr == IntPtr.Zero) return null;
            var buff = ByteValue();
            return Encoding.UTF8.GetString(buff);
        }

        public byte[] ByteValue()
        {
            if (Ptr == IntPtr.Zero) return null;
            var buff = new byte[Length];
            Marshal.Copy(Ptr, buff, 0, buff.Length);
            return buff;
        }

        public Data(string value)
        {
            var buff = Encoding.UTF8.GetBytes(value);
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(buff.Length);
            Marshal.Copy(buff, 0, unmanagedPointer, buff.Length);
            this.Ptr = unmanagedPointer;
            this.Length = (uint) buff.Length;
        }

        public Data(byte[] value)
        {
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(value.Length);
            Marshal.Copy(value, 0, unmanagedPointer, value.Length);
            this.Ptr = unmanagedPointer;
            this.Length = (uint) value.Length;
        }

        public void Free()
        {
            if (this.Ptr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.Ptr);
            }
        }
    }

    public struct ApiError
    {
        public byte Code;
        public Data Data;
    }

    public struct PointerResult
    {
        public IntPtr Ptr;
        public ApiError Error;
    }

    public struct DataResult
    {
        public Data Data;
        public ApiError Error;
    }

    public struct IntResult
    {
        public int Value;
        public ApiError Error;
    }

    public struct BoolResult
    {
        public bool Value;
        public ApiError Error;
    }

    public enum ErrorCode : byte
    {
        CaressOk = 1,
        ErrorInitialize,
        ErrorUnInitialized,
        ErrorNoDataSupplied,
        ErrorNoTargetBuffer,
        ErrorSuppliedDataSize,
        ErrorEncode,
        ErrorDecode,
        ErrorSetBitrate,
        ErrorGetBitrate,
        ErrorSetBitrateInvalidSize,
        ErrorSetComplexity,
        ErrorSetComplexityInvalidSize,
        ErrorGetComplexity,
        ErrorSetSignal,
        ErrorSetSignalInvalidValue,
        ErrorGetSignal,
        ErrorSetInBandFec,
        ErrorGetInBandFec,
        ErrorSetPacketLossPerc,
        ErrorSetPacketLossPercInvalidValue,
        ErrorGetPacketLossPerc,
        ErrorUnDefined
    }
}