// Fool.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading.Tasks;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class Fool<TLab> where TLab : class
	{
		[NotNull] private Task _previous_operation;

		[NotNull] private readonly TLab _lab;

		public Fool([NotNull] Task previous_operation, [NotNull] TLab lab)
		{
			_previous_operation = previous_operation;
			_lab = lab;
		}

		public void run_action([NotNull] Action<TLab, object> action, [NotNull] MailMessage message, [NotNull] Action done)
		{
			_previous_operation = _previous_operation.ContinueWith(r => {
				try
				{
					action(_lab, message);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					// TODO: What about exceptions thrown by action? When should they be observed?
				}
				done();
			});
		}
	}
}
