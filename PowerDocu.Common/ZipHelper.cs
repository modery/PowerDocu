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

        public static List<ZipArchiveEntry> getWorkflowFilesFromZip(string archiveFile)
        {
            ZipArchive archive = ZipFile.Open(archiveFile, ZipArchiveMode.Read);
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

        public static List<ZipArchiveEntry> getFilesInPathFromZip(string archiveFile, string path, string fileExtension)
        {
            ZipArchive archive = ZipFile.Open(archiveFile, ZipArchiveMode.Read);
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

        public static ZipArchiveEntry getFileFromZip(string archiveFile, string fileName)
        {
            ZipArchive archive = ZipFile.Open(archiveFile, ZipArchiveMode.Read);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.Equals(fileName))
                {
                    return entry;
                }
            }
            return null;
        }
    }
}