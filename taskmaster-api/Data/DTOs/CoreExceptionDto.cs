namespace taskmaster_api.Data.DTOs
{
    public class CoreExceptionDto
    {
        public CoreExceptionDto()
        {
            this.errorCode = "COMMON.UNHANDLED_EXCEPTION";
            this.errorMessage = String.Empty;
            this.stackTrace = String.Empty;
            this.suppressToastMessage = false;
            this.suppressSideBarAlert = false;
        }
        public string errorMessage { get; set; }
        public string errorCode { get; set; }
        public string stackTrace { get; set; }
        public bool suppressToastMessage { get; set; }
        public bool suppressSideBarAlert { get; set; }
    }
}
