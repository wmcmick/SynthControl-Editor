using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SynthControlEditor
{
    public class Page
    {
        public List<string> headers;
        public List<Parameter> parameters;
        public string name;

        public Page()
        {
            Reset();
            name = "";
        }

        public bool Load(string fileName)
        {
            return true;
        }

        public bool Save(string fileName)
        {
            BinaryWriter writer = new BinaryWriter( File.Open(fileName, FileMode.Create) );
            string sTmp = "";
            byte[] bytes = null;

            //writer.Write(Encoding.ASCII.GetBytes(fileName));
            
            // Write version, 1 byte
            writer.Write((byte)1);
            // Write line break
            writer.Write(Encoding.ASCII.GetBytes("\n"));
            // Write headers
            foreach (string h in headers)
            {
                sTmp = h.PadRight(20, ' ');
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
            }
            // Write line break
            writer.Write(Encoding.ASCII.GetBytes("\n"));
            for (int i = 0; i < parameters.Count; i++)
            {
                // Write short and long names
                sTmp = parameters[i].nameShort.PadRight(4, ' ');
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
                sTmp = parameters[i].nameLong.PadRight(20, ' ');
                writer.Write(Encoding.ASCII.GetBytes(sTmp));
                // Type
                writer.Write(parameters[i].type);
                // Minimun
                bytes = BitConverter.GetBytes(parameters[i].min);
                writer.Write(bytes);
                // Maximum
                bytes = BitConverter.GetBytes(parameters[i].max);
                writer.Write(bytes);
                // Step size
                bytes = BitConverter.GetBytes(parameters[i].stepSize);
                writer.Write(bytes);
                // Display offset
                bytes = BitConverter.GetBytes(parameters[i].displayOffset);
                writer.Write(bytes);
                // Layers
                writer.Write(parameters[i].layers);
                // Numbers
                writer.Write(Parameter.LSB7bit(parameters[i].number_l1));
                writer.Write(Parameter.MSB7bit(parameters[i].number_l1));
                writer.Write(Parameter.LSB7bit(parameters[i].number_l2));
                writer.Write(Parameter.MSB7bit(parameters[i].number_l2));
                writer.Write(Parameter.LSB7bit(parameters[i].number_l3));
                writer.Write(Parameter.MSB7bit(parameters[i].number_l3));
                writer.Write(Parameter.LSB7bit(parameters[i].number_l4));
                writer.Write(Parameter.MSB7bit(parameters[i].number_l4));
                // Separate midi channels
                writer.Write(parameters[i].separateChannels ? (byte)1 : (byte)0);
                // Translator
                writer.Write(parameters[i].translator);
                writer.Write(parameters[i].translatorOffset);
                writer.Write(parameters[i].translatorUseLastItemForExceeding ? (byte)1 : (byte)0);
                // Write line break
                writer.Write(Encoding.ASCII.GetBytes("\n"));
                // -- SYSEX ---------
                // Write Sysex message, 17 bytes for main and then 3x3 bytes for layers
                foreach (string hex in parameters[i].sysex.message)
                    writer.Write(Parameter.HexStringToByteArray(hex));
                foreach (string hex in parameters[i].sysex.message_l2)
                    writer.Write(Parameter.HexStringToByteArray(hex));
                foreach (string hex in parameters[i].sysex.message_l3)
                    writer.Write(Parameter.HexStringToByteArray(hex));
                foreach (string hex in parameters[i].sysex.message_l4)
                    writer.Write(Parameter.HexStringToByteArray(hex));
                // Write line break
                writer.Write(Encoding.ASCII.GetBytes("\n"));
                // Sysex parameters
                writer.Write(parameters[i].sysex.length);
                writer.Write(parameters[i].sysex.parameterPosition);
                writer.Write(parameters[i].sysex.parameterBytes);
                writer.Write(parameters[i].sysex.valueLsbPosition);
                writer.Write(parameters[i].sysex.valueMsbPosition);
                writer.Write(parameters[i].sysex.channelPosition);
                writer.Write(parameters[i].sysex.channelType);
                writer.Write(parameters[i].sysex.checksum);
                // Write line break
                writer.Write(Encoding.ASCII.GetBytes("\n"));
            }

            writer.Close();
            
            return true;
        }

        public void Reset()
        {
            // Reset headers
            headers = new List<string>();
            for (int i = 0; i < 4; i++)
                headers.Add("");

            // Reset parameters
            parameters = new List<Parameter>();
            for (int i = 0; i < 16; i++)
                parameters.Add(new Parameter());
        }

    }
}
