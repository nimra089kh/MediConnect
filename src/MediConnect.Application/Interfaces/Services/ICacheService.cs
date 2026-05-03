using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Services
{
	public interface ICacheService
	{
		Task<T?> GetAsync<T>(string key);
		Task SetAsync<T>(string key, T value, TimeSpan expiration);
		Task RemoveAsync(string key);
		Task<bool> AcquireLockAsync(string key, TimeSpan expiration);
		Task ReleaselockAsync(string key);
	}
}
