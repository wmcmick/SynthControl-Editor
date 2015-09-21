using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    [Serializable]
    public class Sysex
    {
        enum Checksum { NONE, ROLAND, WALDORF };
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
            SetMessage("000000", 2);
            SetMessage("000000", 3);
            SetMessage("000000", 4);
            length = (byte)message.Count;
            valueLsbPosition = 16;
            valueMsbPosition = 2;
            parameterPosition = 15;
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
