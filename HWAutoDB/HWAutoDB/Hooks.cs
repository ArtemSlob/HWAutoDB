using TechTalk.SpecFlow;

namespace HWAutoDB
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            SqlConnectorHelper _sqlHelper = new SqlConnectorHelper();
            _sqlHelper.ConnectToDataBase();
            _scenarioContext.Add("SqlHelper", _sqlHelper);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            SqlConnectorHelper _sqlHelper = _scenarioContext.Get<SqlConnectorHelper>("SqlHelper");
            _sqlHelper.CloseConnection();
        }
    }
}