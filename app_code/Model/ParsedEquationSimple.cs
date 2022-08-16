namespace Calc.app_code.Model
{
    public class ParsedEquationSimple
    {
        public ICalcOperator Operator { get; set; }
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }

        public ParsedEquationSimple(ICalcOperator @operator, double firstOperand, double secondOperand)
        {
            Operator = @operator;
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
        }
    }
}
