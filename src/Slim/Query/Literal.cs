﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Modl.Db.DatabaseProviders;

namespace Modl.Db.Query
{
    public class Literal : IQuery
    {
        private string sql;
        IEnumerable<IDataParameter> parameters;
        protected DatabaseProvider provider;
        public DatabaseProvider DatabaseProvider { get { return provider; } }

        public Literal(DatabaseProvider database, string sql)
        {
            this.provider = database;
            this.sql = sql;
            this.parameters = new List<IDataParameter>();
        }

        public Literal(DatabaseProvider database, string sql, IEnumerable<IDataParameter> parameters)
        {
            this.provider = database;
            this.sql = sql;
            this.parameters = parameters;
        }

        public Sql ToSql(string paramPrefix)
        {
            return new Sql(sql, parameters.ToArray());
        }

        public IDbCommand ToDbCommand()
        {
            return DatabaseProvider.ToDbCommand(this);
        }

        public int ParameterCount
        {
            get { return parameters.Count(); }
        }

        //public IEnumerable<IDataParameter> QueryPartsParameters()
        //{
        //    return new List<IDataParameter>();
        //}
    }
}