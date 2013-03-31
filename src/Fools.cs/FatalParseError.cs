using System;

namespace Fools.cs
{
	public class FatalParseError : Exception
	{
		public ErrorReport error { get; private set; }

		public FatalParseError(ErrorReport error) : base(error.ToString())
		{
			this.error = error;
		}
	}
}