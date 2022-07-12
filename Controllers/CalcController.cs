using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        public JsonResult Post([FromBody] string equation)
        {
            double? result = null;
            EquationParser equationParser = new EquationParser(equation);
            if (equationParser.Valid())
            {
                result = new Calculator(equationParser.Operator).Calculate((double)equationParser.FirstOperand, (double)equationParser.SecondOperand);
            }

            return new JsonResult(Ok(result));
        }
    }
}
