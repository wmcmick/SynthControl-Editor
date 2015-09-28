using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace SynthControlEditor
{
    public class Preset : ICloneable
    {  
        public string name;
        public string rootPath;
        public string folderName;
        //public List<Page> pages;
        public List<Descriptor> descriptors;
        //public List<Translator> translators;
        
        public Preset()
        {
            //pages = new List<Page>();
            descriptors = new List<Descriptor>();
            //translators = new List<Translator>();
        }

        public void SaveDescriptors()
        {
            string sFolder = Path.Combine(rootPath, folderName);
            string sFileName = Path.Combine(sFolder, "descr.lst");
            //byte index = 0;
            //string sLine = "";

            if (File.Exists(sFileName))
                File.Delete(sFileName);
            BinaryWriter writer = new BinaryWriter(File.Open(sFileName, FileMode.Create));
            sFileName = Path.Combine(sFolder, "descr_names.txt");
            StreamWriter textWriter = new StreamWriter(sFileName, false);

            for (byte i = 0; i < descriptors.Count; i++)
            {
                writer.Write(i);
                writer.Write(BitConverter.GetBytes(descriptors[i].length));
                writer.Write(BitConverter.GetBytes(descriptors[i].offset));
                textWriter.WriteLine(descriptors[i].name);
            }

            writer.Write('\n');

            for (byte i = 0; i < descriptors.Count; i++)
            {
                string[] lines = descriptors[i].GetLines();
                for (int j = 0; j < lines.Length; j++)
                {
                    writer.Write(  Encoding.ASCII.GetBytes(lines[j].PadRight(13, ' '))  );
                    writer.Write('\n');
                }
            }

            

            writer.Close();
            textWriter.Close();
        }

        public void LoadDescriptors()
        {
            descriptors.Clear();
            
            string sFolder = Path.Combine(rootPath, folderName);
            string sFileName = Path.Combine(sFolder, "descr.lst");
            byte index = 0;
            string sLine = "";

            // Read the descriptors
            if (File.Exists(sFileName))
            {
                BinaryReader reader = new BinaryReader(File.Open(sFileName, FileMode.Open));

                while((index = reader.ReadByte()) != (byte)'\n')
                {
                    Descriptor descriptor = new Descriptor();
                    descriptor.index = index;
                    descriptor.length = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    descriptor.offset = BitConverter.ToUInt16(reader.ReadBytes(2), 0);
                    descriptor.name = "Descriptor " + (index + 1).ToString();
                    descriptors.Add(descriptor);
                }

                for (index = 0; index < descriptors.Count; index++)
                {
                    string[] sTmp = new string[descriptors[index].length];
                    for(int i=0; i<descriptors[index].length;i++)
                    {
                        sTmp[i] = Encoding.ASCII.GetString(reader.ReadBytes(13)).TrimEnd();
                        reader.ReadByte(); // Read new line
                    }
                    descriptors[index].SetLines(sTmp);
                }

                reader.Close();
            }
            // Read names
            sFileName = Path.Combine(sFolder, "descr_names.lst");
            if (File.Exists(sFileName))
            {
                TextReader textReader = File.OpenText(sFileName);
                index = 0;
                while ((sLine = textReader.ReadLine()) != null)
                    descriptors[index++].name = sLine;
                textReader.Close();
            }
        }

        public static void ReadUntilNewLine(BinaryReader r)
        {
            char c = ' ';
            while (c != '\n')
                c = r.ReadChar();
        }

        public bool Save()
        {
            return true;
        }

        public void Load()
        {
            /*string sFolder = Path.Combine(rootPath, folderName);

            if (File.Exists(Path.Combine(sFolder, "pages.lst")))
            {
                BinaryReader reader = new BinaryReader(File.Open(Path.Combine(Path.Combine(rootPath, folderName), "pages.lst"), FileMode.Open));
                int i = 1;
                while (reader.PeekChar() > 0)
                {
                    string sTmp = Encoding.ASCII.GetString(reader.ReadBytes(17)).Trim();

                    if (File.Exists(Path.Combine(sFolder, "p" + i.ToString() + ".pag")))
                    {
                        Page page = new Page();
                        page.Load(Path.Combine(sFolder, "p" + i.ToString() + ".pag"));
                        page.name = sTmp;
                        pages.Add(page);
                    }

                    // Read new line character
                    if (reader.PeekChar() > 0)
                        reader.ReadByte();

                    i++;
                }
                reader.Close();
            }*/
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

            DirectoryCopy.CopyDirectory(Path.Combine(sPresetsPath, folderName.Trim().Replace("\0", "")), Path.Combine(Path.Combine(sPresetsPath, "PresetToExport"), folderName.Trim().Replace("\0", "")), true);

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

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
