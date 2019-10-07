using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;
using dbmanager.Common.Repositories;
using dbmanager.Common.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace dbmanager.Tests.Services
{
    public class GenerateScriptServiceTest
    {
        [Fact]
        public async Task CREATE_TABLE_SCRIPT_ISVALID()
        {
            // Arrange
            var tableName = "Test";
            var schema = "dbo";
            var columns = new List<Column>
            {
                new Column { Name = "id", Type = "int", IsNullable = "NO"},
                new Column { Name = "name", Type = "nvarchar", CharacterMaximumLength = 100, IsNullable = "YES"}
            };

            var repo = new Mock<IDbInfoRepository>();
            repo.Setup(d => d.GetColumnsAsync(It.IsAny<Table>())).ReturnsAsync(columns);

            var generateScriptService = new GenerateScriptService(repo.Object);

            var expectableScript = @$"CREATE TABLE dbo.Test (
                                        id int NOT NULL,
                                        name nvarchar(100)
                                        );";

            //Act
            var script = await generateScriptService.GetCreateTableScriptAsync(string.Empty, schema, tableName);

            //Assert
            script.Should().NotBeNullOrWhiteSpace().Should().Equals(expectableScript);
        }
    }
}
