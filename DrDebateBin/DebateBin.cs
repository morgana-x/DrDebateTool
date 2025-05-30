﻿namespace DrDebateBin
{
    public class DebateBin
    {
        public List<DebateSection> Sections = new List<DebateSection>();
        public short UnknownHeaderValue;
        public int SectionValueCount;
        public DebateBin(BinaryReader reader)
        {
            UnknownHeaderValue = reader.ReadInt16();
     
            ushort NumberOfSections = reader.ReadUInt16(); 

            SectionValueCount = (int)((reader.BaseStream.Length-4) / NumberOfSections);
            SectionValueCount &= 0xFFFE; //https://github.com/AdmiralCurtiss/HyoutaTools/blob/master/HyoutaToolsLib/DanganRonpa/Nonstop/NonstopFile.cs
            SectionValueCount /= 2; // https://github.com/AdmiralCurtiss/HyoutaTools/blob/master/HyoutaToolsLib/DanganRonpa/Nonstop/NonstopSingle.cs

            for (int i = 0; i < NumberOfSections; i++)
                Sections.Add(new DebateSection(reader, SectionValueCount));
        }

        public DebateBin(StreamReader reader)
        {
            reader.BaseStream.Position = 0;
            UnknownHeaderValue = Util.StringToVal(Util.ReadLine(reader));
            SectionValueCount = Util.StringToVal(Util.ReadLine(reader));

            while (!reader.EndOfStream)
                Sections.Add(new DebateSection(reader, SectionValueCount));
        }

        public void WriteToString(StreamWriter writer)
        {
            var borderStr = new string('/', 40);

            writer.WriteLine(borderStr);
            writer.WriteLine("// Metadata");
            writer.WriteLine(borderStr);

            writer.WriteLine(Util.ValToString("BinHeader", UnknownHeaderValue));
            writer.WriteLine(Util.ValToString("SectionCount", SectionValueCount));
 
            for (int i=0; i<Sections.Count;i++)
            {
                writer.WriteLine(borderStr);
                writer.WriteLine($"// Section {i+1}");
                writer.WriteLine(borderStr);

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

        public static void Extract(string filepath, string outpath = "")
        {
            if (outpath == "")
                outpath = filepath.Replace(".dat", "") + ".txt";

            using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(fs);
            using var writer = new StreamWriter(outpath);

            new DebateBin(reader).WriteToString(writer);
        }
        public static void Repack(string filepath, string outpath = "")
        {
            if (outpath == "")
                outpath = filepath.Replace(".txt", "") + ".dat";

            using var fs = new FileStream(outpath, FileMode.Create, FileAccess.Write);
            using var reader = new StreamReader(filepath);
            using var writer = new BinaryWriter(fs);

            new DebateBin(reader).WriteToBinary(writer);
        }
    }
}
