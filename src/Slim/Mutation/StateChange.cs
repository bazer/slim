﻿using Slim.Instances;
using Slim.Interfaces;
using Slim.Metadata;
using Slim.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Slim.Mutation
{

    public class StateChange
    {
        public TransactionChangeType Type { get; }
        public IModel Model { get; }
        public Table Table { get; }

        public PrimaryKeys PrimaryKeys { get; }
        public bool HasAutoIncrement =>
            Table.PrimaryKeyColumns.Any(x => x.AutoIncrement);


        public StateChange(IModel model, Table table, TransactionChangeType type)
        {
            Model = model;
            Table = table;
            Type = type;

            PrimaryKeys = new PrimaryKeys(Table.PrimaryKeyColumns.Select(x => x.ValueProperty.GetValue(Model)));
        }

        public void ExecuteQuery(Transaction transaction)
        {
            if (Type == TransactionChangeType.Insert && HasAutoIncrement)
            {
                var newId = transaction.DbTransaction.ExecuteScalar(GetDbCommand(transaction));

                Table.PrimaryKeyColumns
                    .FirstOrDefault(x => x.AutoIncrement)?
                    .ValueProperty
                    .SetValue(Model, newId);
            }
            else
                transaction.DbTransaction.ExecuteNonQuery(GetDbCommand(transaction));
        }

        public IDbCommand GetDbCommand(Transaction transaction) =>
            transaction.Provider.ToDbCommand(GetQuery(transaction));

        public IQuery GetQuery(Transaction transaction)
        {
            var query = new SqlQuery(Table, transaction);

            if (Type == TransactionChangeType.Insert)
            {
                foreach (var column in Table.Columns)
                    query.Set(column.DbName, column.ValueProperty.GetValue(Model));

                if (HasAutoIncrement)
                    query.AddLastIdQuery();

                return query.InsertQuery();
            }
            else if (Type == TransactionChangeType.Update)
            {
                foreach (var key in Table.PrimaryKeyColumns)
                    query.Where(key.DbName).EqualTo(key.ValueProperty.GetValue(Model));

                foreach (var column in Table.Columns)
                    query.Set(column.DbName, column.ValueProperty.GetValue(Model));

                return query.UpdateQuery();
            }
            else if (Type == TransactionChangeType.Delete)
            {
                foreach (var key in Table.PrimaryKeyColumns)
                    query.Where(key.DbName).EqualTo(key.ValueProperty.GetValue(Model));

                return query.DeleteQuery();
            }

            throw new NotImplementedException();
        }
    }
}