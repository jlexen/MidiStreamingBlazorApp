using System;

namespace fnix.alpha.Models
{
    public class KeyboardData
    {
        public KeyboardData()
        {
            KeyCount = 88; // default to 88 key 
        }

        public int KeyCount { get; set; }

        public int StartKey =>
            KeyCount switch
            {
                88 => 21,
                76 => 28,
                61 => 36,
                49 => 36,
                37 => 48,
                25 => 48,
                _ => throw new Exception("Invalid keyboard size")
            };

        public int EndKey =>
            KeyCount switch
            {
                88 => 108,
                76 => 103,
                61 => 96,
                49 => 84,
                37 => 84,
                25 => 72,
                _ => throw new Exception("Invalid keyboard size")
            };
    }
}
