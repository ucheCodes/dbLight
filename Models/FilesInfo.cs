using System;
using System.Collections;
using System.Collections.Generic;

namespace FilesUploadApi.Models
{
    public class FilesInfo
    {
        public int FileCount  { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string ByteConverterString { get; set; }
        public byte[] ByteString { get; set; }
        public List<byte[]> ByteArray { get; set; }
    }
}