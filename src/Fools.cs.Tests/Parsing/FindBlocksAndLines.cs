// FindBlocksAndLines.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using ApprovalTests.Reporters;
using Fools.cs.Tests.Support;
using NUnit.Framework;

namespace Fools.cs.Tests.Parsing
{
    [TestFixture, UseReporter(typeof (DiffReporter))]
    public class FindBlocksAndLines
    {
        [Test]
        public void should_find_nothing_in_an_empty_file()
        {
            string.Empty.find_blocks()
                .should_fail();
        }

        [Test]
        public void file_with_a_single_simple_block_should_parse()
        {
            @"
def foo():
	a = b
	

".find_blocks()
                .should_parse_correctly();
        }

        [Test]
        public void empty_blocks_should_be_explicitly_stated()
        {
            "def foo():\r\n\tpass".find_blocks()
                .should_parse_correctly();
        }

        [Test]
        public void blocks_without_bodies_should_fail()
        {
            "def foo():\r\na=b".find_blocks()
                .should_fail();
        }

        [Test]
        public void blocks_with_only_empty_lines_should_fail()
        {
            "def foo():\r\n\t\r\na=b".find_blocks()
                .should_fail();
        }

        [Test]
        public void should_reject_illegal_indentation_with_good_error_message()
        {
            "def foo():\r\n\tb = c\r\n\t\ta = b".find_blocks()
                .should_fail();
        }

        [Test]
        public void spaces_at_start_of_a_line_should_be_illegal()
        {
            "def foo():\r\n\t b = c".find_blocks()
                .should_fail();
        }

        [Test]
        public void spaces_in_the_indentation_should_be_illegal()
        {
            "def foo():\r\n\tif bar:\r\n\t \tb = c".find_blocks()
                .should_fail();
        }

        [Test]
        public void undefined_block_type_should_be_illegal()
        {
            "frazzle:\r\n\tpass".find_blocks()
                .should_fail();
        }

        [Test]
        public void only_declaration_blocks_should_be_allowed_at_the_top_level()
        {
            "if bar:\r\n\tpass".find_blocks()
                .should_fail();
        }

        [Test]
        public void no_statements_should_be_allowed_at_the_top_level()
        {
            "a=b".find_blocks()
                .should_fail();
        }
    }
}
