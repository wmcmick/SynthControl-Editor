using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    [Serializable]
    public class Sysex
    {
        enum Checksum { NONE, ROLAND, WALDORF, WALDORF_DUMMY };
        enum ChannelType { ZERO_IS_CHANNEL_ONE, ONE_IS_CHANNEL_ONE };

        public List<string> message;
        public List<string> message_l2;
        public List<string> message_l3;
        public List<string> message_l4;

        public byte length;
        public byte valueLsbPosition;
        public byte valueMsbPosition;
        public byte checksum;
        public byte channelPosition;
        public byte channelType;
        public byte parameterPosition;
        public byte parameterBytes;

        public Sysex()
        {
            SetMessage("F0000000000000000000000000000000F7", 1);
            SetMessage("00000000", 2);
            SetMessage("00000000", 3);
            SetMessage("00000000", 4);
            length = (byte)message.Count;
            valueLsbPosition = 5;
            valueMsbPosition = 2;
            parameterPosition = 4;
            parameterBytes = 1;
            checksum = (byte)Checksum.NONE;
            channelPosition = 2;    // No channel data, must be 3 or more to show
            channelType = (byte)ChannelType.ZERO_IS_CHANNEL_ONE;
        }

        public void SetMessage(string sSysex, byte iLayer)
        {
            if(iLayer == 1)
                SetMessage(ref message, sSysex);
            else if(iLayer == 2)
                SetMessage(ref message_l2, sSysex);
            else if (iLayer == 3)
                SetMessage(ref message_l3, sSysex);
            else if (iLayer == 4)
                SetMessage(ref message_l4, sSysex);
        }

        private void SetMessage(ref List<string> mess, string sSysex)
        {
            mess = new List<string>();
            for (int i = 0; i < sSysex.Length; i += 2)
            {
                mess.Add(sSysex.Substring(i, 2));
            }
        }

        public byte[] GetMessage(ushort value, byte channel, byte layer)
        {
            string mess = "";
            for (int i = 0; i < length; i++)
                mess += message[i];
            byte[] bytes = Parameter.HexStringToByteArray(mess);

            // Insert channel
            if (channelPosition > 2) // index for channel position start at 1
                bytes[channelPosition-1] = (byte)((bytes[channelPosition-1] & 0xF0) | channel);

            // Set value byte(s)
            bytes[valueLsbPosition - 1] = Parameter.LSB7bit(value);
            if (valueMsbPosition > 2)
                bytes[valueMsbPosition - 1] = Parameter.MSB7bit(value);

            // Calculate and insert checksum
            if(checksum != (byte)Checksum.NONE)
                bytes[length - 2] = calcChecksum(bytes, (Checksum)checksum);

            return bytes;
        }

        // The sysex checksum is usually calculated from the start of the command byte (address) to the last value byte
        // but sometimes from the second byte (IDW, Waldorf Music ID), example from Waldorf Pulse 2 documentation below 
        byte calcChecksum(byte[] bytes, Checksum checksumType)
        {
            int i;
            byte sysexChecksum = 0;

            if (checksumType == Checksum.WALDORF_DUMMY)
            {
                // Waldorf "dummy" checksum: "A checksum of 7Fh is always accepted as valid. This can be used if
                // data is altered manually or produced by MIDI control surfaces with limited capabilities.
                // This option should not be employed by editor programs to skip the checksum calculation."
                sysexChecksum = 0x7F;
            }
            else
            {
                // Waldorf checksum: Add all values together, save the least significant seven bits.
                // "The sum of all sysex bytes from CMD to the end of MSG"
                //
                // Roland checksum: Same as Waldorf but invert the value if not zero
                //

                // Add all the values to the checksum
                for (i = parameterPosition-1; i < length-2; i++)
                    sysexChecksum += bytes[i];
                // Save the seven least significant bits
                sysexChecksum &= 0x7F;
                // For Roland checksum, if checksum is not zero then we need to "invert" the value (128-checksum)
                if (sysexChecksum > 0 && checksumType == Checksum.ROLAND)
                    sysexChecksum = (byte)(0x80 - sysexChecksum);
            }

            return sysexChecksum;
        }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        /*public string GetMessage(byte layer)
        {
            if(layer == 2)
                return GetMessage(message_l2);
            else if (layer == 3)
                return GetMessage(message_l3);
            else if (layer == 4)
                return GetMessage(message_l4);
            else
                return GetMessage(message);
        }

        private string GetMessage(List<string> mess)
        {
            string tmpMessage = "";
            foreach (string part in mess)
            {
                tmpMessage += part;
            }
            return tmpMessage;
        }*/
    }
}
