using Microsoft.EntityFrameworkCore.Migrations;

namespace ApacheLogParserProject.Data.Migrations
{
    public partial class SP_AddGetHostsSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				SET ANSI_NULLS ON
				GO
				SET QUOTED_IDENTIFIER ON
				GO

				CREATE PROCEDURE GetHosts
					@NumberOfHosts INT,
					@Start DATETIME2(7) = null,
					@End DATETIME2(7) = null
				AS
				BEGIN

				SET NOCOUNT ON;

				SELECT TOP (@NumberOfHosts) 
					ClientIpAddress AS HostIpAddress, 
					ClientGeolocation AS HostCountry, 
					count(ClientIpAddress) as NumberOfRequests
				FROM [LogsDb].[dbo].[Logs]
				WHERE (@Start IS NULL OR RequestDateTime >= @Start) AND
						(@End IS NULL OR RequestDateTime <= @End)
				GROUP BY ClientIpAddress, ClientGeolocation
				ORDER BY NumberOfRequests DESC

			END
			GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
