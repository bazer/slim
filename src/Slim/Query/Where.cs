﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Slim.Extensions;
using Remotion.Linq.Clauses;
using System.Linq.Expressions;

namespace Slim.Query
{
    public enum Relation
    {
        Equal,
        EqualNull,
        NotEqual,
        NotEqualNull,
        Like,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual
    }

    public interface IWhere<T> : IQueryPart
    {
        //IWhere<T> And(string columnName);

        //IWhere<T> Or(string columnName);
        //WhereContinuation<T> EqualTo<V>(V value);
        //WhereContinuation<T> EqualTo<V>(V value, bool isValue);
        //WhereContinuation<T> NotEqualTo<V>(V value);
        //WhereContinuation<T> Like<V>(V value);
        //WhereContinuation<T> GreaterThan<V>(V value);
        //WhereContinuation<T> GreaterThanOrEqual<V>(V value);
        //WhereContinuation<T> LessThan<V>(V value);
        //WhereContinuation<T> LessThanOrEqual<V>(V value);
    }

    public class Where<T> : IWhere<T>
    {
        private string Key;
        private object Value;
        private Relation Relation;
        private bool IsValue = true;
        protected WhereGroup<T> Container;
        private string KeyAlias;
        private string ValueAlias;

        private string KeyName => string.IsNullOrEmpty(KeyAlias)
            ? Key
            : $"{KeyAlias}.{Key}";

        private string ValueName => string.IsNullOrEmpty(ValueAlias)
            ? Value as string
            : $"{ValueAlias}.{Value}";

        internal Where(WhereGroup<T> container, string key, string keyAlias, bool isValue = true)
        {
            this.Container = container;
            this.Key = key;
            this.IsValue = isValue;
            this.KeyAlias = keyAlias;
        }

        internal Where(WhereGroup<T> container)
        {
            this.Container = container;
        }

        internal Where<T> AddKey(string key, string alias, bool isValue = true)
        {
            this.Key = key;
            this.IsValue = isValue;
            this.KeyAlias = alias;

            return this;
        }

        public WhereGroup<T> EqualTo<V>(V value)
        {
            return SetAndReturn(value, value == null ? Relation.EqualNull : Relation.Equal);
        }

        public WhereGroup<T> EqualToColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.Equal);
        }

        public WhereGroup<T> NotEqualTo<V>(V value)
        {
            return SetAndReturn(value, value == null ? Relation.NotEqualNull : Relation.NotEqual);
        }

        public WhereGroup<T> NotEqualToColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.NotEqual);
        }

        public WhereGroup<T> Like<V>(V value)
        {
            return SetAndReturn(value, Relation.Like);
        }

        public WhereGroup<T> LikeColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.Like);
        }

        public WhereGroup<T> GreaterThan<V>(V value)
        {
            return SetAndReturn(value, Relation.GreaterThan);
        }

        public WhereGroup<T> GreaterThanColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.GreaterThan);
        }

        public WhereGroup<T> GreaterThanOrEqual<V>(V value)
        {
            return SetAndReturn(value, Relation.GreaterThanOrEqual);
        }

        public WhereGroup<T> GreaterThanOrEqualToColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.GreaterThanOrEqual);
        }

        public WhereGroup<T> LessThan<V>(V value)
        {
            return SetAndReturn(value, Relation.LessThan);
        }

        public WhereGroup<T> LessThanColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.LessThan);
        }

        public WhereGroup<T> LessThanOrEqual<V>(V value)
        {
            return SetAndReturn(value, Relation.LessThanOrEqual);
        }

        public WhereGroup<T> LessThanOrEqualToColumn(string column, string alias = null)
        {
            return SetAndReturnColumn(column, alias, Relation.LessThanOrEqual);
        }

        protected WhereGroup<T> SetAndReturn<V>(V value, Relation relation)
        {
            this.Value = value;
            this.Relation = relation;

            return this.Container;
        }

        protected WhereGroup<T> SetAndReturnColumn(string column, string alias, Relation relation)
        {
            if (alias == null)
                (column, alias) = QueryUtils.ParseColumnNameAndAlias(column);

            this.Value = column;
            this.ValueAlias = alias;
            this.IsValue = false;
            this.Relation = relation;

            return this.Container;
        }

        public void AddCommandString(Sql sql, string prefix, bool addCommandParameter = true, bool addParentheses = false)
        {
            if (addCommandParameter)
                GetCommandParameter(sql, prefix);

            if (addParentheses)
                sql.AddText("(");

            if (IsValue)
                Container.Query.Transaction.Provider.GetParameterComparison(sql, KeyName, Relation, prefix + "w" + sql.IndexAdd());
            else
                sql.AddFormat("{0} {1} {2}", KeyName, Relation.ToSql(), ValueName);

            if (addParentheses)
                sql.AddText(")");
        }

        protected void GetCommandParameter(Sql sql, string prefix)
        {
            if (IsValue)
                Container.Query.Transaction.Provider.GetParameter(sql, prefix + "w" + sql.Index, Value);
        }
    }
}
