// TransformAstIntoExecutionModel.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using Fools.cs.AST;
using Fools.cs.Api;
using Fools.cs.TransformAst;
using NUnit.Framework;

namespace Fools.cs.Tests.Compilation
{
	[TestFixture]
	public class TransformAstIntoExecutionModel
	{
		[Test]
		public void compiler_should_locate_all_passes()
		{
			NanoPass<ProgramFragment>.all.Should()
				.Contain(pass => pass is CreateTestMissions);
		}

		[Test]
		public void compiler_should_run_passes_in_the_correct_order()
		{
			var data = new FeatureSpecification("This minion", Enumerable.Empty<Node>());
			var test_subject =
				new FoolsCompiler(
					local_passes(new AppendToName("is a dog", conditions(), conditions("is a dog")),
						new AppendToName("that runs fast", conditions("is a dog"), conditions())),
					global_passes());
			test_subject.compile(ProgramFragment.with_declarations(data));
			data.feature.Should()
				.Be("This minion");
			test_subject.result.declarations.Single()
				.As<FeatureSpecification>()
				.feature.Should()
				.Be("This minion is a dog that runs fast");
		}

		private ReadOnlyCollection<NanoPass<ProgramFragment>> local_passes(params NanoPass<ProgramFragment>[] passes)
		{
			return passes.ToList()
				.AsReadOnly();
		}

		private ReadOnlyCollection<NanoPass<Program>> global_passes(params NanoPass<Program>[] passes)
		{
			return passes.ToList()
				.AsReadOnly();
		}

		private ReadOnlyCollection<AstStateCondition> conditions(params string[] condition_names)
		{
			return condition_names.Select(AstStateCondition.named)
				.ToList()
				.AsReadOnly();
		}
	}

	public class AppendToName : NanoPass<ProgramFragment>
	{
		private readonly string _name_to_append;
		private readonly ReadOnlyCollection<AstStateCondition> _causes;

		public AppendToName(string name_to_append,
			IEnumerable<AstStateCondition> requires,
			ReadOnlyCollection<AstStateCondition> causes) : base(requires)
		{
			_name_to_append = name_to_append;
			_causes = causes;
		}

		public override ProgramFragment run(ProgramFragment data, Action<AstStateCondition> add_condition)
		{
			var new_declarations = data.declarations.Select(_transform_declaration).ToList();
			foreach (var resulting_condition in _causes)
			{
				add_condition(resulting_condition);
			}
			return ProgramFragment.with_declarations(new_declarations);
		}

		private Declaration _transform_declaration(Declaration input)
		{
			var specification = input as FeatureSpecification;
			if (specification != null)
			{
				return new FeatureSpecification(string.Format("{0} {1}", specification.feature, _name_to_append), specification.body);
			}
			return input;
		}
	}
}
