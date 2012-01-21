using System;

namespace Fools
{
	public static class Require
	{
		public static void that(bool condition, Func<Exception> ex)
		{
			if(!condition)
				throw ex();
		}
	}
}
