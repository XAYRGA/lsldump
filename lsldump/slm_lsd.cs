using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Be.IO;

namespace lsldump
{



    public static class slm_lsd
    {
        public static uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }

        public static void parse()
        {

        }

        public static Dictionary<string,object> parseMap(BeBinaryReader br, out int size)
        {
            var basel = br.BaseStream.Position;
            var w = parseMap(br);
            size = (int)(br.BaseStream.Position - basel);
            return w;
        }

        public static Dictionary<string,object> parseMap(BeBinaryReader br)
        {
        
            Dictionary<string, object> map = new Dictionary<string, object>();
            if (br.ReadChar() == '{')
            {
                var keyCount = br.ReadInt32();
                for (int i=0; i < keyCount;i++)
                {
                    var k = readKey(br);
                    object data = readDataType(br);
                    map[k] = data;
                }
                var close = br.ReadChar(); // Should be }.

            } else
            {
                throw new Exception("Invalid LLSD Map Parse call");
            }
            return map;
        }


        public static string readKey(BeBinaryReader br)
        {
            var type = br.ReadChar();
            if (type!='k')
            {
                throw new InvalidDataException("No k-type indicator, data misaligned.");
            }
            var keyLen = br.ReadInt32();
            byte[] keyData = br.ReadBytes(keyLen);
            return Encoding.ASCII.GetString(keyData);
        }

        public static object readDataType(BeBinaryReader br)
        {
            var type = br.ReadChar();
            Console.WriteLine("Type {0} at {1:X}", type, br.BaseStream.Position);
            var size = 0;
            byte[] buffer;

            switch (type)
            {
              
                case 'u':
                    return util.getByteString(br.ReadBytes(16)); // 16 byte uid 
                case 'i':
                    return br.ReadInt32();
                case 'r':
                    return br.ReadDouble();
                case 'l':
                case 's':
                    size = br.ReadInt32();
                    buffer = br.ReadBytes(size);
                    return Encoding.ASCII.GetString(buffer);
                case 'b':
                    size = br.ReadInt32();
                    buffer = br.ReadBytes(size);
                    return buffer;
                case '{':
                    br.BaseStream.Seek(-1, SeekOrigin.Current); // parseMap requires a {, so we're going to seek back.
                    return parseMap(br);
                case 'd':
                    return br.ReadInt64();               
            }

            return null;
        }
    }
}
