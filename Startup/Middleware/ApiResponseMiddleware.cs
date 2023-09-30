using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Startup
{


    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture the response stream
            var originalBodyStream = context.Response.Body;

            try
            {
                // Replace the response stream with a memory stream
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    // Continue processing the request
                    await _next(context);
                    string responseContent = "abc";
                    // Intercept and modify the response
                    if (context.Response.StatusCode == 200)
                    {
                        // Replace this with your custom logic to modify the response
                        responseContent = await FormatResponseCode200(context.Response);

                        // Write the modified response content to the memory stream

                    }
                    if (context.Response.StatusCode == 401)
                    {
                        // Handle exceptions here
                        context.Response.StatusCode = 200; // Internal Server Error
                        var errorMessage = "you have no permissions to access this website.";

                        // Create an ApiResponseModel with error details
                        var apiResponse = new ApiResponseModel<object>
                        {
                            Success = false,
                            ErrorMessage = errorMessage,
                            StatusCode = 401
                        };

                        // Serialize the ApiResponseModel to JSON
                        responseContent = JsonConvert.SerializeObject(apiResponse);

                        // Write the formatted response to the original response stream
                        ////var bytes = Encoding.UTF8.GetBytes(formattedResponse);
                        ////await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);
                        ////responseBody.Seek(0, SeekOrigin.Begin);
                        ////await responseBody.CopyToAsync(originalBodyStream);
                    }

                    // Write the formatted response to the original response stream
                    var bytes = Encoding.UTF8.GetBytes(responseContent);
                    await responseBody.WriteAsync(bytes, 0, bytes.Length);

                    //Copy the memory stream back to the original response stream
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                using (var responseBody = new MemoryStream())
                {
                    // Handle exceptions here
                    context.Response.StatusCode = 200; // Internal Server Error
                    var errorMessage = ex.Message;

                    // Create an ApiResponseModel with error details
                    var apiResponse = new ApiResponseModel<object>
                    {
                        Success = false,
                        ErrorMessage = errorMessage,
                        StatusCode = 500
                    };

                    // Serialize the ApiResponseModel to JSON
                    var formattedResponse = JsonConvert.SerializeObject(apiResponse);

                    // Write the formatted response to the original response stream
                    var bytes = Encoding.UTF8.GetBytes(formattedResponse);
                    await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }

            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task<string> FormatResponseCode200(HttpResponse response)
        {
            // Read the original response content
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseContent = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            // Parse the original response content or create your custom response format
            var responseData = JsonConvert.DeserializeObject(responseContent);

            // Create your ApiResponse<T> object
            var apiResponse = new ApiResponseModel<object>
            {
                Success = true,
                Data = responseData,
                StatusCode = response.StatusCode
            };

            // Serialize the ApiResponse<T> object back to JSON
            var formattedResponse = JsonConvert.SerializeObject(apiResponse);

            return formattedResponse;
        }
    }

}
