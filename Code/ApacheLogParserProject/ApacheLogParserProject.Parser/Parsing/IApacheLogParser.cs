using System.Threading.Tasks;
using ApacheLogParserProject.Models;

namespace ApacheLogParserProject.Parser.Parsing
{
    public interface IApacheLogParser
    {
        Task<ILog[]> ParseAsync(string[] logStrings);
    }
}