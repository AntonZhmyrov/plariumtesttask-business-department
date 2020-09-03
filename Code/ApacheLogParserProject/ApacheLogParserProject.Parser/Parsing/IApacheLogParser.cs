using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Parser.Parsing
{
    public interface IApacheLogParser
    {
        /// <summary>
        /// Parses the apache logs presented as the array of strings
        /// </summary>
        Task<ILog[]> ParseAsync(string[] logStrings);
    }
}