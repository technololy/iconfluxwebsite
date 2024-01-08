using System;
using System.Text.Json.Serialization;

namespace iconfluxwebsite.Models
{
    public class ApiResponses
    {
        public ApiResponses()
        {
        }
    }

    public class APIResponse<T>
    {
        public string? Message { get; set; }
        [JsonPropertyName("isSuccessful")]
        public bool IsSuccessful { get; set; }
        public bool HasErrors => !IsSuccessful;
        public object? ServerProcessingTime { get; set; }
        public object? ErrorMessages { get; set; }
        public object? WarningMessages { get; set; }
        public object? InformationMessages { get; set; }
        public int LogId { get; set; }
        public object? SuccessMessage { get; set; }
        public object? ErrorMessage { get; set; }
        [JsonPropertyName("result")]
        public T? Result { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public static APIResponse<T> OK(T data, string message = "Operation completed successfully")
            => new APIResponse<T>
            {
                Result = data,
                IsSuccessful = true,
                SuccessMessage = message,
                StatusCode = 200,
            };

        public static APIResponse<T> Failed(string message = "Bad input. Please check your input and try again.", T data = default)
            => new APIResponse<T>
            {
                Result = data,
                ErrorMessage = message,
                StatusCode = 400,
            };

        public static APIResponse<T> InternalError(string message = "An error occured. Try again later", T data = default)
            => new APIResponse<T>
            {
                Result = data,
                ErrorMessage = message,
                StatusCode = 500,
            };

        public static APIResponse<T> InternalError(Exception ex, T data = default)
            => new APIResponse<T>
            {
                Result = data,
                ErrorMessage = $"{ex.Message}: {ex.InnerException?.Message}",
                StatusCode = 500,
            };

        public static APIResponse<T> NotFound(string message = "Resource not found.", T data = default)
            => new APIResponse<T>
            {
                Result = data,
                ErrorMessage = message,
                StatusCode = 404,
            };
    }

}

