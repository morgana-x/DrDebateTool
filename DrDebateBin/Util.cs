
namespace DrDebateBin
{
    public class Util
    {
        public static short StringToVal(string str)
        {
            return short.Parse(str.Substring(str.IndexOf('|') + 1).Trim());
        }
        public static string ReadLine(StreamReader reader)
        {
            if (reader.BaseStream.Position > reader.BaseStream.Length)
                return "NULL END OF STREAM";

            var line = reader.ReadLine();

            if (line.StartsWith("//"))  // If its a comment or the such
                return ReadLine(reader);

            return line;
        }
    }
}
