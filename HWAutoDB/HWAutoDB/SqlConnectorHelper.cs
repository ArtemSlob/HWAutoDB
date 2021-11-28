using System.Data;
using System.Data.SqlClient;

namespace HWAutoDB
{
    public class SqlConnectorHelper
    {
        private SqlConnection connection;

        public void ConnectToDataBase()
        {
            connection = new SqlConnection("Server = DESKTOP-L7EBTQH\\SQLEXPRESS; Database = XSHOPX; Integrated Security = true");
            connection.Open();
        }

        public DataTable MakeQuery(string query)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (!query.StartsWith("SELECT")) return null;
            return table;
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}