using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Be.IO;


namespace lsldump
{
    public static class slm
    {
        public static void open(string[] args)
        {
            var inpath = args[1];
            var obpath = args[2];
        
            var slmfile = File.OpenRead(inpath);
            // var objectfile = File.OpenRead(obpath);
            var slmreader = new BeBinaryReader(slmfile);
            int header_end = 0;
            var map = slm_lsd.parseMap(slmreader,out header_end);
            Console.WriteLine("Header ends at {0:X}",header_end);            
            util.printMap(map);

            if (map.ContainsKey("physics_convex"))
            {
                var convex = (Dictionary<string, object>)map["physics_convex"];
                var offset = (int)convex["offset"];
                var size = (int)convex["size"];
                slmreader.BaseStream.Position = offset + header_end;
                if (slmreader.ReadByte() > 0)
                {
                    slmreader.BaseStream.Seek(1, SeekOrigin.Current);
                    var ifbuffer = slmreader.ReadBytes(size);
                    var inflated = util.inflate(ifbuffer);
                    Console.WriteLine("physmesh_end {0:X}", slmreader.BaseStream.Position);
                    File.WriteAllBytes("test_phys.bin", inflated);
                } 
                // else it's really not.
            }

            if (map.ContainsKey("lowest_lod"))
            {
                var convex = (Dictionary<string, object>)map["lowest_lod"];
                var offset = (int)convex["offset"];
                var size = (int)convex["size"];
                slmreader.BaseStream.Position = offset + header_end;
                if (slmreader.ReadByte() > 0)
                {
                    slmreader.BaseStream.Seek(1, SeekOrigin.Current);
                    var ifbuffer = slmreader.ReadBytes(size );
                    var inflated = util.inflate(ifbuffer);
                    Console.WriteLine("Final  position {0:X}", slmreader.BaseStream.Position);
                    File.WriteAllBytes("test_lod_l.bin", inflated);
                }
                // else it's really not.
            }


            if (map.ContainsKey("low_lod"))
            {
                var convex = (Dictionary<string, object>)map["low_lod"];
                var offset = (int)convex["offset"];
                var size = (int)convex["size"];
                slmreader.BaseStream.Position = offset + header_end;
                Console.WriteLine("{0:X}", slmreader.BaseStream.Position);
                if (slmreader.ReadByte() > 0)
                {
                    slmreader.BaseStream.Seek(1, SeekOrigin.Current);
                    var ifbuffer = slmreader.ReadBytes(size);
                    var inflated = util.inflate(ifbuffer);
                    Console.WriteLine("Final position low {0:X}", slmreader.BaseStream.Position);
                    File.WriteAllBytes("test_low.bin", inflated);
                }
                // else it's really not.
            }



            if (map.ContainsKey("medium_lod"))
            {
                var convex = (Dictionary<string, object>)map["medium_lod"];
                var offset = (int)convex["offset"];
                var size = (int)convex["size"];
                slmreader.BaseStream.Position = offset + header_end;
                if (slmreader.ReadByte() > 0)
                {
                    slmreader.BaseStream.Seek(1, SeekOrigin.Current);
                    var ifbuffer = slmreader.ReadBytes(size);
                    var inflated = util.inflate(ifbuffer);
                    Console.WriteLine("Final position medium {0:X}", slmreader.BaseStream.Position);
                    File.WriteAllBytes("test_med.bin", inflated);
                }
                // else it's really not.
            }


            if (map.ContainsKey("high_lod"))
            {
                var convex = (Dictionary<string, object>)map["high_lod"];
                var offset = (int)convex["offset"];
                var size = (int)convex["size"];
                slmreader.BaseStream.Position = offset + header_end;

                if (slmreader.ReadByte() > 0)
                {
                    slmreader.BaseStream.Seek(1, SeekOrigin.Current);
                    var ifbuffer = slmreader.ReadBytes(size);
                    var inflated = util.inflate(ifbuffer);
                    Console.WriteLine("Final position high {0:X}", slmreader.BaseStream.Position);
                    File.WriteAllBytes("test_high.bin", inflated);
                }
                // else it's really not.
            }


            Console.ReadLine();
        }
    }
}
