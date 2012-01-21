namespace Fools:
	import Fools.Ast;
	import MetaSharp.Transformation;

	grammar FoolsStructure < Parser:

		Main = Block;

		Block = st:Statement+
			-> {
				var result = new Block();
				result.statements = st.Cast<IStatement>();
				return result;
				}

		Statement = PrintStatement
			| AssignmentStatement
			| UnrecognizedStatement;

		PrintStatement = "print" "(" var:(!")" any)* ")"
			-> {
				var result = new PrintStatement();
				result.variable = var as string;
				return result;
				}

		AssignmentStatement = var:(!'=' any)* '=' val:Expression
			-> {
				var result = new AssignmentStatement();
				result.variable = (var as string).Trim();
				result.value = val as INode;
				return result;
				}

		Expression = val:Number
			-> {
				return new NumberLiteral(val as int);
				}

		UnrecognizedStatement =  s:(!'\n' !'\r' any)*
			-> {
				var result = new UnrecognizedStatement();
				result.text = s.ToString();
				return result;
				}

		override WhitespaceInterleave = !default;

	end
end