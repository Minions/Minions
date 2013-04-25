namespace Fools.cs.AST
{
	public class AssignmentStatement : ExecutableStatement
	{
		public string l_value { get; set; }
		public string expression { get; set; }
	}
}
