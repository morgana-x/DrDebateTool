namespace DrDebateBin
{
    public class DebateSection
    {
        public List<short> Values = new List<short>();

        public DebateSection(BinaryReader br, int numVals)
        {
            for (int i = 0; i < numVals; i++)
                Values.Add(br.ReadInt16());
        }
        public DebateSection(StreamReader reader, int numVals)
        {
            for (int i = 0; i < numVals; i++)
                Values.Add(Util.StringToVal(Util.ReadLine(reader)));
        }
        
        public void WriteToBinary(BinaryWriter bw)
        {
            foreach(var val in Values)
                bw.Write(val);
        }
        public void WriteToString(StreamWriter bw)
        {
            for (int i = 0; i < Values.Count; i++)
                bw.WriteLine("\t" + Util.ValToString(DebateOpcode.GetOpcodeKeyName(i), Values[i]));
        }
    }
}
