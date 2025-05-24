namespace DrDebateBin
{
    public class DebateOpcode
    {
        public static string GetOpcodeKeyName(int opcode)
        {
            return opcode < OpcodeNames.Count ? OpcodeNames[opcode] : "0x" + opcode.ToString("X");
        }

        public static string ValToString(int i, short val)
        {
            return $"{GetOpcodeKeyName(i)} | {val}";
        }
       
        // Names Sourced from BitesizeBird's
        // https://github.com/BitesizeBird/Danganronpa-Modding/blob/master/DR2%20DAT%20Opcode%20Explanations

        public static List<string> OpcodeNames = new List<string>()
        {
            "TextID",
            "Type",
            "Slashes",
            "Shoot with Evidence",
            "Shoot with Argue Point",
            "Early Advance Frame",
            "Has Weak Point",
            "Advance",
            "0x8",
            "Entry Effect",
            "Exit Effect",
            "Fade Out",
            "Horizontal",
            "Vertical",
            "Angle Acceleration",
            "Angle",
            "Initial Scale",
            "Final Scale",
            "Text Shake",
            "Rotation",
            "Rotation Speed",
            "Character",
            "Sprite",
            "Camera",
            "Portrait Shake / Camera 2",
            "Voice",
            "Time Bonus",
            "Chapter",
            "Vertical Characters",
            "Position",
            "Music",
            "Slashed Text Mood Effect",
            "Missed Text Mood Effect",
            "0x21"
        };
    }
}
