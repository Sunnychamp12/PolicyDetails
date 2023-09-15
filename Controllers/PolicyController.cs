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
using System.Data;
using System.Data.Entity;
using PolicyDetails.Models.Account;
using System.Data.SqlClient;

namespace PolicyDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        #region "Declare Common Parameters"
        private bool isValid = false;
        private string headerToken = string.Empty;
        private readonly string _strToken = "c06fc4189a5645e4a4fd480e8b1556e7";
        private readonly string _strAppName = "TransactionDetails";
        private SqlParameter[] _objSPC = null;
        #endregion

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

                if (isValid && APPName == Enums.Enums.HeaderValues.PolicyDetails.ToString())
                {
                    _objSPC = new SqlParameter[] { new SqlParameter("@AccountNo", CustomerCode) };
                    DataTable _dtPolicyDetails = DBContext.GetDataTableSP(_objSPC, "SP_GetPolicyDetails");
                    successResponse.PolicyDetails = DBContext.ConvertDataTable<PolicyData>(_dtPolicyDetails);

                    successResponse.Status = Enums.Enums.statusCode.Success.ToString();
                    if (successResponse.PolicyDetails != null && successResponse.PolicyDetails.Count > 0)
                        return Ok(successResponse);
                    else
                    {
                        response.Status = Enums.Enums.statusCode.Error.ToString();
                        response.Message = "Invalid customer code";
                        return Ok(response);
                    }
                }
                else
                {
                    response.Status = Enums.Enums.statusCode.Error.ToString();
                    response.Message = "Invalid API Key";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Status = Enums.Enums.statusCode.Error.ToString();
                response.Message = ex.Message.ToString();
                return Ok(response);
            }
        }
        #region "Get Policy Transaction"
        [HttpPost]
        public IActionResult GetPolicyTransaction([FromBody] PolicyTransactionRequest paramPolicyData)
        {
            GetPolicyTransactionResponse successResponse = new GetPolicyTransactionResponse();
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    headerToken = Request.Headers["headerToken"].ToString();
                    string APPName = Request.Headers["APPName"].ToString();
                    isValid = (headerToken == _strToken);
                    if (isValid && APPName == _strAppName)
                    {
                        _objSPC = new SqlParameter[] { new SqlParameter("@AccountNo", paramPolicyData.PolicyNo),
                            new SqlParameter("@StartDate", paramPolicyData.StartDate),
                            new SqlParameter("@EndDate", paramPolicyData.EndDate) };
                        DataTable _dtPolicyDetail = DBContext.GetDataTableSP(_objSPC, "SP_GetPolicyTransaction");
                        List<PolicyTransaction> _objPolicyTransation = DBContext.ConvertDataTable<PolicyTransaction>(_dtPolicyDetail);
                        if (_objPolicyTransation != null)
                        {
                            successResponse.customerDetails = new List<CustomerDetails>();
                            foreach (var getCustomerDetails in _objPolicyTransation.Distinct())
                            {
                                if (successResponse.customerDetails != null && !successResponse.customerDetails.Any(C => C.CustomerId == getCustomerDetails.CustomerId))
                                {
                                    CustomerDetails customerDetails = new CustomerDetails();
                                    customerDetails.CustomerId = getCustomerDetails.CustomerId;
                                    customerDetails.PremiumDate = getCustomerDetails.PremiumDate;
                                    customerDetails.PolicyNo = getCustomerDetails.PolicyNo;
                                    customerDetails.PremiumAmount = getCustomerDetails.PremiumAmount;
                                    customerDetails.PolicyStatus = getCustomerDetails.PolicyStatus;
                                    customerDetails.policyDetails = new List<PolicyData>();
                                    successResponse.customerDetails.Add(customerDetails);
                                }
                            }
                            foreach (var item in successResponse.customerDetails)
                            {
                                foreach (var getPolicyDetails in _objPolicyTransation.Where(C => C.CustomerId == item.CustomerId).ToList())
                                {
                                    PolicyData policyDetails = new PolicyData();
                                    policyDetails.ContractNumber = getPolicyDetails.ContractNumber;
                                    policyDetails.CustomerCode = getPolicyDetails.CustomerCode;
                                    policyDetails.RiskCommencementDate = getPolicyDetails.RiskCommencementDate;
                                    policyDetails.ProductName = getPolicyDetails.ProductName;
                                    policyDetails.MaturityDate = getPolicyDetails.MaturityDate;
                                    policyDetails.NextRenewalDue = getPolicyDetails.NextRenewalDue;
                                    policyDetails.SumAssuredAmount = getPolicyDetails.SumAssuredAmount;
                                    policyDetails.PremiumAmount = getPolicyDetails.PremiumAmount;
                                    policyDetails.ContractStatusCode = getPolicyDetails.ContractStatusCode;
                                    policyDetails.PolicyStatus = getPolicyDetails.PolicyStatus;
                                    policyDetails.ETLDate = getPolicyDetails.ETLDate;
                                    item.policyDetails.Add(policyDetails);
                                }
                            }
                            successResponse.Status = (int)Enums.Enums.statusCode.Success;
                            return Ok(successResponse);
                        }
                        else
                        {
                            errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                            errorResponse.Message = "Customers policy transaction details not found";
                            return NotFound(errorResponse);
                        }
                    }
                    else
                    {
                        errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                        errorResponse.Message = "This request has failed authentication";
                        return Unauthorized(errorResponse);
                    }
                }
                else
                {
                    errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                    errorResponse.Message = "Please enter valid data";
                    return BadRequest(errorResponse);
                }
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        #endregion

        #region "Get Account Transaction"
        [HttpPost]
        [Route("GetAccountTransaction")]
        public IActionResult GetAccountTransaction([FromBody] AccountTransactionRequest paramRequestData)
        {
            GetAccountTransactionResponse successResponse = new GetAccountTransactionResponse();
            ErrorResponse errorResponse = new ErrorResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    headerToken = Request.Headers[Enums.Enums.HeaderValues.HeaderToken.ToString()].ToString();
                    string APPName = Request.Headers[Enums.Enums.HeaderValues.APPName.ToString()].ToString();
                    isValid = (headerToken == _strToken);
                    if (isValid && APPName == _strAppName)
                    {
                        _objSPC = new SqlParameter[] { new SqlParameter("@AccountNo", paramRequestData.AccountNo) };
                        DataTable _dtAccount = DBContext.GetDataTableSP(_objSPC, "Proc_GetAccountDetails");
                        List<AccountDetail> _listAccount = DBContext.ConvertDataTable<AccountDetail>(_dtAccount);

                        _objSPC = new SqlParameter[] { new SqlParameter("@AccountNo", paramRequestData.AccountNo), 
                            new SqlParameter("@StartDate", paramRequestData.StartDate), 
                            new SqlParameter("@EndDate", paramRequestData.EndDate) };
                        DataTable _dtTransaction = DBContext.GetDataTableSP(_objSPC, "Proc_GetAccountTransactionDetails");
                        List<Transaction> _listTransaction = DBContext.ConvertDataTable<Transaction>(_dtTransaction);

                        if (_listAccount != null)
                        {
                            successResponse.accountDetails = new AccountDetail();
                            AccountDetail customerDetails = new AccountDetail();
                            successResponse.accountDetails.AccountNo = _listAccount[0].AccountNo;
                            successResponse.accountDetails.AccountName = _listAccount[0].AccountName;
                            successResponse.accountDetails.Address = _listAccount[0].Address;
                            successResponse.accountDetails.AccountDescription = _listAccount[0].AccountDescription;
                            successResponse.accountDetails.OpeningBalance = _listTransaction != null && _listTransaction.Count > 0 ? (_listTransaction.FirstOrDefault().Debit > 0 ?
                                        _listTransaction.FirstOrDefault().Balance + _listTransaction.FirstOrDefault().Debit : _listTransaction.FirstOrDefault().Balance - _listTransaction.FirstOrDefault().Credit) : 0;
                            successResponse.accountDetails.ClosingBalance = _listTransaction != null && _listTransaction.Count > 0 ? _listTransaction.OrderByDescending(s => s.TransDate).FirstOrDefault().Balance : 0;
                            successResponse.accountDetails.Branch = _listAccount[0].Branch;
                            successResponse.accountDetails.IFSCode = _listAccount[0].IFSCode;
                            successResponse.accountDetails.Transactions = new List<Transaction>();
                            if (_listTransaction != null && _listTransaction.Count > 0)
                            {
                                foreach (var Item_Trans in _listTransaction)
                                {
                                    Transaction _objTransaction = new Transaction();
                                    _objTransaction.TransactionId = Item_Trans.TransactionId;
                                    _objTransaction.TransDate = Item_Trans.TransDate;
                                    _objTransaction.Details = Item_Trans.Details;
                                    _objTransaction.Debit = Item_Trans.Debit;
                                    _objTransaction.Credit = Item_Trans.Credit;
                                    _objTransaction.Balance = Item_Trans.Balance;
                                    successResponse.accountDetails.Transactions.Add(_objTransaction);
                                }
                            }
                            successResponse.Status = (int)Enums.Enums.statusCode.Success;
                            return Ok(successResponse);
                        }
                        else
                        {
                            errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                            errorResponse.Message = "Customers policy transaction details not found";
                            return NotFound(errorResponse);
                        }
                    }
                    else
                    {
                        errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                        errorResponse.Message = "This request has failed authentication";
                        return Unauthorized(errorResponse);
                    }
                }
                else
                {
                    errorResponse.Status = (int)Enums.Enums.statusCode.Error;
                    errorResponse.Message = "Please enter valid data";
                    return BadRequest(errorResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion       
    }
}