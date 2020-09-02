using System;
using System.Threading.Tasks;
using ApacheLogParserProject.Data;
using ApacheLogParserProject.Parser.Parsing;
using Microsoft.EntityFrameworkCore;

namespace ApacheLogParserProject
{
    class Program
    {
        // D:\Testing tasks\Plarium\apache_logs.txt
        
        private static async Task Main()
        {
            Console.WriteLine("******* Welcome to Apache Log Parser! *******");
            Console.Write("Please, specify the full path to the file containing Apache logs: ");

            try
            {
                var fullPath = Console.ReadLine();
            
                var dbContext = new ApacheLogsDbContext();
                await dbContext.Database.MigrateAsync();

                var logStrings = await FileReader.ReadLinesFromFileAsync(fullPath);
                IApacheLogParser logParser = new ApacheLogParser();
                
                Console.WriteLine("The parsing has started!");

                var logs = await logParser.ParseAsync(logStrings);
                
                Console.WriteLine();
                Console.WriteLine("The parsing has successfully finished!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}