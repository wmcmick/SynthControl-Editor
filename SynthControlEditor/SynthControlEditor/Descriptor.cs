using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    public class Descriptor
    {
        public static int MAX_DESCRIPTORS = 200;
        
        public enum Types { NONE = 0, UNIT = 1, ASCII = 2, SEMITONES = 3, QUARTERTONES = 4, CUSTOM = 255 };

        public string name;
        public ushort length;
        public ushort offset;
        public byte index;
        public byte indexOld;
        public byte indexNew;
        private string[] lines;

        public Descriptor()
        {
            name = "";
            length = 0;
            offset = 0;
            index = 0;
            indexOld = 0;
            indexNew = 0;
            lines = null;
        }

        public void SetLines(string[] sLines)
        {
            lines = sLines;
            length = (ushort)lines.Length;
        }

        public string GetText()
        {
            if (lines != null)
                return String.Join(Environment.NewLine, lines);
            else
                return null;
        }

        public string GetLine(int i, bool showExceeding)
        {
            if (i >= 0 && i < lines.Length)
                return lines[i];
            else if (showExceeding == true)
            {
                if(i<0)
                    return lines[0];
                else
                    return lines[lines.Length - 1];
            }
            else
                return "";
        }

        public string[] GetLines()
        {
            return lines;
        }

        public static string GetAsciiChar(int asciiValue, int offset)
        {
            if ((asciiValue + offset) >= 32 && (asciiValue + offset) <= 126)
            {
                char character = (char)(asciiValue + offset);
                return character.ToString();
            }
            else
                return "";
        }

        public static string GetSemitone(int noteValue, int offset)
        {
            string[] notes = { "C ", "C#", "D ", "D#", "E ", "F ", "F#", "G ", "G#", "A ", "A#", "B " };
            if (noteValue + offset >= 0)
            {
                int octave = (noteValue + offset) / 12;
                int note = (noteValue + offset) % 12;
                return notes[note] + octave.ToString();
            }
            else
                return "";
        }

        public static string GetQuartertone(int noteValue, int offset)
        {
            string[] notes = { "C  ", "C +", "C# ", "C#+", "D  ", "D +", "D# ", "D#+", "E  ", "E +", "F  ", "F +", "F# ", "F#+", "G  ", "G +", "G# ", "G#+", "A  ", "A +", "A# ", "A#+", "B  ", "B +" };
            if (noteValue + offset >= 0)
            {
                int octave = (noteValue + offset) / 24;
                int note = (noteValue + offset) % 24;
                return notes[note] + octave.ToString();
            }
            else
                return "";
        }
    }
}
