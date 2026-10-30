using System;
using Antlr4.Runtime.Misc;

namespace MyExcelMAUIApp
{
    public class LabCalculatorVisitor : LabCalculatorBaseVisitor<double>
    {
        // Ось цей рядок рятує все: він ігнорує нуль від EOF і повертає реальне число!
        public override double VisitCompileUnit([NotNull] LabCalculatorParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitIncDecExpr([NotNull] LabCalculatorParser.IncDecExprContext context)
        {
            double value = context.expression() != null ? Visit(context.expression()) : 0;

            if (context.operatorToken.Type == LabCalculatorLexer.INC) return value + 1;
            if (context.operatorToken.Type == LabCalculatorLexer.DEC) return value - 1;

            return 0;
        }

        public override double VisitNotExpr([NotNull] LabCalculatorParser.NotExprContext context)
        {
            double val = context.expression() != null ? Visit(context.expression()) : 0;
            return val == 0 ? 1 : 0;
        }

        public override double VisitRelationalExpr([NotNull] LabCalculatorParser.RelationalExprContext context)
        {
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));

            if (context.operatorToken.Type == LabCalculatorLexer.LT) return left < right ? 1 : 0;
            if (context.operatorToken.Type == LabCalculatorLexer.GT) return left > right ? 1 : 0;
            if (context.operatorToken.Type == LabCalculatorLexer.EQ) return left == right ? 1 : 0;

            return 0;
        }

        public override double VisitLogicalExpr([NotNull] LabCalculatorParser.LogicalExprContext context)
        {
            double left = Visit(context.expression(0));
            double right = Visit(context.expression(1));

            if (context.operatorToken.Type == LabCalculatorLexer.AND) return (left != 0 && right != 0) ? 1 : 0;
            if (context.operatorToken.Type == LabCalculatorLexer.OR) return (left != 0 || right != 0) ? 1 : 0;

            return 0;
        }

        public override double VisitNumberExpr([NotNull] LabCalculatorParser.NumberExprContext context)
        {
            if (double.TryParse(context.GetText(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            return 0;
        }

        public override double VisitParenthesesExpr([NotNull] LabCalculatorParser.ParenthesesExprContext context)
        {
            return context.expression() != null ? Visit(context.expression()) : 0;
        }
    }
}