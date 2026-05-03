using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Common.Models
{
	public class Result
	{
		public bool IsSuccess { get; private set; }
		public string Error { get; private set; }

		protected Result(bool isSuccess, string? error)
		{
			IsSuccess = isSuccess;
			Error = error;

		}

		public static Result Success() =>
			new(true, null);
		public static Result Failure(string error) =>
			new(false, error);
	}
		public class Result<T> : Result
		{
			public T? Data { get; private set; }
			private Result(bool isSuccess, string? error, T? data) : base(isSuccess, error)
			{
				Data = data;
			}
			public static Result<T> Success(T data) =>
				new(true, null, data);
			public new static Result<T> Failure(string error) =>
				new(false, error, default);
		}
	
}
