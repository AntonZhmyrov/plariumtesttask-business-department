using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ApacheLogParserProject
{
    public static class FileReader
    {
        public static async Task<string[]> ReadLinesFromFileAsync(string fullPath)
        {
            await using var filestream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(filestream);
            
            var fileEntries = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                fileEntries.Add(line);
            }

            return fileEntries.ToArray();
        }
    }
}