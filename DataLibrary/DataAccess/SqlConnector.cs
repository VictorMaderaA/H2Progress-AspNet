using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    /// <summary>
    /// State identifica donde ocurrio el error
    /// State = -1 -> Error en "SqlQuery" (Conexion Posiblemente)
    /// State = -2 -> Error en al intentar devolver un dato Get row/value   
    /// </summary>

    public static class SqlConnector
    {
        private static readonly object Padlock = new object();

        public static SqlConnectionStringBuilder Builder { get; } = new SqlConnectionStringBuilder();

        public static event EventHandler<string> OnSqlQueryStarted;
        public static event EventHandler<string> OnSqlQueryFinished;
        public static event EventHandler<string> OnSqlQueryFailed;



        public static void SetBuilder(string connectionString)
        {
            Builder.ConnectionString = connectionString;
        }

        private static DataTable SqlQuery(SqlCommand cmd, out int state, QueryType queryType = QueryType.Select)
        {
            var tabla = new DataTable();
            state = -1;
            using (var connection = new SqlConnection(Builder.ConnectionString))
            {
                cmd.Connection = connection;
                try
                {
                    OnSqlQueryStarted?.Invoke(null, cmd.CommandText);
                    connection.Open();
                    if (queryType == QueryType.Select)
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
                    Console.WriteLine(e);

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

        public static DataTable GetTable(SqlCommand cmd, out int state)
        {
            var table = SqlQuery(cmd, out state);
            return table;
        }


        public static DataTable GetTable(SqlCommand cmd)
        {
            var table = SqlQuery(cmd, out _);
            return table;
        }


        public static DataRow GetRow(SqlCommand cmd, out int state, int row = 0)
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


        public static DataRow GetRow(SqlCommand cmd, int row = 0)
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


        public static string GetValue(SqlCommand cmd, out int state, int row = 0, int column = 0)
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

        public static string GetValue(SqlCommand cmd, out int state, int row = 0, string columnName = "")
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


        public static string GetValue(SqlCommand cmd, int row = 0, int column = 0)
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

        public static void ExecuteQuery(SqlCommand cmd, out int state)
        {
            SqlQuery(cmd, out state, QueryType.Execute);
        }


        public static void ExecuteQuery(SqlCommand cmd)
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
