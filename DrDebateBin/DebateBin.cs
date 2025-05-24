using System.Reflection.Emit;

namespace DrDebateBin
{
    public class DebateBin
    {
        public List<DebateSection> Sections = new List<DebateSection>();
        public short UnknownHeaderValue;
        public int SectionValueCount;
        public DebateBin(BinaryReader reader, bool dr2=false)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            UnknownHeaderValue = reader.ReadInt16();
     
            ushort NumberOfSections = reader.ReadUInt16();

            SectionValueCount = (int)((reader.BaseStream.Length-4) / NumberOfSections);
            SectionValueCount &= 0xFFFE; //https://github.com/AdmiralCurtiss/HyoutaTools/blob/master/HyoutaToolsLib/DanganRonpa/Nonstop/NonstopFile.cs
            SectionValueCount /= 2; // https://github.com/AdmiralCurtiss/HyoutaTools/blob/master/HyoutaToolsLib/DanganRonpa/Nonstop/NonstopSingle.cs
             
            Console.WriteLine("Section value count: " + SectionValueCount.ToString());
            for (int i = 0; i < NumberOfSections; i++)
                Sections.Add(new DebateSection(reader, SectionValueCount));

        }

        public DebateBin(StreamReader reader, bool dr2=false)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            UnknownHeaderValue = Util.StringToVal(Util.ReadLine(reader));
            SectionValueCount = Util.StringToVal(Util.ReadLine(reader));

            while (reader.BaseStream.Position < reader.BaseStream.Length)
                Sections.Add(new DebateSection(reader, SectionValueCount));
        }

        public void WriteToString(StreamWriter writer)
        {
            writer.WriteLine($"BinHeader      | {UnknownHeaderValue}");
            writer.WriteLine($"BinSectionSize | {SectionValueCount}");
            for (int i=0; i<Sections.Count;i++)
            {
                writer.WriteLine($"// Section {i}");
                Sections[i].WriteToString(writer);
            }
        }

        public void WriteToBinary(BinaryWriter writer)
        {
            writer.Write(UnknownHeaderValue);
            writer.Write((ushort)Sections.Count);
            foreach (var section in Sections)
                section.WriteToBinary(writer);
        }

        public static void Extract(string filepath, string outpath = "", bool dr2=false)
        {
            if (outpath == "")
                outpath = filepath.Replace(".dat", "") + ".txt";

            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs);
            using var writer = new StreamWriter(outpath);

            new DebateBin(reader, dr2).WriteToString(writer);
        }
        public static void Repack(string filepath, string outpath = "", bool dr2=false)
        {
            if (outpath == "")
                outpath = filepath.Replace(".txt", "") + ".bin";

            using var fs = new FileStream(outpath, FileMode.Create, FileAccess.Write);
            using var reader = new StreamReader(filepath);
            using var writer = new BinaryWriter(fs);

            new DebateBin(reader, dr2).WriteToBinary(writer);
        }
    }
}
