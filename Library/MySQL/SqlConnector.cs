using System;
using System.Data;
using Library.Utils;
using MySql.Data.MySqlClient;

namespace Library.MySQL
{
    /// <summary>
    /// State identifica donde ocurrio el error
    /// State = -1 -> Error en "SqlQuery" (Conexion Posiblemente)
    /// State = -2 -> Error en al intentar devolver un dato Get row/value   
    /// </summary>

    public sealed partial class SqlConnector
    {
        private static volatile SqlConnector _instance;
        private static readonly object Padlock = new object();
        private static DateTime _lastSqlQueryFailedErrorTime;

        private SqlConnector()
        {
        }

        public static SqlConnector Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (Padlock)
                {
                    if (_instance == null)
                        _instance = new SqlConnector();
                }

                return _instance;
            }
        }



        private MySqlConnectionStringBuilder Builder { get; } = new MySqlConnectionStringBuilder();

        public event EventHandler<string> OnSqlQueryStarted;
        public event EventHandler<string> OnSqlQueryFinished;
        public event EventHandler<string> OnSqlQueryFailed;

        public void SetBuilder(string server, string database, string userId, string password,
            MySqlSslMode sslMode = MySqlSslMode.None)
        {
            Builder.Server = server;
            Builder.Database = database;
            Builder.UserID = userId;
            Builder.Password = password;
            Builder.SslMode = sslMode;
        }

        public void SetBuilder(string connectionString, string password, MySqlSslMode sslMode = MySqlSslMode.None)
        {
            Builder.ConnectionString = connectionString;
            Builder.Password = password;
            Builder.SslMode = sslMode;
        }

        public void SetBuilder(string connectionString)
        {
            Builder.ConnectionString = connectionString;
        }



        private DataTable SqlQuery(MySqlCommand cmd, out int state, QueryType queryType = QueryType.Select)
        {
            var tabla = new DataTable();
            state = -1;
            using (var connection = new MySqlConnection(Builder.ConnectionString))
            {
                cmd.Connection = connection;
                try
                {
                    OnSqlQueryStarted?.Invoke(null, cmd.CommandText);
                    connection.Open();
                    if (queryType == QueryType.Select)
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(tabla);
                        state = 1;
                    }
                    else
                    {
                        state = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    SqlQueryException(e, cmd, state, tabla);
                }
                finally
                {
                    OnSqlQueryFinished?.Invoke(null, cmd.CommandText);
                    connection.Close();
                }
            }
            return tabla;
        }

        #region Get

        public DataTable GetTable(MySqlCommand cmd,  out int state)
        {
            var table = SqlQuery(cmd, out state);
            return table;
        }


        public DataTable GetTable(MySqlCommand cmd)
        {
            var table = SqlQuery(cmd, out _);
            return table;
        }


        public DataRow GetRow(MySqlCommand cmd, out int state, int row = 0)
        {
            var dataTable = SqlQuery(cmd, out state);
            try
            {
                var getRow = dataTable.Rows[row];
                return getRow;
            }
            catch (IndexOutOfRangeException)
            {
                state = -2;
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }


        public DataRow GetRow(MySqlCommand cmd, int row = 0)
        {
            var dataTable = SqlQuery(cmd, out _);
            try
            {
                var getRow = dataTable.Rows[row];
                return getRow;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }


        public string GetValue(MySqlCommand cmd, out int state, int row = 0, int column = 0)
        {
            var dataTable = SqlQuery(cmd, out state);
            try
            {
                var val = dataTable.Rows[row][column].ToString();
                return val;
            }
            catch (IndexOutOfRangeException)
            {
                state = -2;
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public string GetValue(MySqlCommand cmd, out int state, int row = 0, string columnName = "")
        {
            var dataTable = SqlQuery(cmd, out state);
            try
            {
                var val = dataTable.Rows[row][columnName].ToString();
                return val;
            }
            catch (IndexOutOfRangeException)
            {
                state = -2;
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }


        public string GetValue(MySqlCommand cmd, int row = 0, int column = 0)
        {
            var dataTable = SqlQuery(cmd, out _);
            try
            {
                var val = dataTable.Rows[row][column].ToString();
                return val;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        #endregion


        #region Execute

        public void ExecuteQuery(MySqlCommand cmd, out int state)
        {
            SqlQuery(cmd, out state, QueryType.Execute);
        }


        public void ExecuteQuery(MySqlCommand cmd)
        {
            SqlQuery(cmd, out _, QueryType.Execute);
        }

        #endregion


        #region Debug

        public static void PrintTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine();
                for (var x = 0; x < table.Columns.Count; x++)
                {
                    // ReSharper disable once RedundantToStringCall
                    Console.Write(row[x].ToString() + " | ");
                }
            }

            Console.WriteLine("\n");
        }

        public static void PrintRow(DataRow tableRow)
        {
            for (var x = 0; x < tableRow.Table.Columns.Count; x++)
            {
                // ReSharper disable once RedundantToStringCall
                Console.Write(tableRow[x].ToString() + " | ");
            }

            Console.WriteLine("\n");
        }

        #endregion
    }

    public enum QueryType
    {
        Select = 1,
        Execute = 2
    }
}