using NUnit.Framework;
using System.Data;
using TechTalk.SpecFlow;

namespace HWAutoDB
{
    [Binding]

    public class DataBaseWorkSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private SqlConnectorHelper _sqlHelper;

        public DataBaseWorkSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _sqlHelper = _scenarioContext.Get<SqlConnectorHelper>("SqlHelper");
        }

        [When(@"I create row in table '(.*)' with data")]
        public void WhenICreateRowInTableWithData(string tableName, Table table)
        {
            string query = $"INSERT INTO {tableName} (FirstName, LastName, Age, City) " +
                $"VALUES ('{table.Rows[0]["FirstName"]}', '{table.Rows[0]["LastName"]}', " +
                $"{table.Rows[0]["Age"]}, '{table.Rows[0]["City"]}');";
            _sqlHelper.MakeQuery(query);
        }

        [When(@"I select whole '(.*)' table")]
        public void WhenISelectWholeTable(string tableName)
        {
            string query = $"SELECT * FROM {tableName}";
            DataTable responseTable = _sqlHelper.MakeQuery(query);
            _scenarioContext["PersonsTable"] = responseTable;
        }

        [Then(@"Last row in table Persons contains data")]
        public void ThenLastRowInTableContainsData(Table table)
        {
            DataTable responseTable = _scenarioContext.Get<DataTable>("PersonsTable");
            int numOfRows = responseTable.Rows.Count;
            string lastAuthors = responseTable.Rows[numOfRows - 1]["FirstName"].ToString();
            Assert.AreEqual(lastAuthors, table.Rows[0]["FirstName"]);
        }

        [When(@"I delete last row in table '(.*)'")]
        public void WhenIDeleteLastRowInTable(string tableName)
        {
            DataTable responseTable = _scenarioContext.Get<DataTable>("PersonsTable");
            int numOfRows = responseTable.Rows.Count;
            int lastPersonId = int.Parse(responseTable.Rows[numOfRows - 1]["PersonID"].ToString());
            string query = $"DELETE FROM {tableName} WHERE PersonID={lastPersonId};";
            _sqlHelper.MakeQuery(query);
        }
    }
}