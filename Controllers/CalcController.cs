using Microsoft.AspNetCore.Mvc;
using Autofac;

namespace Calc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        private readonly ICalcFlowManager _calcFlowManager;
        public CalcController(ICalcFlowManager calcFlowManager)
        {
            _calcFlowManager = calcFlowManager;
        }
        public JsonResult Post([FromBody] string equation)
        {
            double result;
            try
            {
                result = _calcFlowManager.Calc(equation);
            }
            catch(Exception ex)
            {
                return new JsonResult(new { Success = "False", responseText = ex.Message });
            }

            return new JsonResult(Ok(result));
        }
    }
}
