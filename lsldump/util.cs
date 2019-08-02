using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using ZLibNet; // Microsoft's implementation doesn't work.
using System.IO.Compression;

namespace lsldump
{
    public static class util
    {
        public static string getByteString(byte[] bill)
        {
            string john = "";
            for (int i = 0; i < bill.Length; i++)
            {
                john += string.Format("{0:X}", bill[i]);
            }
            return john;
        }

        private static int level = 0; 

        public static void printMap(Dictionary<string,object> map)
        {
            var ls = level;
          
            foreach (KeyValuePair<string, object> kvp in map)
            {   
                for (int i=0; i < level; i++)
                {
                    Console.Write("\t");
                }

                if (kvp.Value is Dictionary<string, object>)
                {
                    level++;

                    Console.WriteLine("{0} = <", kvp.Key);
                    
                    printMap((Dictionary<string, object>)kvp.Value);

                    level--;
                    Console.WriteLine(">");
                }
                else
                {
                    Console.WriteLine("{0} = {1}", kvp.Key, kvp.Value);
                }
            }
            for (int i = 0; i < level; i++)
            {
                Console.Write("\t");
            }
            if (ls==0)
            {
                level = 0;
            }
        }

        public static byte[] inflate(byte[] data)
        {
           

            const int size = 65535;
            int current_size = 0;
            
            using (DeflateStream stream = new DeflateStream(new MemoryStream(data),CompressionMode.Decompress ))
            {

                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        Console.WriteLine("GZ: {0}", count);
                        current_size += count;
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);

                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
