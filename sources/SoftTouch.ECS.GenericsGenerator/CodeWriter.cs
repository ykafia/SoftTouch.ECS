using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftTouch.ECS.GenericsGenerator
{
    internal class CodeWriter
    {
        public static int indentSize = 4;
        public static char eol = '\n';

        public int indentation = 0;
        bool lineStart = true;
        StringBuilder builder = new StringBuilder();

        internal CodeWriter Indent()
        {
            indentation += 1;
            return this;
        }
        internal CodeWriter Dedent()
        {
            indentation = Math.Max(indentation - 1, 0);
            return this;
        }

        internal CodeWriter Append(string content)
        {
            WriteIndent();
            builder.Append(content);
            lineStart = content.EndsWith("\n");
            return this;
        }
        internal CodeWriter Append(char content)
        {
            WriteIndent();
            builder.Append(content);
            lineStart = content == '\n';
            return this;
        }
        internal CodeWriter Append(int content)
        {
            WriteIndent();
            builder.Append(content);
            lineStart = false;
            return this;
        }
        public CodeWriter WriteLine(string content)
        {
            WriteIndent();
            builder.AppendLine(content);
            lineStart = true;
            return this;
        }
        public CodeWriter WriteEmptyLines(int count)
        {
            
            for(int i = 0; i < count; i++)
            {
                WriteIndent();
                builder.AppendLine();
            }
            lineStart = true;
            return this;
        }

        public CodeWriter OpenBlock()
        {
            return WriteLine("{").Indent();
        }

        public CodeWriter CloseBlock()
        {
            return Dedent().WriteLine("}");
        }
        public CodeWriter CloseAllBlocks()
        {
            for(int i = 0; i < indentation; i++)
                CloseBlock();
            if(indentation > 0)
            {
                CloseBlock();
            }
            return this;
        }

        void WriteIndent()
        {
            if(lineStart)
                builder.Append(' ', indentSize * indentation);
        }

        public override string ToString() => builder.ToString();

    }
}
