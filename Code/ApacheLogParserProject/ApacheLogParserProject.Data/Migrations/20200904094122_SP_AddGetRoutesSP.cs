using Microsoft.EntityFrameworkCore.Migrations;

namespace ApacheLogParserProject.Data.Migrations
{
    public partial class SP_AddGetRoutesSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				SET ANSI_NULLS ON
				GO
				SET QUOTED_IDENTIFIER ON
				GO

				CREATE PROCEDURE GetRoutes
					@NumberOfHosts INT,
					@Start DATETIME2(7) = null,
					@End DATETIME2(7) = null
				AS
				BEGIN

				SET NOCOUNT ON;

				SELECT TOP (@NumberOfHosts) RequestRoute, count(RequestRoute) as NumberOfRequests
				FROM [LogsDb].[dbo].[Logs]
				WHERE (@Start IS NULL OR RequestDateTime >= @Start) AND
						(@End IS NULL OR RequestDateTime <= @End)
				GROUP BY RequestRoute
				ORDER BY NumberOfRequests DESC

			END
			GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
