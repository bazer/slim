﻿using Remotion.Linq.Clauses;
using Slim.Query;
using System;
using System.Linq.Expressions;

namespace Slim.Linq.Visitors
{
    internal class OrderByVisitor : ExpressionVisitor
    {
        protected SqlQuery select;
        protected OrderingDirection direction;

        internal OrderByVisitor(SqlQuery query)
        {
            this.select = query;
        }

        internal void Parse(Ordering ordering)
        {
            direction = ordering.OrderingDirection;
            Visit(ordering.Expression);
        }

        //protected override Expression VisitExtension(Expression node)
        //{
        //    return base.VisitExtension(node);
        //}

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.NodeType == ExpressionType.MemberAccess)
            {
                var column = select.GetColumn(node);

                select.OrderBy(column, null, direction == OrderingDirection.Asc);
            }
            else
                throw new NotImplementedException("Operation not implemented");

            return node;
        }

        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    if (node.NodeType == ExpressionType.AndAlso)
        //    {
        //        Visit(node.Left);
        //        Visit(node.Right);

        //        return node;
        //    }

        //    var fields = select.GetFields(node);

        //    var where = select.Where(fields.Key);

        //    if (node.NodeType == ExpressionType.Equal)
        //        where.EqualTo(fields.Value);
        //    else if (node.NodeType == ExpressionType.NotEqual)
        //        where.NotEqualTo(fields.Value);
        //    else if (node.NodeType == ExpressionType.GreaterThan)
        //        where.GreaterThan(fields.Value);
        //    else if (node.NodeType == ExpressionType.GreaterThanOrEqual)
        //        where.GreaterThanOrEqual(fields.Value);
        //    else if (node.NodeType == ExpressionType.LessThan)
        //        where.LessThan(fields.Value);
        //    else if (node.NodeType == ExpressionType.LessThanOrEqual)
        //        where.LessThanOrEqual(fields.Value);
        //    else
        //        throw new NotImplementedException("Operation not implemented");

        //    return node;
        //}
    }
}