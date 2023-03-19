using System.Collections.Generic;

namespace Ink
{
    public partial class InkParser
    {
        // Handles both newline and endOfFile
        protected object EndOfLine()
        {
            return OneOf(Newline, EndOfFile);
        }

        // Allow whitespace before the actual newline
        protected object Newline()
        {
            Whitespace();

            bool gotNewline = ParseNewline() != null;

            // Optional \r, definite \n to support Windows (\r\n) and Mac/Unix (\n)

            return !gotNewline ? null : (object)ParseSuccess;
        }

        protected object EndOfFile()
        {
            Whitespace();

            return !endOfInput ? null : (object)ParseSuccess;
        }


        // General purpose space, returns N-count newlines (fails if no newlines)
        protected object MultilineWhitespace()
        {
            List<object> newlines = OneOrMore(Newline);
            if (newlines == null)
                return null;

            // Use content field of Token to say how many newlines there were
            // (in most circumstances it's unimportant)
            int numNewlines = newlines.Count;
            return numNewlines >= 1 ? ParseSuccess : (object)null;
        }

        protected object Whitespace()
        {
            return ParseCharactersFromCharSet(_inlineWhitespaceChars) != null ? ParseSuccess : (object)null;
        }

        protected ParseRule Spaced(ParseRule rule)
        {
            return () =>
            {

                Whitespace();

                var result = ParseObject(rule);
                if (result == null)
                {
                    return null;
                }

                Whitespace();

                return result;
            };
        }

        protected object AnyWhitespace()
        {
            bool anyWhitespace = false;
            while (OneOf(Whitespace, MultilineWhitespace) != null)
            {
                anyWhitespace = true;
            }
            return anyWhitespace ? ParseSuccess : null;
        }

        protected ParseRule MultiSpaced(ParseRule rule)
        {
            return () =>
            {

                AnyWhitespace();

                var result = ParseObject(rule);
                if (result == null)
                {
                    return null;
                }

                AnyWhitespace();

                return result;
            };
        }

        private CharacterSet _inlineWhitespaceChars = new CharacterSet(" \t");
    }
}

