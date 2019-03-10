﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NLinq.Parser.DbExpressions
{
    public class DbCompareExpression : DbExpression
    {
        public DbExpression Left;
        public DbExpression Right;

        public DbCompareExpression(DbExpression left, DbExpression right, DbExpressionKind kind)
            : base(kind)
        {
            this.Left = left;
            this.Right = right;
        }

        [System.Diagnostics.DebuggerNonUserCode]
        public override T Visit<T>(DbExpressionVisitor<T> visitor)
        {
            throw new NotImplementedException();
        }
    }
}