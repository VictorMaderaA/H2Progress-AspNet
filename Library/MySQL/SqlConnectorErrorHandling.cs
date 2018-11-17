using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Library.MySQL
{
    public sealed partial class SqlConnector
    {
        private void SqlQueryException(Exception e, IDbCommand cmd, int state, DataTable tabla)
        {
            SqlQueryError(cmd, out state, out tabla);
            if (e is MySqlException && e.Message.Contains(@"sql_mode=only_full_group_by"))
            {
#if DEBUG
                Console.WriteLine($"sql_mode=only_full_group_by => Error\nError al intentar ejecutar SqlQuery\nCommandText:\n{cmd.CommandText}");
#endif

            }
            else if (e is MySqlException)
            {
#if DEBUG
                Console.WriteLine($"Error al intentar Obtener Tabla de BD \n Error: \n {e} \n\n CommandText: '{cmd.CommandText}'");
#endif

            }
            else // Exception default
            {
#if DEBUG
                Console.WriteLine($"Error al intentar Obtener Tabla de BD \n Error: \n {e} \n\n CommandText: '{cmd.CommandText}'");
#endif
            }
        }

        private void SqlQueryError(IDbCommand cmd, out int state, out DataTable tabla)
        {
            state = -1;
            tabla = null;
            if ((DateTime.Now - _lastSqlQueryFailedErrorTime).TotalSeconds < 60.0D) return;
            _lastSqlQueryFailedErrorTime = DateTime.Now;
            OnSqlQueryFailed?.Invoke(null, cmd.CommandText);
        }
    }
}
