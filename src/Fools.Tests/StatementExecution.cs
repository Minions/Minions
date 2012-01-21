//using Fools.Vm;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture]
	public class StatementExecution
	{
		[Test]
		public void Assignment()
		{
			//var testSubject = new FoolishMachine();
			// Should be a lot more low-level than this.
			// I should really just make a sequence of instructions, all of which are low-level (eg, [push local_universe, push 1, push "hi", set_var])
			// VM model consists of code chunks, triggers for those code chunks, one stack per code chunk, and universes. Universes can come
			// from locals, or from other sources. Eg, might create a "Session" universe for a particular HTTP request/response and its associated
			// TCP stuff. Thus, user-level code can create shared universes, but there are compiler-generated ones for things like locals.
			//
			// A universe can be constructed with pre-allocated variables (compiler knows that it'll need 2 ints and a string), and can also have
			// dynamic variables.
			//
			// Stack follows same consistency rules as the .Net VM one. Want similarly computable trustworthiness.
			//
			// Also, our VM should be a thin decorator on top of the .Net VM. That gives us easy port to .Net & Mono, and should be re-implementable
			// on LLVM for other platforms (if necessary, eventually).

		/*
		 var minion = new Fool();
			var code = new Scope(max_stack_depth:2);
			code.commands.add.load_universe(Universe.FOOL_DEFAULT);
			var variable_value = new FoolString("hello");
			code.commands.add.load_constant(variable_value);
			code.commands.add.create_variable();
			minion.execute(code);

			var current_stack = code.PeekAtStack();
			Assert.That(current_stack.depth, Is.EqualTo(1));
			Assert.That(current_stack[0].type_code, Is.EqualTo(BuiltinTypeCodes.VARIABLE_REFERENCE));
			var new_var = current_stack[0].value as VariableReference;
			Assert.That(new_var.universe, Is.EqualTo(minion.default_universe));
			var stored_variable = minion.default_universe.PeekAt(new_var.pointer);
			Assert.That(stored_variable.value, Is.SameAs(variable_value));
			*/
			// the below is wrong. Don't even look at it.
			//var universe = testSubject.create_universe();
			//var continuation = testSubject.create_continuation();
			//var foo = continuation.instructions.add.allocate_local();
			//continuation.instructions.add.load_constant(foo, "hi");
			//testSubject.execute(continuation, universe);
			//Assert.That(continuation.local_universe.TestApi.Get(foo), Is.EqualTo("hi"));
		}
	}

	public class Fool {}
}
