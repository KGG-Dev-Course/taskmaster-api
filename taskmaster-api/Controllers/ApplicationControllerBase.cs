using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using taskmaster_api.Data.DTOs;
using taskmaster_api.Data.DTOs.Interface;

namespace taskmaster_api.Controllers
{
    public class ApplicationControllerBase : ControllerBase
    {
        public ApplicationControllerBase() { }

        protected IActionResult ToHttpResult(ICoreActionResult coreActionResult)
        {
            if (coreActionResult.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(ToExceptionDto(coreActionResult));
        }

        protected IActionResult ToHttpResult<T>(ICoreActionResult<T> coreActionResult)
        {
            if (coreActionResult.IsSuccess)
            {
                return Ok(coreActionResult.Data);
            }

            return BadRequest(ToExceptionDto(coreActionResult));
        }

        private CoreExceptionDto ToExceptionDto<T>(ICoreActionResult<T> result)
        {
            CoreExceptionDto e = new CoreExceptionDto();

            e.errorCode = result.ErrorCode;
            e.errorMessage = result.ErrorMessage;
            e.suppressToastMessage = false;
            e.stackTrace = String.Empty;

            return e;
        }

        private CoreExceptionDto ToExceptionDto(ICoreActionResult result)
        {
            CoreExceptionDto e = new CoreExceptionDto();

            e.errorCode = result.ErrorCode;
            e.errorMessage = result.ErrorMessage;
            e.suppressToastMessage = false;
            e.suppressSideBarAlert = false;
            e.stackTrace = String.Empty;

            return e;
        }
    }
}
