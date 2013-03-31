namespace Fools.cs.AST
{
	public abstract class Node
	{
		public abstract void transform_with(DeclarationTransformer transformer);
	}
}