using Autofac;
using System.Text.RegularExpressions;
using Calc.app_code.Model;
using Autofac.Core;

public interface ICalcFlowManager
{
    public double Calc(string equetion);
}

public interface ICalcOperator
{
    public double Operate (double x, double y);
}

public interface IEquationParser
{
    public ParsedEquationSimple Parse(string equation);
}

public class Multiply: ICalcOperator
{
    public double Operate (double x, double y)
    {
        return x * y;
    }
}

public class Substract : ICalcOperator
{
    public double Operate(double x, double y)
    {
        return x - y;
    }
}

public class Add : ICalcOperator
{
    public double Operate(double x, double y)
    {
        return x + y;
    }
}

public class Divide : ICalcOperator
{
    public double Operate(double x, double y)
    {
        if(y == 0)
        {
            throw new InvalidOperationException("Cannot divide in zero");
        }

        return x / y;
    }
}

public class CalcFlowManagerSimple: ICalcFlowManager
{
    private readonly IEquationParser _parser;
    public CalcFlowManagerSimple(IEquationParser parser)
    {
        _parser = parser;
    }

    public double Calc(string equation)
    {
        ParsedEquationSimple result = _parser.Parse(equation);
        return result.Operator.Operate(result.FirstOperand, result.SecondOperand);
    }
}

public class EquationParser: IEquationParser
{
    private readonly IComponentContext _context;
    public EquationParser(IComponentContext context)
    {
        _context = context;
    }
    public ParsedEquationSimple Parse(string equation)
    {
        Regex regex = new Regex("([0-9 /.]+)(.)([0-9 /.]+)");
        Match match = regex.Match(equation);
        if (!match.Success)
        {
            throw new InvalidOperationException("Invalid equation format");
        }

        double firstOperand = Double.Parse(match.Groups[1].Value);
        double secondOperand = Double.Parse(match.Groups[3].Value);
        var calcOperator = _context.ResolveKeyed<ICalcOperator>(match.Groups[2].Value);

         
        return new ParsedEquationSimple(calcOperator, firstOperand, secondOperand);
    }
}
