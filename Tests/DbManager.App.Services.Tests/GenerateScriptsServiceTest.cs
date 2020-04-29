using System.Collections.Generic;
using System.Threading.Tasks;
using DbManager.Domain.Models;
using DbManager.Domain.Models.DefaultImpl;
using DbManager.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace DbManager.App.Services.Tests
{
    public class GenerateScriptsServiceTest
    {
        [Fact]
        public async Task Create_Table_Script_IsValid()
        {
            // Arrange
            var table = new Table
            {
                Catalog = string.Empty,
                Schema = "dbo",
                Name = "Test"
            };

            var columns = new List<IColumn>
            {
                new Column {Name = "id", Type = "int", IsNullable = "NO"},
                new Column {Name = "name", Type = "nvarchar", CharactersMaxLength = 100, IsNullable = "YES"}
            };

            var repo = new Mock<ISchemaRepository>();
            repo.Setup(d => d.GetColumnsAsync(It.IsAny<ITable>())).ReturnsAsync(columns);

            var dbScriptsService = new DbScriptsService(repo.Object);

            var expectScript = "CREATE TABLE dbo.Test (" +
                               "\r\n    id int NOT NULL," +
                               "\r\n    name nvarchar(100)" +
                               "\r\n);";

            //Act
            var script = await dbScriptsService.GenerateCreateTableScriptAsync(table);

            //Assert
            script.Should().BeEquivalentTo(expectScript);
        }
    }
}