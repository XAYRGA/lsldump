using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lsldump
{
    public class vfs
    {
        public static Dictionary<SLAssetType, string> fileExtensions = new Dictionary<SLAssetType, string>()
        {
            {SLAssetType.AT_LINK,".lnk" },
            {SLAssetType.AT_TEXTURE,".j2c" },
            {SLAssetType.AT_CALLINGCARD, ".cal" },
            {SLAssetType.AT_LANDMARK,".lmk" },
            {SLAssetType.AT_SCRIPT,".txt" },
            {SLAssetType.AT_SOUND,".ogg" },
            {SLAssetType.AT_SOUND_WAV,".wav" },
            {SLAssetType.AT_TEXTURE_TGA,".tga"},
            {SLAssetType.AT_IMAGE_TGA,".tga"},
            {SLAssetType.AT_IMAGE_JPEG,".jpg"},
            {SLAssetType.AT_OBJECT,".ojg" },
            {SLAssetType.AT_MESH,".slm" },
            {SLAssetType.AT_LSL_TEXT, ".lsl.txt" },
            {SLAssetType.AT_PERSON, ".per" },
            {SLAssetType.AT_LSL_BYTECODE,".lsl" },
            {SLAssetType.AT_ANIMATION,".anm" }
        };




     

        public static void dumpVFS(string[] args)
        {
            
            var ifpath = args[1];
            var dbpath = args[2];
            var of = args[3];

            var datafile = File.OpenRead(dbpath);
            var indexfile = File.OpenRead(ifpath);

            if (!Directory.Exists(of))
            {
                Directory.CreateDirectory(of);
            }

            var index = new BinaryReader(indexfile);
            var databinfile = new BinaryReader(datafile);



            while (true)
            {
                if (indexfile.Length == index.BaseStream.Position)
                {
                    Console.WriteLine("Safely reached EOF. Terminating.");
                    return;
                }
                int dataloc = index.ReadInt32();
                int length = index.ReadInt32();
                int accesstime = index.ReadInt32();
                byte[] data = index.ReadBytes(16);
                short type = index.ReadInt16();
                int blocklessSize = index.ReadInt32();

                var tname = util.getByteString(data);
                if (blocklessSize > length)
                {
                    Console.WriteLine("Unsupported multi-block: {0} {1:X} {2:X}", tname, length, blocklessSize);
                    Console.ReadLine();
                    continue;
                }


                Console.WriteLine("{0} {1:X} {2:X}",tname,length,blocklessSize);

                var AType = (SLAssetType)type;
                string ext;
                var ok = fileExtensions.TryGetValue(AType, out ext);
                if (!ok)
                {
                    ext = ".dat";
                }
                databinfile.BaseStream.Seek(dataloc, SeekOrigin.Begin);
                var buffer = databinfile.ReadBytes(blocklessSize);

                File.WriteAllBytes(of + "/" + tname + ext, buffer);
                //Console.ReadLine();

            }
            Console.WriteLine("Shouldn't ever reach here.");




        }
    }
}
