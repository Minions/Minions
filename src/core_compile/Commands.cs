using System;
using Fools.cs.Utilities;

namespace core_compile
{
	public class Commands<T> where T : class
	{
		private Program.ErrorLevel _error_level = Program.ErrorLevel.Ok;
		[CanBeNull] private Exception _exception;

		private Commands(T args)
		{
			this.args = args;
		}

		[CanBeNull]
		public T args { get; private set; }

		public Program.ErrorLevel error_level { get { return _error_level; } }

		[CanBeNull]
		public Exception exception { get { return _exception; } }

		[NotNull]
		public static Commands<T> quit(Program.ErrorLevel error_level, [CanBeNull] Exception exception)
		{
			var commands = new Commands<T>(null) {_error_level = error_level, _exception = exception};
			return commands;
		}

		[NotNull]
		public static Commands<T> run([NotNull] T args)
		{
			return new Commands<T>(args);
		}
	}
}