using System;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.utils
{
    // Utility class for generating consistent API responses.

    public static class ApiResponse
    {
        public static IActionResult Success<T>(T data, string message = "Success")
        {
            /// Generates a successful API response with the provided data and optional message.

            return new ObjectResult(new ApiResponseTemplate<T>(true, data, message, 200));
        }

        public static IActionResult Created<T>(T data, string message = "Resource Created")
        {
            /// Generates a successful API response for resource creation with the provided data and optional message.

            return new ObjectResult(new ApiResponseTemplate<T>(true, data, message, 201));
        }

        public static IActionResult NotFound(string message = "Resource not found")
        {
            /// Generates a "Not Found" API response with the provided message.

            return new ObjectResult(new ApiResponseTemplate<object>(false, "", message, 404));
        }

        public static IActionResult Conflict(string message = "Conflict Detected")
        {
            /// Generates a "Conflict" API response with the provided message.

            return new ObjectResult(new ApiResponseTemplate<object>(false, "", message, 409));
        }

        public static IActionResult BadRequest(string message = "Bad request")
        {
            /// Generates a "Bad Request" API response with the provided message.

            return new ObjectResult(new ApiResponseTemplate<object>(false, "", message, 400));
        }

        public static IActionResult UnAuthorized(string message = "Unauthorized access")
        {
            /// Generates an "Unauthorized" API response with the provided message.

            return new ObjectResult(new ApiResponseTemplate<object>(false, "", message, 401));
        }

        public static IActionResult Forbidden(string message = "Forbidden access")
        {
            /// Generates a "Forbidden" API response with the provided message.

            return new ObjectResult(new ApiResponseTemplate<object>(false, "", message, 403));
        }

        public static IActionResult ServerError(string message = "Internal server error")
        {
            /// Generates a "Server Error" API response with the provided message.

            return new ObjectResult(new ApiResponseTemplate<object>(false, "", message, 500));
        }
    }

    /// Template for API responses.
    public class ApiResponseTemplate<T>
    {
        /// Indicates if the API call was successful.
        public bool Success { get; set; }

        /// The data returned by the API call.
        public T Data { get; set; }

        /// A message providing additional information about the response.
        public string Message { get; set; }

        /// The status code of the API response.
        public int StatusCode { get; set; }

        /// Constructs a new instance of ApiResponseTemplate with the specified properties.
        public ApiResponseTemplate(bool success, T data, string message, int statusCode = 200)
        {
            Success = success;
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
