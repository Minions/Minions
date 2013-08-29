// CompilerUserInteractionModel.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Api.CommandLineApp;
using Fools.cs.Utilities;
using PowerArgs;

namespace core_compile
{
	/// <summary>
	///    View model used between the compiler and the user. Handles both input and output. Makes no assumptions about whether the compiler is singly- or multiply-entrant.
	/// </summary>
	[UsedImplicitly]
	public class CompilerUserInteractionModel : UniversalCommands
	{
		[ArgDescription("Path to the project file you want built. Each project file results in one assembly.")]
		public string project_file_name { get; set; }
	}
}
