// Fools.peg.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Fools.cs.AST;
using Pegasus.Common;

namespace
#line 1 "Fools.peg"
    Fools.cs.ParseToAst
#line default
{
    [GeneratedCode("Pegasus", "2.3.2.0")]
    public class
#line 2 "Fools.peg"
        FoolsPegParser
#line default
    {
#line 6 "Fools.peg"

        public Report report { get; set; }
#line default
        public IList<Declaration> Parse(string subject, string fileName = null)
        {
            var cursor = new Cursor(subject, 0, fileName);
            var result = program(ref cursor);
            if (result == null) throw ExceptionHelper(cursor, state => "Failed to parse 'program'.");
            return result.Value;
        }

        private IParseResult<
#line 10 "Fools.peg"
            IList<Declaration>
#line default
            > program(ref Cursor cursor)
        {
            IParseResult<IList<Declaration>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = INIT_INDENTATION(ref cursor);
            if (r1 != null)
            {
                IParseResult<IList<Declaration>> r2 = null;
                var sStart = cursor;
                r2 = declarations(ref cursor);
                var sEnd = cursor;
                var s = ValueOrDefault(r2);
                if (r2 != null)
                {
                    IParseResult<IList<Node>> r3 = null;
                    var startCursor1 = cursor;
                    var l0 = new List<Node>();
                    while (true)
                    {
                        IParseResult<Node> r4 = null;
                        r4 = empty_line(ref cursor);
                        if (r4 != null) l0.Add(r4.Value);
                        else break;
                    }
                    if (l0.Count >= 0) r3 = new ParseResult<IList<Node>>(startCursor1, cursor, l0.AsReadOnly());
                    else cursor = startCursor1;
                    if (r3 != null)
                    {
                        IParseResult<string> r5 = null;
                        r5 = eof(ref cursor);
                        if (r5 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 11 "Fools.peg"
                                    s
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 13 "Fools.peg"
            IList<Declaration>
#line default
            > declarations(ref Cursor cursor)
        {
            IParseResult<IList<Declaration>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<IList<Declaration>> r1 = null;
            var dStart = cursor;
            var startCursor1 = cursor;
            var l0 = new List<Declaration>();
            while (true)
            {
                IParseResult<Declaration> r2 = null;
                r2 = decl_block(ref cursor);
                if (r2 != null) l0.Add(r2.Value);
                else break;
            }
            if (l0.Count >= 0) r1 = new ParseResult<IList<Declaration>>(startCursor1, cursor, l0.AsReadOnly());
            else cursor = startCursor1;
            var dEnd = cursor;
            var d = ValueOrDefault(r1);
            if (r1 != null)
            {
                IParseResult<string> r3 = null;
                var startCursor2 = cursor;
                IParseResult<string> r4 = null;
                var startCursor3 = cursor;
                IParseResult<string> r5 = null;
                if (new Func<Cursor, bool>(state =>
#line 14 "Fools.peg"
                    d.Count == 0
#line default
                    )(cursor)) r5 = new ParseResult<string>(cursor, cursor, string.Empty);
                if (r5 != null)
                {
                    IParseResult<string> r6 = null;
                    if (new Func<Cursor, bool>(state =>
#line 14 "Fools.peg"
                        report.no_declarations_in_file(state)
#line default
                        )(cursor)) r6 = new ParseResult<string>(cursor, cursor, string.Empty);
                    if (r6 != null)
                    {
                        var len = cursor.Location - startCursor3.Location;
                        r4 = new ParseResult<string>(startCursor3,
                            cursor,
                            cursor.Subject.Substring(startCursor3.Location, len));
                    }
                    else cursor = startCursor3;
                }
                else cursor = startCursor3;
                cursor = startCursor2;
                if (r4 == null) r3 = new ParseResult<string>(cursor, cursor, string.Empty);
                if (r3 != null)
                {
                    r0 = ReturnHelper(startCursor0,
                        cursor,
                        state =>
#line 14 "Fools.peg"
                            d
#line default
                        );
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 16 "Fools.peg"
            Declaration
#line default
            > decl_block(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            r2 = eof(ref cursor);
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<IList<Node>> r3 = null;
                var startCursor2 = cursor;
                var l0 = new List<Node>();
                while (true)
                {
                    IParseResult<Node> r4 = null;
                    r4 = empty_line(ref cursor);
                    if (r4 != null) l0.Add(r4.Value);
                    else break;
                }
                if (l0.Count >= 0) r3 = new ParseResult<IList<Node>>(startCursor2, cursor, l0.AsReadOnly());
                else cursor = startCursor2;
                if (r3 != null)
                {
                    IParseResult<Declaration> r5 = null;
                    var sStart = cursor;
                    r5 = declaration(ref cursor);
                    var sEnd = cursor;
                    var s = ValueOrDefault(r5);
                    if (r5 != null)
                    {
                        r0 = ReturnHelper(startCursor0,
                            cursor,
                            state =>
#line 17 "Fools.peg"
                                s
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 19 "Fools.peg"
            Node
#line default
            > line(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            r2 = eof(ref cursor);
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<IList<Node>> r3 = null;
                var startCursor2 = cursor;
                var l0 = new List<Node>();
                while (true)
                {
                    IParseResult<Node> r4 = null;
                    r4 = empty_line(ref cursor);
                    if (r4 != null) l0.Add(r4.Value);
                    else break;
                }
                if (l0.Count >= 0) r3 = new ParseResult<IList<Node>>(startCursor2, cursor, l0.AsReadOnly());
                else cursor = startCursor2;
                if (r3 != null)
                {
                    IParseResult<string> r5 = null;
                    r5 = INDENTATION(ref cursor);
                    if (r5 != null)
                    {
                        IParseResult<Node> r6 = null;
                        var sStart = cursor;
                        r6 = statement(ref cursor);
                        var sEnd = cursor;
                        var s = ValueOrDefault(r6);
                        if (r6 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 20 "Fools.peg"
                                    s
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 22 "Fools.peg"
            Node
#line default
            > feature_spec_line(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            r2 = eof(ref cursor);
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<IList<Node>> r3 = null;
                var startCursor2 = cursor;
                var l0 = new List<Node>();
                while (true)
                {
                    IParseResult<Node> r4 = null;
                    r4 = empty_line(ref cursor);
                    if (r4 != null) l0.Add(r4.Value);
                    else break;
                }
                if (l0.Count >= 0) r3 = new ParseResult<IList<Node>>(startCursor2, cursor, l0.AsReadOnly());
                else cursor = startCursor2;
                if (r3 != null)
                {
                    IParseResult<string> r5 = null;
                    r5 = INDENTATION(ref cursor);
                    if (r5 != null)
                    {
                        IParseResult<Declaration> r6 = null;
                        var sStart = cursor;
                        r6 = feature_spec_declaration(ref cursor);
                        var sEnd = cursor;
                        var s = ValueOrDefault(r6);
                        if (r6 != null)
                        {
                            r0 = ReturnHelper<Node>(startCursor0,
                                cursor,
                                state =>
#line 23 "Fools.peg"
                                    s
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 25 "Fools.peg"
            Declaration
#line default
            > declaration(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            if (r0 == null) r0 = bad_block(ref cursor);
            if (r0 == null) r0 = function(ref cursor);
            if (r0 == null) r0 = feature_spec(ref cursor);
            if (r0 == null) r0 = unrecognized_declaration(ref cursor);
            return r0;
        }

        private IParseResult<
#line 31 "Fools.peg"
            Declaration
#line default
            > feature_spec_declaration(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            if (r0 == null) r0 = bad_block(ref cursor);
            if (r0 == null) r0 = feature_spec(ref cursor);
            if (r0 == null) r0 = feature_requirement(ref cursor);
            if (r0 == null) r0 = valid_mission_declaration(ref cursor);
            if (r0 == null) r0 = unrecognized_declaration(ref cursor);
            return r0;
        }

        private IParseResult<
#line 38 "Fools.peg"
            Declaration
#line default
            > mission_declaration(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            if (r0 == null) r0 = bad_block(ref cursor);
            if (r0 == null) r0 = valid_mission_declaration(ref cursor);
            if (r0 == null) r0 = unrecognized_declaration(ref cursor);
            return r0;
        }

        private IParseResult<
#line 43 "Fools.peg"
            Declaration
#line default
            > valid_mission_declaration(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            if (r0 == null) r0 = mission_preparation(ref cursor);
            if (r0 == null) r0 = mission_cleanup(ref cursor);
            return r0;
        }

        private IParseResult<
#line 47 "Fools.peg"
            Node
#line default
            > statement(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            if (r0 == null) r0 = bad_block(ref cursor);
            if (r0 == null) r0 = function(ref cursor);
            if (r0 == null) r0 = known_executable_blocks(ref cursor);
            if (r0 == null)
            {
                var startCursor0 = cursor;
                IParseResult<Node> r1 = null;
                var sStart = cursor;
                r1 = simpleStatement(ref cursor);
                var sEnd = cursor;
                var s = ValueOrDefault(r1);
                if (r1 != null)
                {
                    IParseResult<string> r2 = null;
                    r2 = eol(ref cursor);
                    if (r2 != null)
                    {
                        r0 = ReturnHelper(startCursor0,
                            cursor,
                            state =>
#line 51 "Fools.peg"
                                s
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            if (r0 == null) r0 = unrecognized_statement(ref cursor);
            return r0;
        }

        private IParseResult<
#line 54 "Fools.peg"
            Declaration
#line default
            > function(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "def");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    var nStart = cursor;
                    r3 = name(ref cursor);
                    var nEnd = cursor;
                    var n = ValueOrDefault(r3);
                    if (r3 != null)
                    {
                        IParseResult<string> r4 = null;
                        r4 = ParseLiteral(ref cursor, "()");
                        if (r4 != null)
                        {
                            IParseResult<IList<string>> r5 = null;
                            r5 = __(ref cursor);
                            if (r5 != null)
                            {
                                IParseResult<IList<Node>> r6 = null;
                                var sStart = cursor;
                                r6 = block_body(ref cursor);
                                var sEnd = cursor;
                                var s = ValueOrDefault(r6);
                                if (r6 != null)
                                {
                                    r0 = ReturnHelper<Declaration>(startCursor0,
                                        cursor,
                                        state =>
#line 56 "Fools.peg"
                                            new FunctionDefinition {name = n, body = s}
#line default
                                        );
                                }
                                else cursor = startCursor0;
                            }
                            else cursor = startCursor0;
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 58 "Fools.peg"
            Declaration
#line default
            > feature_spec(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "specify");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    r3 = ParseLiteral(ref cursor, "feature");
                    if (r3 != null)
                    {
                        IParseResult<IList<string>> r4 = null;
                        r4 = __(ref cursor);
                        if (r4 != null)
                        {
                            IParseResult<string> r5 = null;
                            r5 = ParseLiteral(ref cursor, "-");
                            if (r5 != null)
                            {
                                IParseResult<IList<string>> r6 = null;
                                r6 = __(ref cursor);
                                if (r6 != null)
                                {
                                    IParseResult<string> r7 = null;
                                    var nStart = cursor;
                                    r7 = block_header(ref cursor);
                                    var nEnd = cursor;
                                    var n = ValueOrDefault(r7);
                                    if (r7 != null)
                                    {
                                        IParseResult<IList<Node>> r8 = null;
                                        var sStart = cursor;
                                        r8 = spec_body(ref cursor);
                                        var sEnd = cursor;
                                        var s = ValueOrDefault(r8);
                                        if (r8 != null)
                                        {
                                            r0 = ReturnHelper<Declaration>(startCursor0,
                                                cursor,
                                                state =>
#line 60 "Fools.peg"
                                                    new FeatureSpecification {feature = n, body = s}
#line default
                                                );
                                        }
                                        else cursor = startCursor0;
                                    }
                                    else cursor = startCursor0;
                                }
                                else cursor = startCursor0;
                            }
                            else cursor = startCursor0;
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 62 "Fools.peg"
            Declaration
#line default
            > feature_requirement(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "requires");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    var nStart = cursor;
                    r3 = block_header(ref cursor);
                    var nEnd = cursor;
                    var n = ValueOrDefault(r3);
                    if (r3 != null)
                    {
                        IParseResult<IList<Node>> r4 = null;
                        var sStart = cursor;
                        r4 = block_body(ref cursor);
                        var sEnd = cursor;
                        var s = ValueOrDefault(r4);
                        if (r4 != null)
                        {
                            r0 = ReturnHelper<Declaration>(startCursor0,
                                cursor,
                                state =>
#line 64 "Fools.peg"
                                    new FeatureRequirement {requirement = n, body = s}
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 66 "Fools.peg"
            MissionActivity
#line default
            > mission_preparation(ref Cursor cursor)
        {
            IParseResult<MissionActivity> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "mission");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    r3 = ParseLiteral(ref cursor, "preparation");
                    if (r3 != null)
                    {
                        IParseResult<IList<Node>> r4 = null;
                        var sStart = cursor;
                        r4 = mission_activity_body(ref cursor);
                        var sEnd = cursor;
                        var s = ValueOrDefault(r4);
                        if (r4 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 68 "Fools.peg"
                                    new MissionActivity {
                                        name = "clean up",
                                        body = s,
                                        special_purpose = Mission.Purpose.Preparation
                                    }
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 70 "Fools.peg"
            MissionActivity
#line default
            > mission_cleanup(ref Cursor cursor)
        {
            IParseResult<MissionActivity> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "mission");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    r3 = ParseLiteral(ref cursor, "clean");
                    if (r3 != null)
                    {
                        IParseResult<IList<string>> r4 = null;
                        r4 = __(ref cursor);
                        if (r4 != null)
                        {
                            IParseResult<string> r5 = null;
                            r5 = ParseLiteral(ref cursor, "up");
                            if (r5 != null)
                            {
                                IParseResult<IList<Node>> r6 = null;
                                var sStart = cursor;
                                r6 = mission_activity_body(ref cursor);
                                var sEnd = cursor;
                                var s = ValueOrDefault(r6);
                                if (r6 != null)
                                {
                                    r0 = ReturnHelper(startCursor0,
                                        cursor,
                                        state =>
#line 72 "Fools.peg"
                                            new MissionActivity {
                                                name = "clean up",
                                                body = s,
                                                special_purpose = Mission.Purpose.CleanUp
                                            }
#line default
                                        );
                                }
                                else cursor = startCursor0;
                            }
                            else cursor = startCursor0;
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 74 "Fools.peg"
            Node
#line default
            > known_executable_blocks(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "if");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    var nStart = cursor;
                    r3 = name(ref cursor);
                    var nEnd = cursor;
                    var n = ValueOrDefault(r3);
                    if (r3 != null)
                    {
                        IParseResult<IList<Node>> r4 = null;
                        var sStart = cursor;
                        r4 = block_body(ref cursor);
                        var sEnd = cursor;
                        var s = ValueOrDefault(r4);
                        if (r4 != null)
                        {
                            r0 = ReturnHelper<Node>(startCursor0,
                                cursor,
                                state =>
#line 76 "Fools.peg"
                                    new ConditionalStatement {condition = n, body_when_true = s}
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 78 "Fools.peg"
            Node
#line default
            > simpleStatement(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            var startCursor2 = cursor;
            IParseResult<string> r3 = null;
            r3 = block_header(ref cursor);
            if (r3 != null)
            {
                IParseResult<string> r4 = null;
                r4 = ParseLiteral(ref cursor, ":");
                if (r4 != null)
                {
                    var len = cursor.Location - startCursor2.Location;
                    r2 = new ParseResult<string>(startCursor2,
                        cursor,
                        cursor.Subject.Substring(startCursor2.Location, len));
                }
                else cursor = startCursor2;
            }
            else cursor = startCursor2;
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<string> r5 = null;
                var aStart = cursor;
                r5 = name(ref cursor);
                var aEnd = cursor;
                var a = ValueOrDefault(r5);
                if (r5 != null)
                {
                    IParseResult<string> r6 = null;
                    r6 = ParseLiteral(ref cursor, "=");
                    if (r6 != null)
                    {
                        IParseResult<IList<string>> r7 = null;
                        r7 = __(ref cursor);
                        if (r7 != null)
                        {
                            IParseResult<string> r8 = null;
                            var bStart = cursor;
                            r8 = name(ref cursor);
                            var bEnd = cursor;
                            var b = ValueOrDefault(r8);
                            if (r8 != null)
                            {
                                r0 = ReturnHelper<Node>(startCursor0,
                                    cursor,
                                    state =>
#line 80 "Fools.peg"
                                        new AssignmentStatement {l_value = a, expression = b}
#line default
                                    );
                            }
                            else cursor = startCursor0;
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 82 "Fools.peg"
            Declaration
#line default
            > unrecognized_declaration(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            var startCursor2 = cursor;
            IParseResult<IList<string>> r3 = null;
            var startCursor3 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r4 = null;
                r4 = ParseClass(ref cursor, "  \t\t");
                if (r4 != null) l0.Add(r4.Value);
                else break;
            }
            if (l0.Count >= 0) r3 = new ParseResult<IList<string>>(startCursor3, cursor, l0.AsReadOnly());
            else cursor = startCursor3;
            if (r3 != null)
            {
                IParseResult<string> r5 = null;
                r5 = eol(ref cursor);
                if (r5 != null)
                {
                    var len = cursor.Location - startCursor2.Location;
                    r2 = new ParseResult<string>(startCursor2,
                        cursor,
                        cursor.Subject.Substring(startCursor2.Location, len));
                }
                else cursor = startCursor2;
            }
            else cursor = startCursor2;
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<string> r6 = null;
                var sStart = cursor;
                r6 = rest_of_current_line(ref cursor);
                var sEnd = cursor;
                var s = ValueOrDefault(r6);
                if (r6 != null)
                {
                    IParseResult<string> r7 = null;
                    if (new Func<Cursor, bool>(state =>
#line 83 "Fools.peg"
                        report.unrecognized_declaration(state, s)
#line default
                        )(cursor)) r7 = new ParseResult<string>(cursor, cursor, string.Empty);
                    if (r7 != null)
                    {
                        r0 = ReturnHelper<Declaration>(startCursor0,
                            cursor,
                            state =>
#line 83 "Fools.peg"
                                null
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 85 "Fools.peg"
            Node
#line default
            > unrecognized_statement(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            var startCursor2 = cursor;
            IParseResult<IList<string>> r3 = null;
            var startCursor3 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r4 = null;
                r4 = ParseClass(ref cursor, "  \t\t");
                if (r4 != null) l0.Add(r4.Value);
                else break;
            }
            if (l0.Count >= 0) r3 = new ParseResult<IList<string>>(startCursor3, cursor, l0.AsReadOnly());
            else cursor = startCursor3;
            if (r3 != null)
            {
                IParseResult<string> r5 = null;
                r5 = eol(ref cursor);
                if (r5 != null)
                {
                    var len = cursor.Location - startCursor2.Location;
                    r2 = new ParseResult<string>(startCursor2,
                        cursor,
                        cursor.Subject.Substring(startCursor2.Location, len));
                }
                else cursor = startCursor2;
            }
            else cursor = startCursor2;
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<string> r6 = null;
                var sStart = cursor;
                r6 = rest_of_current_line(ref cursor);
                var sEnd = cursor;
                var s = ValueOrDefault(r6);
                if (r6 != null)
                {
                    IParseResult<string> r7 = null;
                    if (new Func<Cursor, bool>(state =>
#line 86 "Fools.peg"
                        report.unrecognized_statement(state, s)
#line default
                        )(cursor)) r7 = new ParseResult<string>(cursor, cursor, string.Empty);
                    if (r7 != null)
                    {
                        r0 = ReturnHelper<Node>(startCursor0,
                            cursor,
                            state =>
#line 86 "Fools.peg"
                                null
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 88 "Fools.peg"
            Declaration
#line default
            > bad_block(ref Cursor cursor)
        {
            IParseResult<Declaration> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            var startCursor2 = cursor;
            IParseResult<string> r3 = null;
            r3 = block_header(ref cursor);
            if (r3 != null)
            {
                IParseResult<string> r4 = null;
                r4 = block_delim(ref cursor);
                if (r4 != null)
                {
                    IParseResult<string> r5 = null;
                    var startCursor3 = cursor;
                    IParseResult<IList<string>> r6 = null;
                    r6 = possible_block_body(ref cursor);
                    cursor = startCursor3;
                    if (r6 == null) r5 = new ParseResult<string>(cursor, cursor, string.Empty);
                    if (r5 != null)
                    {
                        var len = cursor.Location - startCursor2.Location;
                        r2 = new ParseResult<string>(startCursor2,
                            cursor,
                            cursor.Subject.Substring(startCursor2.Location, len));
                    }
                    else cursor = startCursor2;
                }
                else cursor = startCursor2;
            }
            else cursor = startCursor2;
            cursor = startCursor1;
            if (r2 != null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<string> r7 = null;
                var codeStart = cursor;
                var startCursor4 = cursor;
                IParseResult<string> r8 = null;
                r8 = rest_of_current_line(ref cursor);
                if (r8 != null)
                {
                    IParseResult<string> r9 = null;
                    r9 = eol(ref cursor);
                    if (r9 != null)
                    {
                        IParseResult<string> r10 = null;
                        r10 = rest_of_current_line(ref cursor);
                        if (r10 != null)
                        {
                            var len = cursor.Location - startCursor4.Location;
                            r7 = new ParseResult<string>(startCursor4,
                                cursor,
                                cursor.Subject.Substring(startCursor4.Location, len));
                        }
                        else cursor = startCursor4;
                    }
                    else cursor = startCursor4;
                }
                else cursor = startCursor4;
                var codeEnd = cursor;
                var code = ValueOrDefault(r7);
                if (r7 != null)
                {
                    IParseResult<string> r11 = null;
                    if (new Func<Cursor, bool>(state =>
#line 89 "Fools.peg"
                        report.implicitly_empty_block(state, code)
#line default
                        )(cursor)) r11 = new ParseResult<string>(cursor, cursor, string.Empty);
                    if (r11 != null)
                    {
                        r0 = ReturnHelper<Declaration>(startCursor0,
                            cursor,
                            state =>
#line 89 "Fools.peg"
                                null
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 91 "Fools.peg"
            Node
#line default
            > empty_line(ref Cursor cursor)
        {
            IParseResult<Node> r0 = null;
            if (r0 == null)
            {
                var startCursor0 = cursor;
                IParseResult<IList<string>> r1 = null;
                var startCursor1 = cursor;
                var l0 = new List<string>();
                while (true)
                {
                    IParseResult<string> r2 = null;
                    r2 = ParseClass(ref cursor, "  \t\t");
                    if (r2 != null) l0.Add(r2.Value);
                    else break;
                }
                if (l0.Count >= 0) r1 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
                else cursor = startCursor1;
                if (r1 != null)
                {
                    IParseResult<string> r3 = null;
                    r3 = eol_not_eof(ref cursor);
                    if (r3 != null)
                    {
                        r0 = ReturnHelper<Node>(startCursor0,
                            cursor,
                            state =>
#line 92 "Fools.peg"
                                null
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            if (r0 == null)
            {
                var startCursor2 = cursor;
                IParseResult<IList<string>> r4 = null;
                var startCursor3 = cursor;
                var l1 = new List<string>();
                while (true)
                {
                    IParseResult<string> r5 = null;
                    r5 = ParseClass(ref cursor, "  \t\t");
                    if (r5 != null) l1.Add(r5.Value);
                    else break;
                }
                if (l1.Count >= 1) r4 = new ParseResult<IList<string>>(startCursor3, cursor, l1.AsReadOnly());
                else cursor = startCursor3;
                if (r4 != null)
                {
                    IParseResult<string> r6 = null;
                    r6 = eof(ref cursor);
                    if (r6 != null)
                    {
                        r0 = ReturnHelper<Node>(startCursor2,
                            cursor,
                            state =>
#line 93 "Fools.peg"
                                null
#line default
                            );
                    }
                    else cursor = startCursor2;
                }
                else cursor = startCursor2;
            }
            return r0;
        }

        private IParseResult<
#line 95 "Fools.peg"
            string
#line default
            > block_header(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<IList<string>> r1 = null;
            var lStart = cursor;
            var startCursor1 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r2 = null;
                r2 = ParseClass(ref cursor, "::\r\r\n\n", negated: true);
                if (r2 != null) l0.Add(r2.Value);
                else break;
            }
            if (l0.Count >= 0) r1 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
            else cursor = startCursor1;
            var lEnd = cursor;
            var l = ValueOrDefault(r1);
            if (r1 != null)
            {
                r0 = ReturnHelper(startCursor0,
                    cursor,
                    state =>
#line 96 "Fools.peg"
                        l.Flatten()
#line default
                    );
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> block_delim(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, ":");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                r2 = __(ref cursor);
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    r3 = eol(ref cursor);
                    if (r3 != null)
                    {
                        var len = cursor.Location - startCursor0.Location;
                        r0 = new ParseResult<string>(startCursor0,
                            cursor,
                            cursor.Subject.Substring(startCursor0.Location, len));
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 101 "Fools.peg"
            IList<Node>
#line default
            > block_body(ref Cursor cursor)
        {
            IParseResult<IList<Node>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = block_delim(ref cursor);
            if (r1 != null)
            {
                IParseResult<string> r2 = null;
                r2 = INDENT(ref cursor);
                if (r2 != null)
                {
                    IParseResult<
#line 102 "Fools.peg"
                        IList<Node>
#line default
                        > r3 = null;
                    var sStart = cursor;
                    if (r3 == null) r3 = pass_block(ref cursor);
                    if (r3 == null)
                    {
                        var startCursor1 = cursor;
                        var l0 = new List<Node>();
                        while (true)
                        {
                            IParseResult<Node> r4 = null;
                            r4 = line(ref cursor);
                            if (r4 != null) l0.Add(r4.Value);
                            else break;
                        }
                        if (l0.Count >= 1) r3 = new ParseResult<IList<Node>>(startCursor1, cursor, l0.AsReadOnly());
                        else cursor = startCursor1;
                    }
                    var sEnd = cursor;
                    var s = ValueOrDefault(r3);
                    if (r3 != null)
                    {
                        IParseResult<string> r5 = null;
                        r5 = UNINDENT(ref cursor);
                        if (r5 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 105 "Fools.peg"
                                    s
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 107 "Fools.peg"
            IList<Node>
#line default
            > spec_body(ref Cursor cursor)
        {
            IParseResult<IList<Node>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = block_delim(ref cursor);
            if (r1 != null)
            {
                IParseResult<string> r2 = null;
                r2 = INDENT(ref cursor);
                if (r2 != null)
                {
                    IParseResult<
#line 108 "Fools.peg"
                        IList<Node>
#line default
                        > r3 = null;
                    var sStart = cursor;
                    if (r3 == null) r3 = pass_block(ref cursor);
                    if (r3 == null)
                    {
                        var startCursor1 = cursor;
                        var l0 = new List<Node>();
                        while (true)
                        {
                            IParseResult<Node> r4 = null;
                            r4 = feature_spec_line(ref cursor);
                            if (r4 != null) l0.Add(r4.Value);
                            else break;
                        }
                        if (l0.Count >= 1) r3 = new ParseResult<IList<Node>>(startCursor1, cursor, l0.AsReadOnly());
                        else cursor = startCursor1;
                    }
                    var sEnd = cursor;
                    var s = ValueOrDefault(r3);
                    if (r3 != null)
                    {
                        IParseResult<string> r5 = null;
                        r5 = UNINDENT(ref cursor);
                        if (r5 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 111 "Fools.peg"
                                    s
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 113 "Fools.peg"
            IList<Node>
#line default
            > mission_activity_body(ref Cursor cursor)
        {
            IParseResult<IList<Node>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = block_delim(ref cursor);
            if (r1 != null)
            {
                IParseResult<string> r2 = null;
                r2 = INDENT(ref cursor);
                if (r2 != null)
                {
                    IParseResult<
#line 114 "Fools.peg"
                        IList<Node>
#line default
                        > r3 = null;
                    var sStart = cursor;
                    r3 = pass_block(ref cursor);
                    var sEnd = cursor;
                    var s = ValueOrDefault(r3);
                    if (r3 != null)
                    {
                        IParseResult<string> r4 = null;
                        r4 = UNINDENT(ref cursor);
                        if (r4 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 116 "Fools.peg"
                                    s
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 118 "Fools.peg"
            IList<string>
#line default
            > possible_block_body(ref Cursor cursor)
        {
            IParseResult<IList<string>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = INDENT(ref cursor);
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                var linesStart = cursor;
                var startCursor1 = cursor;
                var l0 = new List<string>();
                while (true)
                {
                    IParseResult<string> r3 = null;
                    r3 = possible_line(ref cursor);
                    if (r3 != null) l0.Add(r3.Value);
                    else break;
                }
                if (l0.Count >= 1) r2 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
                else cursor = startCursor1;
                var linesEnd = cursor;
                var lines = ValueOrDefault(r2);
                if (r2 != null)
                {
                    IParseResult<IList<Node>> r4 = null;
                    var startCursor2 = cursor;
                    var l1 = new List<Node>();
                    while (true)
                    {
                        IParseResult<Node> r5 = null;
                        r5 = empty_line(ref cursor);
                        if (r5 != null) l1.Add(r5.Value);
                        else break;
                    }
                    if (l1.Count >= 0) r4 = new ParseResult<IList<Node>>(startCursor2, cursor, l1.AsReadOnly());
                    else cursor = startCursor2;
                    if (r4 != null)
                    {
                        IParseResult<string> r6 = null;
                        r6 = UNINDENT(ref cursor);
                        if (r6 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 119 "Fools.peg"
                                    lines
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> possible_line(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            r2 = eof(ref cursor);
            cursor = startCursor1;
            if (r2 == null) r1 = new ParseResult<string>(cursor, cursor, string.Empty);
            if (r1 != null)
            {
                IParseResult<IList<Node>> r3 = null;
                var startCursor2 = cursor;
                var l0 = new List<Node>();
                while (true)
                {
                    IParseResult<Node> r4 = null;
                    r4 = empty_line(ref cursor);
                    if (r4 != null) l0.Add(r4.Value);
                    else break;
                }
                if (l0.Count >= 0) r3 = new ParseResult<IList<Node>>(startCursor2, cursor, l0.AsReadOnly());
                else cursor = startCursor2;
                if (r3 != null)
                {
                    IParseResult<string> r5 = null;
                    r5 = INDENTATION_WITHOUT_ERROR_REPORTING(ref cursor);
                    if (r5 != null)
                    {
                        IParseResult<IList<string>> r6 = null;
                        var sStart = cursor;
                        var startCursor3 = cursor;
                        var l1 = new List<string>();
                        while (true)
                        {
                            IParseResult<string> r7 = null;
                            r7 = ParseClass(ref cursor, "\r\r\n\n", negated: true);
                            if (r7 != null) l1.Add(r7.Value);
                            else break;
                        }
                        if (l1.Count >= 1) r6 = new ParseResult<IList<string>>(startCursor3, cursor, l1.AsReadOnly());
                        else cursor = startCursor3;
                        var sEnd = cursor;
                        var s = ValueOrDefault(r6);
                        if (r6 != null)
                        {
                            r0 = ReturnHelper(startCursor0,
                                cursor,
                                state =>
#line 122 "Fools.peg"
                                    s.Flatten()
#line default
                                );
                        }
                        else cursor = startCursor0;
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<
#line 124 "Fools.peg"
            IList<Node>
#line default
            > pass_block(ref Cursor cursor)
        {
            IParseResult<IList<Node>> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = INDENTATION_WITHOUT_ERROR_REPORTING(ref cursor);
            if (r1 != null)
            {
                IParseResult<string> r2 = null;
                r2 = ParseLiteral(ref cursor, "pass");
                if (r2 != null)
                {
                    IParseResult<string> r3 = null;
                    r3 = eol(ref cursor);
                    if (r3 != null)
                    {
                        r0 = ReturnHelper<IList<Node>>(startCursor0,
                            cursor,
                            state =>
#line 125 "Fools.peg"
                                new List<Node>()
#line default
                            );
                    }
                    else cursor = startCursor0;
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> rest_of_current_line(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<IList<string>> r1 = null;
            var lStart = cursor;
            var startCursor1 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r2 = null;
                r2 = ParseClass(ref cursor, "\r\r\n\n", negated: true);
                if (r2 != null) l0.Add(r2.Value);
                else break;
            }
            if (l0.Count >= 0) r1 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
            else cursor = startCursor1;
            var lEnd = cursor;
            var l = ValueOrDefault(r1);
            if (r1 != null)
            {
                r0 = ReturnHelper(startCursor0,
                    cursor,
                    state =>
#line 128 "Fools.peg"
                        l.Flatten()
#line default
                    );
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> name(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            var nStart = cursor;
            var startCursor1 = cursor;
            IParseResult<string> r2 = null;
            r2 = ParseClass(ref cursor, "azAZ");
            if (r2 != null)
            {
                IParseResult<IList<string>> r3 = null;
                var startCursor2 = cursor;
                var l0 = new List<string>();
                while (true)
                {
                    IParseResult<string> r4 = null;
                    r4 = ParseClass(ref cursor, "azAZ09__");
                    if (r4 != null) l0.Add(r4.Value);
                    else break;
                }
                if (l0.Count >= 0) r3 = new ParseResult<IList<string>>(startCursor2, cursor, l0.AsReadOnly());
                else cursor = startCursor2;
                if (r3 != null)
                {
                    var len = cursor.Location - startCursor1.Location;
                    r1 = new ParseResult<string>(startCursor1,
                        cursor,
                        cursor.Subject.Substring(startCursor1.Location, len));
                }
                else cursor = startCursor1;
            }
            else cursor = startCursor1;
            var nEnd = cursor;
            var n = ValueOrDefault(r1);
            if (r1 != null)
            {
                IParseResult<IList<string>> r5 = null;
                r5 = __(ref cursor);
                if (r5 != null)
                {
                    r0 = ReturnHelper(startCursor0,
                        cursor,
                        state =>
#line 131 "Fools.peg"
                            n
#line default
                        );
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<IList<string>> __(ref Cursor cursor)
        {
            IParseResult<IList<string>> r0 = null;
            var startCursor0 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r1 = null;
                r1 = ParseClass(ref cursor, "  ");
                if (r1 != null) l0.Add(r1.Value);
                else break;
            }
            if (l0.Count >= 0) r0 = new ParseResult<IList<string>>(startCursor0, cursor, l0.AsReadOnly());
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> eol(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            if (r0 == null) r0 = eol_not_eof(ref cursor);
            if (r0 == null) r0 = eof(ref cursor);
            return r0;
        }

        private IParseResult<string> eol_not_eof(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            if (r0 == null) r0 = ParseLiteral(ref cursor, "\r\n");
            if (r0 == null) r0 = ParseLiteral(ref cursor, "\n\r");
            if (r0 == null) r0 = ParseLiteral(ref cursor, "\r");
            if (r0 == null) r0 = ParseLiteral(ref cursor, "\n");
            return r0;
        }

        private IParseResult<string> comment(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseLiteral(ref cursor, "//");
            if (r1 != null)
            {
                IParseResult<IList<string>> r2 = null;
                var startCursor1 = cursor;
                var l0 = new List<string>();
                while (true)
                {
                    IParseResult<string> r3 = null;
                    r3 = ParseClass(ref cursor, "\r\r\n\n", negated: true);
                    if (r3 != null) l0.Add(r3.Value);
                    else break;
                }
                if (l0.Count >= 0) r2 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
                else cursor = startCursor1;
                if (r2 != null)
                {
                    var len = cursor.Location - startCursor0.Location;
                    r0 = new ParseResult<string>(startCursor0,
                        cursor,
                        cursor.Subject.Substring(startCursor0.Location, len));
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> eof(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<string> r1 = null;
            r1 = ParseAny(ref cursor);
            cursor = startCursor0;
            if (r1 == null) r0 = new ParseResult<string>(cursor, cursor, string.Empty);
            return r0;
        }

        private IParseResult<string> INDENTATION(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<IList<string>> r1 = null;
            var indentsStart = cursor;
            var startCursor1 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r2 = null;
                r2 = ParseLiteral(ref cursor, "\t");
                if (r2 != null) l0.Add(r2.Value);
                else break;
            }
            if (l0.Count >= 0) r1 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
            else cursor = startCursor1;
            var indentsEnd = cursor;
            var indents = ValueOrDefault(r1);
            if (r1 != null)
            {
                IParseResult<string> r3 = null;
                if (r3 == null)
                {
                    var startCursor2 = cursor;
                    IParseResult<string> r4 = null;
                    var codeStart = cursor;
                    var startCursor3 = cursor;
                    IParseResult<string> r5 = null;
                    r5 = ParseLiteral(ref cursor, " ");
                    if (r5 != null)
                    {
                        IParseResult<IList<string>> r6 = null;
                        var startCursor4 = cursor;
                        var l1 = new List<string>();
                        while (true)
                        {
                            IParseResult<string> r7 = null;
                            r7 = ParseClass(ref cursor, "\r\r\n\n", negated: true);
                            if (r7 != null) l1.Add(r7.Value);
                            else break;
                        }
                        if (l1.Count >= 0) r6 = new ParseResult<IList<string>>(startCursor4, cursor, l1.AsReadOnly());
                        else cursor = startCursor4;
                        if (r6 != null)
                        {
                            var len = cursor.Location - startCursor3.Location;
                            r4 = new ParseResult<string>(startCursor3,
                                cursor,
                                cursor.Subject.Substring(startCursor3.Location, len));
                        }
                        else cursor = startCursor3;
                    }
                    else cursor = startCursor3;
                    var codeEnd = cursor;
                    var code = ValueOrDefault(r4);
                    if (r4 != null)
                    {
                        IParseResult<string> r8 = null;
                        if (new Func<Cursor, bool>(state =>
#line 145 "Fools.peg"
                            report.indent_with_spaces_error(state, indents.Flatten() + code)
#line default
                            )(cursor)) r8 = new ParseResult<string>(cursor, cursor, string.Empty);
                        if (r8 != null)
                        {
                            r3 = ReturnHelper(startCursor2,
                                cursor,
                                state =>
#line 145 "Fools.peg"
                                    String.Empty
#line default
                                );
                        }
                        else cursor = startCursor2;
                    }
                    else cursor = startCursor2;
                }
                if (r3 == null)
                {
                    if (new Func<Cursor, bool>(state =>
#line 146 "Fools.peg"
                        indents.Count == state["Indentation"]
#line default
                        )(cursor)) r3 = new ParseResult<string>(cursor, cursor, string.Empty);
                }
                if (r3 == null)
                {
                    var startCursor5 = cursor;
                    IParseResult<string> r9 = null;
                    if (new Func<Cursor, bool>(state =>
#line 147 "Fools.peg"
                        indents.Count > state["Indentation"]
#line default
                        )(cursor)) r9 = new ParseResult<string>(cursor, cursor, string.Empty);
                    if (r9 != null)
                    {
                        IParseResult<string> r10 = null;
                        var codeStart = cursor;
                        r10 = rest_of_current_line(ref cursor);
                        var codeEnd = cursor;
                        var code = ValueOrDefault(r10);
                        if (r10 != null)
                        {
                            IParseResult<string> r11 = null;
                            if (new Func<Cursor, bool>(state =>
#line 147 "Fools.peg"
                                report.indentation_error(state,
                                    state["Indentation"],
                                    indents.Count,
                                    indents.Flatten() + code)
#line default
                                )(cursor)) r11 = new ParseResult<string>(cursor, cursor, string.Empty);
                            if (r11 != null)
                            {
                                r3 = ReturnHelper(startCursor5,
                                    cursor,
                                    state =>
#line 147 "Fools.peg"
                                        String.Empty
#line default
                                    );
                            }
                            else cursor = startCursor5;
                        }
                        else cursor = startCursor5;
                    }
                    else cursor = startCursor5;
                }
                if (r3 != null)
                {
                    var len = cursor.Location - startCursor0.Location;
                    r0 = new ParseResult<string>(startCursor0,
                        cursor,
                        cursor.Subject.Substring(startCursor0.Location, len));
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> INDENTATION_WITHOUT_ERROR_REPORTING(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            IParseResult<IList<string>> r1 = null;
            var indentsStart = cursor;
            var startCursor1 = cursor;
            var l0 = new List<string>();
            while (true)
            {
                IParseResult<string> r2 = null;
                r2 = ParseLiteral(ref cursor, "\t");
                if (r2 != null) l0.Add(r2.Value);
                else break;
            }
            if (l0.Count >= 0) r1 = new ParseResult<IList<string>>(startCursor1, cursor, l0.AsReadOnly());
            else cursor = startCursor1;
            var indentsEnd = cursor;
            var indents = ValueOrDefault(r1);
            if (r1 != null)
            {
                IParseResult<string> r3 = null;
                if (new Func<Cursor, bool>(state =>
#line 150 "Fools.peg"
                    indents.Count == state["Indentation"]
#line default
                    )(cursor)) r3 = new ParseResult<string>(cursor, cursor, string.Empty);
                if (r3 != null)
                {
                    var len = cursor.Location - startCursor0.Location;
                    r0 = new ParseResult<string>(startCursor0,
                        cursor,
                        cursor.Subject.Substring(startCursor0.Location, len));
                }
                else cursor = startCursor0;
            }
            else cursor = startCursor0;
            return r0;
        }

        private IParseResult<string> INDENT(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            {
                var state = cursor.WithMutability(mutable: true);
#line 153 "Fools.peg"
                state["Indentation"] += 1;
#line default
                cursor = state.WithMutability(mutable: false);
            }
            r0 = new ParseResult<string>(startCursor0, cursor, null);
            return r0;
        }

        private IParseResult<string> UNINDENT(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            {
                var state = cursor.WithMutability(mutable: true);
#line 156 "Fools.peg"
                state["Indentation"] -= 1;
#line default
                cursor = state.WithMutability(mutable: false);
            }
            r0 = new ParseResult<string>(startCursor0, cursor, null);
            return r0;
        }

        private IParseResult<string> INIT_INDENTATION(ref Cursor cursor)
        {
            IParseResult<string> r0 = null;
            var startCursor0 = cursor;
            {
                var state = cursor.WithMutability(mutable: true);
#line 159 "Fools.peg"
                state["Indentation"] = 0;
#line default
                cursor = state.WithMutability(mutable: false);
            }
            r0 = new ParseResult<string>(startCursor0, cursor, null);
            return r0;
        }

        private IParseResult<string> ParseLiteral(ref Cursor cursor, string literal, bool ignoreCase = false)
        {
            if (cursor.Location + literal.Length <= cursor.Subject.Length)
            {
                var substr = cursor.Subject.Substring(cursor.Location, literal.Length);
                if (ignoreCase ? substr.Equals(literal, StringComparison.OrdinalIgnoreCase) : substr == literal)
                {
                    var endCursor = cursor.Advance(substr.Length);
                    var result = new ParseResult<string>(cursor, endCursor, substr);
                    cursor = endCursor;
                    return result;
                }
            }
            return null;
        }

        private IParseResult<string> ParseClass(ref Cursor cursor,
            string characterRanges,
            bool negated = false,
            bool ignoreCase = false)
        {
            if (cursor.Location + 1 <= cursor.Subject.Length)
            {
                var c = cursor.Subject[cursor.Location];
                var match = false;
                for (var i = 0; !match && i < characterRanges.Length; i += 2)
                {
                    match = c >= characterRanges[i] && c <= characterRanges[i + 1];
                }
                if (!match && ignoreCase && (char.IsUpper(c) || char.IsLower(c)))
                {
                    var cs = c.ToString();
                    for (var i = 0; !match && i < characterRanges.Length; i += 2)
                    {
                        var min = characterRanges[i];
                        var max = characterRanges[i + 1];
                        for (var o = min; !match && o <= max; o++)
                        {
                            match = (char.IsUpper(o) || char.IsLower(o))
                                && cs.Equals(o.ToString(), StringComparison.CurrentCultureIgnoreCase);
                        }
                    }
                }
                if (match ^ negated)
                {
                    var endCursor = cursor.Advance(1);
                    var result = new ParseResult<string>(cursor, endCursor, cursor.Subject.Substring(cursor.Location, 1));
                    cursor = endCursor;
                    return result;
                }
            }
            return null;
        }

        private IParseResult<string> ParseAny(ref Cursor cursor)
        {
            if (cursor.Location + 1 <= cursor.Subject.Length)
            {
                var substr = cursor.Subject.Substring(cursor.Location, 1);
                var endCursor = cursor.Advance(1);
                var result = new ParseResult<string>(cursor, endCursor, substr);
                cursor = endCursor;
                return result;
            }
            return null;
        }

        private IParseResult<T> ReturnHelper<T>(Cursor startCursor, Cursor endCursor, Func<Cursor, T> wrappedCode)
        {
            return new ParseResult<T>(startCursor, endCursor, wrappedCode(endCursor));
        }

        private Exception ExceptionHelper(Cursor cursor, Func<Cursor, string> wrappedCode)
        {
            var ex = new FormatException(wrappedCode(cursor));
            ex.Data["cursor"] = cursor;
            return ex;
        }

        private T ValueOrDefault<T>(IParseResult<T> result)
        {
            return result == null ? default(T) : result.Value;
        }
    }
}
