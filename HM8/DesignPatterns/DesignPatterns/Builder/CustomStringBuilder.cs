using System;
using System.Linq;

namespace DesignPatterns.Builder
{
    public class CustomStringBuilder : ICustomStringBuilder
    {
        private char[] _text;

        public CustomStringBuilder()
        {
            _text = new char[] { };
        }

        public CustomStringBuilder(string text)
        {
            _text = text.ToCharArray();
        }

        public ICustomStringBuilder Append(string str)
        {
            _text = _text
                .Concat(str.ToCharArray())
                .ToArray();

            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            _text = _text
                .Concat(new char[] { ch })
                .ToArray();

            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            _text = _text
                .Concat(new char[] { '\n' })
                .ToArray();

            return this;
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            _text = _text
                .Concat(
                    str.ToCharArray()
                        .Concat(new char[] { '\n' })
                    ).ToArray();

            return this;
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            _text = _text
                .Concat(new char[] {ch, '\n'})
                .ToArray();


            return this;
        }

        public string Build()
        {
            return new string(_text);
        }
    }
}