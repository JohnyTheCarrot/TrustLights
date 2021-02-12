using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TrustLightsCS
{
    public class TrustCommand
    {
        private readonly byte[] _header = new byte[43];
        private byte[] _data;

        public void SetFrame(byte num)
        {
            _header[0] = num;
        }

        public void SetType(byte num)
        {
            _header[2] = num;
        }

        public void SetMac(string mac)
        {
            var bytes = TrustCryptography.StringToByteArray(mac);
            if (bytes.Length != 6) return;
            for (var i = 0; i < bytes.Length; i++)
                _header[3 + i] = bytes[i];
        }

        public void SetMagic()
        {
            const int num = 653213;
            InsertInt32(num, 9);
        }

        public void SetEntityId(int entityId)
        {
            InsertInt32(entityId, 29);
        }

        public void SetData(string data, string aes)
        {
            _data = TrustCryptography.EncryptStringToBytes_Aes(data, aes);
            Debug.WriteLine(BitConverter.ToString(_data).Replace("-", string.Empty));
        }

        private void InsertInt32(int toInsert, int spot)
        {
            var intBytes = BitConverter.GetBytes(toInsert);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(intBytes);
            var result = intBytes;
            for (var i = 0; i < result.Length; i++)
                _header[spot + i] = result[i];
        }


        public string GetCommand()
        {
            _header[41] = (byte)(_data.Length & 0xFF);
            _header[42] = (byte)(_data.Length >> 8 & 0xFF);
            var header = BitConverter.ToString(_header).Replace("-", string.Empty);
            var data = BitConverter.ToString(_data).Replace("-", string.Empty);
            return header + data;
        }

        public static TrustCommand SimpleCommand(string mac, string aes, int entity, int function, long value)
        {
            var command = new TrustCommand();
            command.SetMac(mac);
            command.SetType(128);
            command.SetMagic();
            command.SetEntityId(entity);
            command.SetData("{\"module\":{\"id\":" + entity + ",\"function\":" + function + ",\"value\":" + value + "}}", aes);
            return command;
        }
    }
}
