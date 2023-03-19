using System.Collections.Generic;
using System.Text;

namespace Ink.Parsed
{
    public class StringExpression : Parsed.Expression
    {
        public bool isSingleString
        {
            get
            {
                if (content.Count != 1)
                    return false;

                var c = content[0];
                return c is Text;
            }
        }

        public StringExpression(List<Parsed.Object> content)
        {
            AddContent(content);
        }

        public override void GenerateIntoContainer(Runtime.Container container)
        {
            container.AddContent(Runtime.ControlCommand.BeginString());

            foreach (var c in content)
            {
                container.AddContent(c.runtimeObject);
            }

            container.AddContent(Runtime.ControlCommand.EndString());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var c in content)
            {
                sb.Append(c.ToString());
            }
            return sb.ToString();
        }

        // Equals override necessary in order to check for CONST multiple definition equality
        public override bool Equals(object obj)
        {
            var otherStr = obj as StringExpression;
            if (otherStr == null) return false;

            // Can only compare direct equality on single strings rather than
            // complex string expressions that contain dynamic logic
            if (!isSingleString || !otherStr.isSingleString)
            {
                return false;
            }

            var thisTxt = ToString();
            var otherTxt = otherStr.ToString();
            return thisTxt.Equals(otherTxt);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}

