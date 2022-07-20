using Microsoft.AspNetCore.Mvc;
using Autofac;

namespace Calc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        public JsonResult Post([FromBody] string equation)
        {
            var conatiner = ContainerConfig.ConfigureController();
            double? result = null;
            using (var scope = conatiner.BeginLifetimeScope())
            {
                IEquationParser equationParser = scope.Resolve<IEquationParser>(new TypedParameter(typeof(string), equation));
                if (equationParser.Valid())
                {
                    var calc = scope.Resolve<ICalc>(new TypedParameter(typeof(ICalcOperator), equationParser.Operator));
                    result = calc.Calculate((double)equationParser.FirstOperand, (double)equationParser.SecondOperand);
                }
            }
            return new JsonResult(Ok(result));
        }
    }
}
