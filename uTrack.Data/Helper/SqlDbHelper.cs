using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace uTrack.Data.Helper
{
    public static class SqlDbHelper
    {
        public static async Task<List<T>> ExcecuteStoredProcedureAsync<T>(this DbContext context, string storedProcName, params (string,object)[] parameters) where T : class
        {
            using (DbCommand command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = storedProcName;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 90;

                addParams(command, parameters);

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    command.Connection.Open();
                }

                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return mapToList<T>(reader);
                    }
                }
                finally
                {
                    await command.Connection.CloseAsync();
                }
            }
        }

        private static DbCommand addParams(DbCommand cmd, (string, object)[] nameValueParamPairs)
        {
            foreach(var pair in nameValueParamPairs)
            {
                DbParameter param = cmd.CreateParameter();
                param.ParameterName = pair.Item1;
                param.Value = pair.Item2 ?? DBNull.Value;
                cmd.Parameters.Add(param);

            }

            return cmd;
        }

        private static List<T> mapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
            .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
            .ToDictionary(Key => Key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach(var prop in props)
                    {
                        if (colMapping.ContainsKey(prop.Name.ToLower()))
                        {
                            var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                            prop.SetValue(obj, val == DBNull.Value ? null : val);
                        }
                    }
                    objList.Add(obj);
                }
            }

            return objList;
        }
    }
}
