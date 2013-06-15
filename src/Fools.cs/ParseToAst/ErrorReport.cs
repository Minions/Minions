// ErrorReport.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Pegasus.Common;

namespace Fools.cs.ParseToAst
{
    public class ErrorReport
    {
        public string file_name { get; private set; }
        public int line { get; private set; }
        public string error_type { get; private set; }
        public object expected { get; private set; }
        public object actual { get; private set; }
        public string snippet { get; private set; }
        public string suggestion { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    "{4} on line {2}.\r\n\tExpected code to be indented {0}, but it was indented {1}.\r\n\tThe code reads:\r\n\"{3}\"\r\n\r\n{5}",
                    expected,
                    actual,
                    line,
                    snippet,
                    error_type,
                    suggestion);
        }

        public ErrorReport(string error_type,
            Cursor state,
            object expected,
            object actual,
            string snippet,
            string suggestion)
        {
            line = state == null ? 0 : state.Line;
            file_name = state == null ? string.Empty : state.FileName;
            this.error_type = error_type;
            this.expected = expected;
            this.actual = actual;
            this.snippet = snippet;
            this.suggestion = suggestion;
        }
    }
}
