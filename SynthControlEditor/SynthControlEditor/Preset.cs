using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace SynthControlEditor
{
    public class Preset
    {
        public string name;
        public string folderName;
        public List<Page> pages;
        
        public Preset()
        {
            pages = new List<Page>();
        }

        public bool Save()
        {
            return true;
        }

        public bool Export(string sFileName, string sPresetsPath)
        {
            //ZipFile zipFile = new ZipFile(sFileName);
            Directory.CreateDirectory(Path.Combine(sPresetsPath, "PresetToExport"));

            BinaryWriter writer = new BinaryWriter(File.Open(Path.Combine(Path.Combine(sPresetsPath, "PresetToExport"), "preset.info"), FileMode.Create));
            writer.Write(Encoding.ASCII.GetBytes(folderName));
            writer.Write(Encoding.ASCII.GetBytes("\n"));
            writer.Write(Encoding.ASCII.GetBytes(name));
            writer.Close();

            DirectoryCopy.CopyDirectory(Path.Combine(sPresetsPath, folderName), Path.Combine(Path.Combine(sPresetsPath, "PresetToExport"), folderName), true);

            FastZip fastZip = new FastZip();
            fastZip.CreateEmptyDirectories = true;
            fastZip.CreateZip(sFileName, Path.Combine(sPresetsPath, "PresetToExport"), true, null);

            Directory.Delete(Path.Combine(sPresetsPath, "PresetToExport"), true);
            
            return true;
        }

        public bool Import(string sFileName, string sPresetsPath)
        {
            FastZip fastZip = new FastZip();
            fastZip.CreateEmptyDirectories = true;
            fastZip.ExtractZip(sFileName, sPresetsPath, null);

            BinaryReader binaryReader = new BinaryReader(File.Open(Path.Combine(sPresetsPath, "preset.info"), FileMode.Open));
            byte[] file = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            binaryReader.Close();
            string fileText = System.Text.Encoding.ASCII.GetString(file);
            string[] lines = fileText.Split('\n');
            folderName = lines[0].Trim();
            name = lines[1].Trim();

            File.Delete(Path.Combine(sPresetsPath, "preset.info"));
            //if(!Directory.Exists( Path.Combine( Path.Combine(sPresetsPath, folderName), "sysex") ))
            //    Directory.CreateDirectory(Path.Combine( Path.Combine(sPresetsPath, folderName), "sysex") );

            return true;
        }
    }
}
