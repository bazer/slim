﻿using System;
using System.Data;
using System.Data.Common;
using Modl.Db.Query;
using MySql.Data.MySqlClient;
using Slim;
using Slim.Extensions;
using Slim.Interfaces;

namespace Slim.MySql
{
    //public class MySQLProvider<T> : DatabaseProvider<T>
    //    where T : IDatabaseModel
    //{
    //    public MySQLProvider(string name, string connectionString) : base(name, connectionString, typeof(T))
    //    {
    //    }
    //}

    public class MySQLProvider<T> : DatabaseProvider<T>
        where T : class, IDatabaseModel
    {
        public MySQLProvider(string connectionString) : base(connectionString)
        {
        }

        public MySQLProvider(string connectionString, string databaseName) : base(connectionString, databaseName)
        {
        }

        //public override Transaction<T> StartTransaction()
        //{
        //    return new MySqlTransactionProvider<T>(this);
        //}

        public override DatabaseTransaction GetNewDatabaseTransaction(TransactionType type)
        {
            if (type == TransactionType.NoTransaction)
                return new MySqlDbAccess(ConnectionString, type);
            else
                return new MySqlDatabaseTransaction(ConnectionString, type);
        }

        //public override IQuery GetLastIdQuery()
        //{
        //    return new Literal(this, "SELECT last_insert_id()");
        //}

        public override string GetLastIdQuery()
        {
            return "SELECT last_insert_id()";
        }

        public override Sql GetParameterValue(Sql sql, string key)
        {
            return sql.AddFormat("?{0}", key);
        }

        public override Sql GetParameterComparison(Sql sql, string field, Relation relation, string key)
        {
            return sql.AddFormat("{0} {1} ?{2}", field, relation.ToSql(), key);
        }

        public override Sql GetParameter(Sql sql, string key, object value)
        {
            return sql.AddParameters(new MySqlParameter("?" + key, value ?? DBNull.Value));
        }

        public override IDbCommand ToDbCommand(IQuery query)
        {
            var sql = query.ToSql("");
            var command = new MySqlCommand(sql.Text);
            command.Parameters.AddRange(sql.Parameters.ToArray());

            return command;
        }


        //internal override List<IDbCommand> ToDbCommands(List<IQuery> queries)
        //{
        //    int i = 0;
        //    var sql = queries.Select(x => x.ToSql("q" + i++)).ToList();
        //    var commands = new List<IDbCommand>();

        //    var command = new MySqlCommand(string.Join("; \r\n", sql.Select(x => x.Text)), (MySqlConnection)GetConnection());
        //    command.Parameters.AddRange(sql.SelectMany(x => x.Parameters).ToArray());
        //    commands.Add(command);

        //    return commands;
        //}
    }
}