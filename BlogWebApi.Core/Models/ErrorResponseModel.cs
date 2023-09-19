using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApi.Core.Models
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(string errorMessage, string errorTitle, int errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorTitle = errorTitle;
            ErrorCode = errorCode;
        }

        public string ErrorMessage { get; set; }
        public string ErrorTitle { get; set; }
        public int ErrorCode { get; set; }

    }
}
