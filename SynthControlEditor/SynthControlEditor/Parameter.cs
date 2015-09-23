using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SynthControlEditor
{
    [Serializable]
    public class Parameter : ICloneable
    {
        public enum Types {NONE, CC7, CC14, RPN7, RPN14, NRPN7, NRPN14, SYSEX, PB, AT};
        public enum Translators {
            NONE, TRANSLATOR1, TRANSLATOR2, TRANSLATOR3,
            TRANSLATOR4, TRANSLATOR5, TRANSLATOR6, TRANSLATOR7, TRANSLATOR8,
            TRANSLATOR9, TRANSLATOR10, TRANSLATOR11, TRANSLATOR12, TRANSLATOR13,
            TRANSLATOR14, TRANSLATOR15, TRANSLATOR16, TRANSLATOR17, TRANSLATOR18,
            ASCII, NOTES
        };

        public string nameShort;
        public string nameLong;
        public byte type;
        public byte layers;
        public ushort number_l1;
        public ushort number_l2;
        public ushort number_l3;
        public ushort number_l4;
        public bool separateChannels;
        public ushort min;
        public ushort max;
        public ushort stepSize;
        public short displayOffset;
        public byte translator;
        public byte translatorOffset;
        public bool translatorUseLastItemForExceeding;
        public Sysex sysex;
        
        public Parameter()
        {
            nameShort = "";
            nameLong = "";
            type = (byte)Types.NONE;
            layers = 1;
            number_l1 = 0;
            number_l2 = 0;
            number_l3 = 0;
            number_l4 = 0;
            separateChannels = false;
            min = 0;
            max = 127;
            stepSize = 1;
            displayOffset = 0;
            translator = (byte)Translators.NONE;
            translatorOffset = 0;
            translatorUseLastItemForExceeding = false;
            sysex = new Sysex();
        }

        public static ushort Create14bitValue(byte lsb, byte msb)
        {
            return (ushort)((msb << 7) | lsb);
        }

        public static byte MSB7bit(ushort val)
        {
            return (byte)(val / 128);
        }

        public static byte LSB7bit(ushort val)
        {
            return (byte)(val % 128);
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        #region ICloneable Members
        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }
        #endregion
    }
}
