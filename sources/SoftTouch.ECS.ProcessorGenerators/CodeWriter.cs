using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftTouch.ECS.ProcessorGenerators
{
    internal class CodeWriter
    {
        public static int indentSize = 4;
        public static char eol = '\n';

        public int indentation = 0;
        bool lineStart = true;
        StringBuilder builder = new StringBuilder();

        public CodeWriter Indent()
        {
            indentation += 1;
            return this;
        }
        public CodeWriter Dedent()
        {
            indentation = Math.Max(indentation - 1, 0);
            return this;
        }

        public CodeWriter Append(string content)
        {
            WriteIndent();
            builder.Append(content);
            lineStart = content.EndsWith("\n");
            return this;
        }
        public CodeWriter Append(char content)
        {
            WriteIndent();
            builder.Append(content);
            lineStart = content == '\n';
            return this;
        }
        public CodeWriter Append(int content)
        {
            WriteIndent();
            builder.Append(content);
            lineStart = false;
            return this;
        }
        public CodeWriter AppendLine(string content)
        {
            WriteIndent();
            builder.AppendLine(content);
            lineStart = true;
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
