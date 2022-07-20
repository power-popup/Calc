using Autofac;
using System.Text.RegularExpressions;

public interface ICalc {
    public double? Calculate (double x, double y);
}

public interface ICalcOperator
{
    public double? Operate (double x, double y);
}

public interface IEquationParser
{
    ICalcOperator? Operator { get; set; }
    double? FirstOperand { get; set; }
    double? SecondOperand { get; set; }
    public void Parse(string equation);
    public Boolean Valid();
}

public class Calculator: ICalc
{
    private ICalcOperator CalcOperator;

    public Calculator(ICalcOperator operat)
    {
        CalcOperator = operat;
    }

    public double? Calculate (double x, double y)
    {
        return CalcOperator.Operate(x, y);
    }
}

public class Multiply: ICalcOperator
{
    public double? Operate (double x, double y)
    {
        return x * y;
    }
}

public class Substract : ICalcOperator
{
    public double? Operate(double x, double y)
    {
        return x - y;
    }
}

public class Add : ICalcOperator
{
    public double? Operate(double x, double y)
    {
        return x + y;
    }
}

public class Divide : ICalcOperator
{
    public double? Operate(double x, double y)
    {
        if (y > 0)
        {
            return x / y;
        }
        return null;
    }
}

public class EquationParser: IEquationParser
{
    public ICalcOperator? Operator { get; set; }
    public double? FirstOperand { get; set; }
    public double? SecondOperand { get; set; }

    public EquationParser(string equation)
    {
        Parse(equation);
    }


    public void Parse(string equation)
    {
        Regex regex = new Regex("([0-9 /.]+)(.)([0-9 /.]+)");
        Match match = regex.Match(equation);
        if (!match.Success)
        {
            return;
        }

        FirstOperand = Double.Parse(match.Groups[1].Value);
        SecondOperand = Double.Parse(match.Groups[3].Value);
        var container = ContainerConfig.ConfigureOperator();
        using (var scope = container.BeginLifetimeScope())
        {
            Operator = scope.ResolveKeyed<ICalcOperator>(match.Groups[2].Value);
        }
    }

    public Boolean Valid()
    {
        return FirstOperand != null && SecondOperand != null && Operator != null;
    }
}
