using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPPlus.FrameWork.Practice.Common
{
    public class ZipHelper
    {
        public List<Stream> UnZip(Stream inStream, string password = null)
        {
            try
            {
                var streams = new List<Stream>();
                using (var zipInStream = new ZipInputStream(inStream))
                {
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        zipInStream.Password = password;//md5.encrypt(password);
                    }
                    ZipEntry entry;
                    //压缩文件的第一个文件夹
                    while ((entry = zipInStream.GetNextEntry()) != null)
                    {
                        string fileName = Path.GetFileName(entry.Name);
                        //以下为解压zip文件的基本步骤
                        //基本思路：遍历压缩文件里的所有文件，创建一个相同的文件
                        if (fileName != String.Empty)
                        {
                            Stream outStream = new MemoryStream();
                            int size = 4096;
                            byte[] data = new byte[4096];
                            while (true)
                            {
                                size = zipInStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    outStream.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            streams.Add(outStream);
                        }
                    }
                    return streams;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Stream> CompressFile(Dictionary<string, Stream> fileDic, string password)
        {
            var result = new MemoryStream();
            var zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(result);
            zipStream.Password = string.Empty;
            zipStream.SetLevel(9); //0-9, 9 being the highest level of compression
            foreach (var data in fileDic)
            {
                var newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(data.Key)
                {
                    DateTime = DateTime.Now,
                    IsUnicodeText = true
                };
                zipStream.PutNextEntry(newEntry);
                var length = data.Value.Length < 1024 ? 1024 : data.Value.Length;
                ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(data.Value, zipStream, new byte[length]);
                zipStream.CloseEntry();
                zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
            }
            zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.
            result.Position = 0;
            return result;
        }
    }
}
