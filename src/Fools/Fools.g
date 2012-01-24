namespace Fools:
	import System;
	import System.Linq;
	import Fools.Ast;
	import MetaSharp.Transformation;

	grammar FoolsStructure < Parser:

		override WhitespaceInterleave = !default;

		Main = Block;

		Block = st:Statement+
			-> {
				var result = new Block();
				result.statements = st.Cast<IStatement>();
				return result;
				}

		Statement = s:(PrintStatement
			| AssignmentStatement
			| UnrecognizedStatement
			)
			error unless StatementEnd
			-> s;

		PrintStatement = "print(" var:(!")" any)* ")"
			-> {
				var result = new PrintStatement();
				result.variable = var as string;
				return result;
				}

		AssignmentStatement = var:(!'=' !' ' !'\t' any)+
			OptionalWhitespace '=' OptionalWhitespace
			val:Expression
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

		UnrecognizedStatement =  s:(!'\n' !'\r' any)+
			-> {
				var result = new UnrecognizedStatement();
				result.text = (s as Nodes).Select(n=>n.value);
				return result;
				}

		StatementEnd = WhitespaceOrComment "\r\n" | '\r' | '\n' | Eof;

		WhitespaceOrComment = OptionalWhitespace ('#' (!'\r' !'\n' any)*)?;

		OptionalWhitespace = (' ' | '\t')*;

		Eof = !any;

	end
end