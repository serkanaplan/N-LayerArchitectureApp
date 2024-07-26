using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs;

public class CustomResponseDto<T>
{
    public T Data { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }//yapılan istsonucu status kod zatan döndüğü için ekstra olarak dönsün istemiyoruz
    public List<String> Errors { get; set; }
    public static CustomResponseDto<T> Success(int statusCode, T data) => new() { Data = data, StatusCode = statusCode };
    public static CustomResponseDto<T> Success(int statusCode) => new() { StatusCode = statusCode };
    public static CustomResponseDto<T> Fail(int statusCode, List<string> errors) => new() { StatusCode = statusCode, Errors = errors };
    public static CustomResponseDto<T> Fail(int statusCode, string error) => new() { StatusCode = statusCode, Errors = [error] };
}
