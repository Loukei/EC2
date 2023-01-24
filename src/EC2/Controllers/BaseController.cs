/// Exammple code for base controller
/// 適合用來回傳各種形式的資料範例

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using EC2.Models;

//namespace EC2.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BaseController : ControllerBase
//    {

//        protected ProductServiceResponse<T> Run<T>(Func<T> resolve) where T : class
//        {
//            ProductServiceResponse<T> response = new ProductServiceResponse<T>();
//            try
//            {
//                response.Result = resolve();
//                response.IsSuccessful = true;
//            }
//            catch (ExpectedException exp)
//            {
//                //Log.Error(exp, "Expected Exception");
//                //_logger.LogError("Expected Exception", exp);
//                response.Message = new { code = exp.ErrorCode, Message = exp.Message };
//                // response.Result = reject?.Invoke();
//            }
//            catch (Exception exp)
//            {
//                //Log.Error(exp, "ERUKN:Unknown Error");
//                // _logger.LogError("ERUKN:Unknown Error", exp);
//                response.Message = new { code = "ERUKN", Message = "UnKnown Error" };
//                // response.Result = reject?.Invoke();

//                //Elmah.ErrorSignal.FromCurrentContext().Raise(exp);
//            }
//            return response;

//        }



//    }
//}
