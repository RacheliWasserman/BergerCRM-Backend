using System;

namespace CRM.BLL.Common
{
    public enum ServiceResultStatus
    {
        Success,
        ValidationError,
        NotFound,
        Conflict
    }

    public class ServiceResult<T>
    {
        public ServiceResultStatus Status { get; private set; }

        public T? Data { get; private set; }

        public string? ErrorMessage { get; private set; }

        private ServiceResult(ServiceResultStatus status, T? data = default, string? errorMessage = null)
        {
            Status = status;
            Data = data;
            ErrorMessage = errorMessage;
        }

        public static ServiceResult<T> Success(T data) => new ServiceResult<T>(ServiceResultStatus.Success, data);

        public static ServiceResult<T> ValidationError(string message) => new ServiceResult<T>(ServiceResultStatus.ValidationError, default, message);

        public static ServiceResult<T> NotFound(string? message = null) => new ServiceResult<T>(ServiceResultStatus.NotFound, default, message);

        public static ServiceResult<T> Conflict(string message) => new ServiceResult<T>(ServiceResultStatus.Conflict, default, message);
    }
}
