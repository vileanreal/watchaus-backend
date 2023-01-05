using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace DBHelper
{
    public class BaseManager : IDisposable
    {
        public static string CONNECTION_STRING { get; set; }
        private MySqlConnection _conn;
        private MySqlTransaction _trans;
        private MySqlCommand _cmd;
        private bool _useTransaction;
        private Dictionary<string, object> _parameters;


        protected long LastInsertedId
        {
            get
            {
                return _cmd.LastInsertedId;
            }
        }


        public BaseManager()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            _conn = new MySqlConnection(CONNECTION_STRING);
            _conn.Open();
            _cmd = _conn.CreateCommand();
            _parameters = new Dictionary<string, object>();
        }

        protected void ClearParameters()
        {
            _cmd.Parameters.Clear();
            _parameters.Clear();
        }

        protected void AddParameter(string parameterName, object value)
        {
            _parameters.Add(parameterName, value);
            _cmd.Parameters.Add(new MySqlParameter(parameterName, value));
        }


        public void BeginTransaction()
        {
            _useTransaction = true;
            _cmd.Dispose();
            _cmd = _conn.CreateCommand();
            _trans = _conn.BeginTransaction();
            _cmd.Transaction = _trans;
        }


        public void Commit()
        {
            if (_useTransaction)
            {
                _trans.Commit();
            }
        }

        public void Rollback()
        {
            if (_useTransaction)
            {
                _trans.Rollback();
            }
        }


        // EXECUTE SP
        protected void ExecuteSp(string sp)
        {
            var result = _conn.Query(sp, _parameters, commandType: CommandType.StoredProcedure);
            ClearParameters();
        }


        // RETURN SQL DATA READER
        protected MySqlDataReader ExecuteReader(string sql)
        {
            _cmd.CommandText = sql;
            var result = _cmd.ExecuteReader();
            ClearParameters();
            return result;
        }


        // RETURN A SINGLE RECORD FROM QUERY
        protected T SelectSingle<T>(string sql)
        {
            var result = _conn.QuerySingleOrDefault<T>(sql, _parameters);
            ClearParameters();
            return result;
        }
        protected Task<T> SelectSingleAsync<T>(string sql)
        {
            var result = _conn.QuerySingleOrDefaultAsync<T>(sql, _parameters);
            ClearParameters();
            return result;
        }


        // RETURN MULTIPLE ROWS FROM SELECT
        protected List<T> SelectList<T>(string sql)
        {
            var result = _conn.Query<T>(sql, _parameters);
            ClearParameters();
            return result.ToList();
        }
        protected Task<IEnumerable<T>> SelectListAsync<T>(string sql)
        {
            var result = _conn.QueryAsync<T>(sql, _parameters);
            ClearParameters();
            return result;
        }



        // FOR INSERT / UPDATE / DELETE
        protected void ExecuteQuery(string sql)
        {
            _cmd.CommandText = sql;
            _cmd.ExecuteNonQuery();
            ClearParameters();
        }

        protected Task ExecuteQueryAsync(string sql)
        {
            _cmd.CommandText = sql;
            var result = _cmd.ExecuteNonQueryAsync();
            ClearParameters();
            return result;
        }


        public void Close()
        {
            _cmd?.Dispose();
            _trans?.Dispose();
            _conn?.Dispose();
        }

        public void Dispose()
        {
            Close();
        }
    }
}