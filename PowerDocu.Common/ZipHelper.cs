using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace PowerDocu.Common
{
    public static class ZipHelper
    {
        public const string FlowDefinitionFile = "definition.json";
        public const string SolutionPackageWorkflowsPath = "Workflows/";

        public static List<ZipArchiveEntry> getWorkflowFilesFromZip(Stream archiveFileStream)
        {
            ZipArchive archive = new ZipArchive(archiveFileStream,ZipArchiveMode.Read);
            List<ZipArchiveEntry> entries = new List<ZipArchiveEntry>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                //case 1: file name equals FlowDefinitionFile, then we can add it as it is an actual Flow
                //case 2: a JSON file inside the folder SolutionPackageWorkflowsPath, then it is an actual Flow inside a Solution
                if (entry.Name.Equals(FlowDefinitionFile) || (entry.FullName.StartsWith(SolutionPackageWorkflowsPath) && entry.Name.EndsWith(".json")))
                {
                    entries.Add(entry);
                }
            }
            return entries;
        }

        public static List<ZipArchiveEntry> getFilesInPathFromZip(Stream archiveFileStream, string path, string fileExtension)
        {
            ZipArchive archive = new ZipArchive(archiveFileStream,ZipArchiveMode.Read);
            List<ZipArchiveEntry> entries = new List<ZipArchiveEntry>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.StartsWith(path) && entry.Name.EndsWith(fileExtension))
                {
                    entries.Add(entry);
                }
            }
            return entries;
        }

        public static ZipArchiveEntry getFileFromZip(Stream archiveFileStream, string fileName)
        {
            ZipArchive archive = new ZipArchive(archiveFileStream,ZipArchiveMode.Read);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.Equals(fileName))
                {
                    return entry;
                }
            }
            return null;
        }

        public static void test()
        {
            Stream data = new MemoryStream(); // The original data
            Stream unzippedEntryStream; // Unzipped data from a file in the archive

            ZipArchive archive = new ZipArchive(data);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    unzippedEntryStream = entry.Open(); // .Open will return a stream
                                                        // Process entry data here
                }
            }
        }
    }
}