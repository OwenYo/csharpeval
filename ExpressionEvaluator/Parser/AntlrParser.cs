using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Antlr4.Runtime;

namespace ExpressionEvaluator.Parser
{
    public class AntlrParser : IParser
    {
        public string ExpressionString { get; set; }
        public Expression Expression { get; set; }

        public TypeRegistry TypeRegistry { get; set; }
        
        public Dictionary<string, Type> DynamicTypeLookup { get; set; }

        public CompilationContext Context { get; set; }

        public AntlrParser()
        {
        }

        public AntlrParser(string expression)
        {
            ExpressionString = expression;
        }

        public Expression Parse(Expression scope, bool isCall = false)
        {
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(ExpressionString));
            var input = new AntlrInputStream(ms);
            var lexer = new CSharp4Lexer(input);
            var tokens = new CommonTokenStream(lexer);
            if (TypeRegistry == null) TypeRegistry = new TypeRegistry();


            var parser = new CSharp4Parser(tokens);
            //{ TypeRegistry = TypeRegistry, Scope = scope, IsCall = isCall, DynamicTypeLookup = DynamicTypeLookup, Context = System.Runtime.Remoting.Contexts.Context };
            if (ExternalParameters != null)
            {
                //parser.ParameterList.Add(ExternalParameters);
            }

            

            switch (ExpressionType)
            {
                case ExpressionEvaluator.ExpressionType.Expression:
                    var expression = parser.expression();
                    break;
                case ExpressionEvaluator.ExpressionType.Statement:
                    var statement = parser.statement();
                    //if (statement != null)
                    //{
                    //    Expression = statement.Expression;
                    //}
                    break;
                case ExpressionEvaluator.ExpressionType.StatementList:
                    var statements = parser.statement_list();
                    //Expression = statements.ToBlock();
                    break;
            }
            return Expression;
        }

        public object Global { get; set; }

        public ExpressionType ExpressionType { get; set; }

        public List<ParameterExpression> ExternalParameters { get; set; }
        public Type ReturnType { get; set; }

    }
}
