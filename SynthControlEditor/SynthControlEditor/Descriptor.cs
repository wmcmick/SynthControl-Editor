using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynthControlEditor
{
    public class Descriptor
    {
        public enum Types { NONE, ASCII, SEMITONES, QUARTERTONES, CUSTOM };

        public string name;
        public ushort length;
        public ushort offset;
        public byte indexOld;
        public byte indexNew;
        private string[] lines;

        public Descriptor()
        {
            name = "";
            length = 0;
            offset = 0;
            indexOld = 0;
            indexNew = 0;
            lines = null;
        }

        public void SetLines(string[] sLines)
        {
            lines = sLines;
        }

        public string GetLines()
        {
            if (lines != null)
                return String.Join(Environment.NewLine, lines);
            else
                return null;
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
            int octave = (noteValue + offset) / 12;
            int note = (noteValue + offset) % 12;
            return notes[note] + octave.ToString();
        }

        public static string GetQuartertone(int noteValue, int offset)
        {
            string[] notes = { "C  ", "C +", "C# ", "C#+", "D  ", "D +", "D# ", "D#+", "E  ", "E +", "F  ", "F +", "F# ", "F#+", "G  ", "G +", "G# ", "G#+", "A  ", "A +", "A# ", "A#+", "B  ", "B +" };
            int octave = (noteValue + offset) / 24;
            int note = (noteValue + offset) % 24;
            return notes[note] + octave.ToString();
        }
    }
}
