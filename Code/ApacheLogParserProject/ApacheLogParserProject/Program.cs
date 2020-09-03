using System;
using System.Threading.Tasks;
using ApacheLogParserProject.Data;
using ApacheLogParserProject.Data.Repositories;
using ApacheLogParserProject.GeolocationService;
using ApacheLogParserProject.Parser.Parsing;
using Microsoft.EntityFrameworkCore;

namespace ApacheLogParserProject
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("******* Welcome to Apache Log Parser! *******");
            Console.Write("Please, specify the full path to the file containing Apache logs: ");

            try
            {
                var fullPath = Console.ReadLine();
            
                // Create db context and apply migrations to db
                var dbContext = new ApacheLogsDbContext();
                await dbContext.Database.MigrateAsync();

                // Read log lines from specified file
                var logStrings = await FileReader.ReadLinesFromFileAsync(fullPath);
                
                Console.WriteLine("The parsing has started!");

                // Parse logs
                IApacheLogParser logParser = new ApacheLogParser();
                var logs = await logParser.ParseAsync(logStrings);
                
                Console.WriteLine("The parsing has successfully finished!");
                Console.WriteLine("The load of geolocations has been started");
                
                // Fill parsed logs with geolocations
                IGeolocationFiller geolocationFiller = new GeolocationFiller();
                await geolocationFiller.FillLogsWithGeolocationAsync(logs);
                
                Console.WriteLine("The geolocations were successfully loaded");
                Console.WriteLine("The saving of logs has begun");
                
                // Save logs into th database
                IApacheLogsRepository apacheLogsRepository = new ApacheLogsRepository(dbContext);
                await apacheLogsRepository.CreateManyAsync(logs);
                
                Console.WriteLine("The saving of logs has successfully finished");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}