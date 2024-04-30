using System.Diagnostics;

namespace MovieManager.Service.Common
{
	public sealed class RequestResult<T>
	{
		private readonly Stopwatch _stopWatch = new();
		private Exception _exception;

		public RequestResult()
		{
			_stopWatch.Start();
		}

		public TimeSpan TimeSpan => _stopWatch.Elapsed;
		public T Value { get; set; }
		public bool HasError { get; private set; }

		public Exception Exception
		{
			get => _exception;
			set
			{
				_exception = value;
				if (_exception != null)
					HasError = true;
			}
		}

		public void Stop()
		{
			_stopWatch.Stop();
		}
	}
}
