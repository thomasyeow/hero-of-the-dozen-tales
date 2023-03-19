
namespace Ink.Parsed
{
    public class Number : Parsed.Expression
    {
        public object value;

        public Number(object value)
        {
            this.value = value is int || value is float || value is bool ? value : throw new System.Exception("Unexpected object type in Number");
        }

        public override void GenerateIntoContainer(Runtime.Container container)
        {
            if (value is int)
            {
                container.AddContent(new Runtime.IntValue((int)value));
            }
            else if (value is float)
            {
                container.AddContent(new Runtime.FloatValue((float)value));
            }
            else if (value is bool)
            {
                container.AddContent(new Runtime.BoolValue((bool)value));
            }
        }

        public override string ToString()
        {
            return value is float ? ((float)value).ToString(System.Globalization.CultureInfo.InvariantCulture) : value.ToString();
        }

        // Equals override necessary in order to check for CONST multiple definition equality
        public override bool Equals(object obj)
        {
            var otherNum = obj as Number;
            return otherNum == null ? false : value.Equals(otherNum.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

    }
}

