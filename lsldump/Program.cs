using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace lsldump
{
    class lsldump
    {
        

        static void Main(string[] args)
        {
#if DEBUG 
            args = new string[3];
            args[0] = "slm";
            args[1] = "test.slm";
            args[2] = "out.obj";
        
#endif

            if (args.Length < 1)
            {
                Console.WriteLine("lsldump <operation> ");
                return;
            }

            if (args[0]=="vfs")
            {
                if (args.Length < 4)
                {
                    Console.WriteLine("lsldump vfs <index> <data> <outfolder>");
                    return;
                }
                vfs.dumpVFS(args);
                return;
            }


            if (args[0] == "slm")
            {
                if (args.Length < 3)
                {
                    Console.WriteLine("lsldump slm <input> <output>");
                    Console.WriteLine("Converts the <input>.slm to an <output>.obj");
                    return;
                }
                slm.open(args);
                return;
            }



        }
    }
}
