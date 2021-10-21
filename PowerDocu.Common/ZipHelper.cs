using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace PowerDocu.Common
{
    public class ZipHelper
    {
        public const string FlowDefinitionFile = "definition.json";

        public static List<ZipArchiveEntry> getFilesFromZip(string archiveFile, string filename)
        {
            ZipArchive archive = ZipFile.Open(archiveFile, ZipArchiveMode.Read);
            List<ZipArchiveEntry> entries = new List<ZipArchiveEntry>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.Name.Equals(filename))
                {
                    entries.Add(entry);
                }
            }
            return entries;
        }
    }
}