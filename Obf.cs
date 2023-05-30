using dnlib.DotNet.Writer;
using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wzo0sX6pWg
{
    class Obf
    {
        static public void SaveToFile(ModuleDefMD moduleDef, string path, string obf_path)
        {
            ModuleWriterOptions moduleWriterOption = new ModuleWriterOptions(moduleDef);
            moduleWriterOption.MetadataOptions.Flags = moduleWriterOption.MetadataOptions.Flags | MetadataFlags.KeepOldMaxStack;
            moduleWriterOption.Logger = DummyLogger.NoThrowInstance;
            moduleDef.Write(obf_path, moduleWriterOption);
        }

        static public List<int> SearchBytePattern(byte[] pattern, byte[] bytes)
        {
            List<int> positions = new List<int>();
            int patternLength = pattern.Length;
            int totalLength = bytes.Length;
            byte firstMatchByte = pattern[0];
            for (int i = 0; i < totalLength; i++)
            {
                if (firstMatchByte == bytes[i] && totalLength - i >= patternLength)
                {
                    byte[] match = new byte[patternLength];
                    Array.Copy(bytes, i, match, 0, patternLength);
                    if (match.SequenceEqual<byte>(pattern))
                    {
                        positions.Add(i);
                        i += patternLength - 1;
                    }
                }
            }
            return positions;
        }
    }
}
