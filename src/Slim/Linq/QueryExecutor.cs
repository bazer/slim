﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Modl.Db.Query;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ExpressionVisitors;
using Remotion.Linq.Parsing.Structure;
using Slim.Instances;
using Slim.Interfaces;
using Slim.Metadata;

namespace Slim.Linq
{
    internal class QueryExecutor : IQueryExecutor
    {
        internal QueryExecutor(DatabaseProvider databaseProvider, Table table)
        {
            this.DatabaseProvider = databaseProvider;
            this.Table = table;
        }

        private DatabaseProvider DatabaseProvider { get; }

        private Table Table { get; }

        private Select ParseQueryModel(QueryModel queryModel)
        {
            var select = GetSelect();

            foreach (var body in queryModel.BodyClauses)
            {
                if (body is WhereClause where)
                {
                    select.Where(where);
                }
            }

            return select;
        }

        public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
        {

            var select = ParseQueryModel(queryModel);

            if (Table.PrimaryKeyColumns.Count != 0)
            {
                select.What(Table.PrimaryKeyColumns);

                var keys = ParseQueryModel(queryModel)
                    .ReadKeys()
                    .ToArray();

                foreach (var row in Table.Cache.GetRows(keys))
                    yield return (T)row;
            }
            else
            {
                var rows = ParseQueryModel(queryModel)
                    .ReadInstances()
                    .Select(InstanceFactory.NewImmutableRow);

                foreach (var row in rows)
                    yield return (T)row;
            }
        }

        public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
        {
            var sequence = ExecuteCollection<T>(queryModel);

            return returnDefaultWhenEmpty ? sequence.SingleOrDefault() : sequence.Single();
        }

        public T ExecuteScalar<T>(QueryModel queryModel)
        {
            var keys = ParseQueryModel(queryModel)
                .ReadKeys();

            //var results = ParseQueryModel(queryModel)
            //    .ReadInstances();

            if (queryModel.ResultOperators.Any())
            {
                if (queryModel.ResultOperators[0].ToString() == "Count()")
                    return (T)(object)keys.Count();
            }

            throw new NotImplementedException();
        }

        private Select GetSelect()
        {
            return new Select(DatabaseProvider, Table);
        }
    }
}
