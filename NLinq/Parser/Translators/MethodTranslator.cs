﻿using System;
using System.Collections.Generic;
using System.Text;
using NLinq.Parser.DbExpressions;
using System.Linq.Expressions;

namespace NLinq.Parser.Translators
{
    internal enum MethodKind
    {
        Select,
        Where
    }

    internal abstract class MethodTranslator
    {
        public IEnumerable<MethodKind> MethodKinds;

        public MethodTranslator(params MethodKind[] methods)
        {
            this.MethodKinds = methods;
        }

        public abstract DbExpression Translate(ExpressionParser parser, MethodCallExpression callExpression);
    }

    internal class SelectMethodTranslator : MethodTranslator
    {
        public SelectMethodTranslator()
            : base(MethodKind.Select)
        { }

        public override DbExpression Translate(ExpressionParser parser, MethodCallExpression callExpression)
        {
            DbProjectExpression input = parser.Parse(callExpression.Arguments[0]) as DbProjectExpression;
            LambdaExpression lambda = parser.GetLambdaExpression(callExpression.Arguments[1]);
            DbExpression body = parser.ParseLambda(lambda);
            return new DbSelectExpression(input, body);
        }
    }

    internal class WhereMethodTranslator : MethodTranslator
    {
        public WhereMethodTranslator()
            : base(MethodKind.Where)
        { }

        public override DbExpression Translate(ExpressionParser parser, MethodCallExpression callExpression)
        {
            DbProjectExpression input = parser.Parse(callExpression.Arguments[0]) as DbProjectExpression;
            LambdaExpression lambda = parser.GetLambdaExpression(callExpression.Arguments[1]);
            DbExpression body = parser.ParseLambda(lambda);
            return new DbWhereExpression(input, body);
        }
    }
}
