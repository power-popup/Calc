public interface ICalc {
    public double? Calculate (double x, double y);
}

public interface ICalcOperator
{
    public double? Operate (double x, double y);
}

public interface IEquationParser
{
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
    public double? FirstOperand = null;
    public double? SecondOperand = null;
    public ICalcOperator? Operator = null;

    public EquationParser(string equation)
    {
        Parse(equation);
    }

    public void Parse(string equation)
    {
        char[] operators = new char[] { 'X', '/', '+', '-' };
        var index = Array.FindIndex(operators, x => equation.Contains(x));
        char operandSymbol;
        if (index == -1)
        {
            return;
        }

        operandSymbol = operators[index];
        var operands = equation.Split(operandSymbol).ToArray();
        FirstOperand = Double.Parse(operands[0]);
        SecondOperand = Double.Parse(operands[1]);
        switch (operandSymbol)
        {
            case 'X':
                Operator = new Multiply();
                break;
            case '/':
                Operator = new Divide();
                break;
            case '+':
                Operator = new Add();
                break;
            case '-':
                Operator = new Substract();
                break;
        }
    }

    public Boolean Valid()
    {
        return FirstOperand != null && SecondOperand != null && Operator != null;
    }
}
