using Fools.cs.Api;
using Fools.cs.Interpret;

namespace Fools.cs.builtins
{
	public class TestPartialInfoMessage : MailMessage
	{
		public string message { get; private set; }

		public TestPartialInfoMessage(string message)
		{
			this.message = message;
		}

		public void send_to(MessageRecipient mail_slot)
		{
			mail_slot.accept(this);
		}
	}
}