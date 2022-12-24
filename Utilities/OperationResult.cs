namespace Utilities
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public List<object> Errors { get; set; }



        public static OperationResult<T> Success(string? message = "Success")
        {
            return new OperationResult<T>()
            {
                IsSuccess = true,
                Message = message,
                Data = default(T)
            };
        }

        public static OperationResult<T> Success(T? data, string? message = "Success")
        {
            return new OperationResult<T>()
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static OperationResult<T> Failed(string? message)
        {
            return new OperationResult<T>()
            {
                IsSuccess = false,
                Message = message,
                Data = default(T)
            };
        }

        public static OperationResult<T> FailedWithResult(T? data, string? message)
        {
            return new OperationResult<T>()
            {
                IsSuccess = false,
                Message = message,
                Data = data
            };
        }
    }





    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object Data { get; set; }



        public static OperationResult Success(string? message = "Success")
        {
            return new OperationResult()
            {
                IsSuccess = true,
                Message = message,
                Data = null
            };
        }
 

        public static OperationResult Failed(string? message = "Operation failed")
        {
            return new OperationResult()
            {
                IsSuccess = false,
                Message = message,
                Data = null
            };
        }

    }
}
