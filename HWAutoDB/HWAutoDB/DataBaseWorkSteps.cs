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
            string lastRowFirsName = responseTable.Rows[numOfRows - 1]["FirstName"].ToString();
            string lastRowLastName = responseTable.Rows[numOfRows - 1]["LastName"].ToString();
            Assert.AreEqual(table.Rows[0]["FirstName"], lastRowFirsName);
            Assert.AreEqual(table.Rows[0]["LastName"], lastRowLastName);
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

        [When(@"I try to create row in table Persons '(.*)'")]
        public void WhenITryToCreateRowInTablePersons(string tableName, Table table)
        {
            string query = $"BEGIN TRY INSERT INTO {tableName} (FirstName, LastName, Age, City) " +
                $"VALUES ('{table.Rows[0]["FirstName"]}', '{table.Rows[0]["LastName"]}', " +
                $"'{table.Rows[0]["Age"]}', '{table.Rows[0]["City"]}') " +
                $"END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber; END CATCH;";
            DataTable responseTable = _sqlHelper.MakeQuery(query);
            _scenarioContext["ErrorTable"] = responseTable;
        }

        [Then(@"I get an error message '(.*)' in response")]
        public void ThenIGetAnErrorMessageInResponse(int expectedErrorNumber)
        {
            DataTable responseTable = _scenarioContext.Get<DataTable>("ErrorTable");
            int responseErrorNumber = int.Parse(responseTable.Rows[0]["ErrorNumber"].ToString());
            Assert.AreEqual(expectedErrorNumber, responseErrorNumber);
        }

        [When(@"I create row in table '(.*)' without FirstName field")]
        public void WhenICreateRowInTableWithoutFirstNameField(string tableName, Table table)
        {
            string query = $"BEGIN TRY INSERT INTO {tableName} (LastName, Age, City) " +
                $"VALUES ('{table.Rows[0]["LastName"]}', " +
                $"'{table.Rows[0]["Age"]}', '{table.Rows[0]["City"]}') " +
                $"END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber; END CATCH;";
            DataTable responseTable = _sqlHelper.MakeQuery(query);
            _scenarioContext["ErrorTable"] = responseTable;
        }

        [When(@"I create row in table '(.*)' without LastName field")]
        public void WhenICreateRowInTableWithoutLastNameField(string tableName, Table table)
        {
            string query = $"BEGIN TRY INSERT INTO {tableName} (FirstName, Age, City) " +
                $"VALUES ('{table.Rows[0]["FirstName"]}', " +
                $"'{table.Rows[0]["Age"]}', '{table.Rows[0]["City"]}') " +
                $"END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber; END CATCH;";
            DataTable responseTable = _sqlHelper.MakeQuery(query);
            _scenarioContext["ErrorTable"] = responseTable;
        }

        [When(@"I create row in table '(.*)' without Age field")]
        public void WhenICreateRowInTableWithoutAgeField(string tableName, Table table)
        {
            string query = $"BEGIN TRY INSERT INTO {tableName} (FirstName, LastName, City) " +
                $"VALUES ('{table.Rows[0]["FirstName"]}', '{table.Rows[0]["LastName"]}', " +
                $"'{table.Rows[0]["City"]}') " +
                $"END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber; END CATCH;";
            DataTable responseTable = _sqlHelper.MakeQuery(query);
            _scenarioContext["ErrorTable"] = responseTable;
        }

        [When(@"I create row in table '(.*)' without City field")]
        public void WhenICreateRowInTableWithoutCityField(string tableName, Table table)
        {
            string query = $"BEGIN TRY INSERT INTO {tableName} (FirstName, LastName, Age) " +
                $"VALUES ('{table.Rows[0]["FirstName"]}', '{table.Rows[0]["LastName"]}', " +
                $"'{table.Rows[0]["Age"]}') " +
                $"END TRY BEGIN CATCH SELECT ERROR_NUMBER() AS ErrorNumber; END CATCH;";
            DataTable responseTable = _sqlHelper.MakeQuery(query);
            _scenarioContext["ErrorTable"] = responseTable;
        }

        [When(@"I update last row in table '(.*)' with new data")]
        public void WhenIUpdateLastRowInTableWithNewData(string tableName, Table table)
        {
            string selectQuery = $"SELECT * FROM {tableName}";
            DataTable responseTable = _sqlHelper.MakeQuery(selectQuery);
            _scenarioContext["PersonsTable"] = responseTable;
            int numOfRows = responseTable.Rows.Count;
            int lastPersonId = int.Parse(responseTable.Rows[numOfRows - 1]["PersonID"].ToString());
            string updateQuery = $"UPDATE {tableName} " +
                $"SET FirstName = '{table.Rows[0]["FirstName"]}', LastName = '{table.Rows[0]["LastName"]}', " +
                $"Age = {table.Rows[0]["Age"]}, City = '{table.Rows[0]["City"]}' " +
                $"WHERE PersonID={lastPersonId};";
            _sqlHelper.MakeQuery(updateQuery);
        }
    }
}