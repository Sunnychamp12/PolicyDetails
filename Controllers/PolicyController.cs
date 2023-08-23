using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PolicyDetails.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Microsoft.OpenApi.Extensions;
using System.Runtime.Serialization;

namespace PolicyDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public PolicyController(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetPolicyDetailsById([FromHeader] string headerToken)
        {
            Response response = new Response();
            SuccessResponse successResponse = new SuccessResponse();
            try
            {
                string CustomerCode = Request.Query["Customer_Code"].ToString();
                string APPName = Request.Headers["APPName"].ToString();
                bool isValid = headerToken == "c06fc4189a5645e4a4fd480e8b1556e7";

                if (isValid && APPName == Enums.Enums.APPName.PolicyDetails.ToString())
                {
                    successResponse.PolicyDetails = _dbContext.getPolicyDataList(CustomerCode);

                    successResponse.Status = Enums.Enums.status.Success.ToString();
                    if (successResponse.PolicyDetails != null && successResponse.PolicyDetails.Count > 0)
                        return Ok(successResponse);
                    else
                    {
                        response.Status = Enums.Enums.status.Error.ToString();
                        response.Message = "Invalid customer code";
                        return Ok(response);
                        // Git checkin check.
                    }
                }
                else
                {
                    response.Status = Enums.Enums.status.Error.ToString();
                    response.Message = "Invalid API Key";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = Enums.Enums.status.Error.ToString();
                response.Message = ex.Message.ToString();
                return Ok(response);
            }
        }
    }
}