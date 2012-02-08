using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Fools.Ast;

namespace Fools.Generation
{
	public class MethodBuilder
	{
		private static readonly Type _STACK_FRAME_TYPE = typeof(Dictionary<string, object>);
		private readonly ILGenerator _body_generator;
		private readonly DynamicMethod _method;

		public MethodBuilder()
		{
			_method = new DynamicMethod("foo", _STACK_FRAME_TYPE, new[] {_STACK_FRAME_TYPE},
				typeof(MethodBuilder).Module);
			_body_generator = _method.GetILGenerator();
		}

		public void AddAssignmentStatement(AssignmentStatement assignment_statement)
		{
			var set_item_property =
				_STACK_FRAME_TYPE.GetProperty("Item",
					BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.Public).GetSetMethod();
			var value = (NumberLiteral)assignment_statement.value;

			_body_generator.Emit(OpCodes.Ldarg_0);
			_body_generator.Emit(OpCodes.Ldstr, assignment_statement.variable);
			_body_generator.Emit(OpCodes.Ldc_I4, value.value);
			_body_generator.Emit(OpCodes.Box, typeof(Int32));
			_body_generator.Emit(OpCodes.Callvirt, set_item_property);
		}

		public DynamicMethod ToCode()
		{
			_body_generator.Emit(OpCodes.Ldarg_0);
			_body_generator.Emit(OpCodes.Ret);

			return _method;
		}
	}
}
