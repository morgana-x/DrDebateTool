namespace DrDebateBin
{
    public class DebateSection
    {
        public List<short> Values = new List<short>();

        public DebateSection(BinaryReader br, int numVals = -1)
        {
            if (numVals == -1) numVals = DebateOpcode.OpcodeNames.Count;
            for (int i = 0; i < numVals; i++)
            {
                if (br.BaseStream.Position >= br.BaseStream.Length) { Console.WriteLine("Uh oh end of stream!"); return; }
                Values.Add(br.ReadInt16());
            }
        }
        public DebateSection(StreamReader reader, int numVals = -1)
        {
            for (int i = 0; i < (numVals != -1 ? numVals : DebateOpcode.OpcodeNames.Count); i++)
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
                bw.WriteLine("\t" + DebateOpcode.ValToString(i, Values[i]));
        }
        public override string ToString()
        {
            string str = "";

            for (int i = 0; i < Values.Count;i++)
                str += DebateOpcode.ValToString(i, Values[i]);

            return str;
        }


    }
}
