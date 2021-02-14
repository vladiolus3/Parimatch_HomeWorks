using System;

namespace DesignPatterns.ChainOfResponsibility
{
    public class InvertMutator : IStringMutator
    {
        private IStringMutator _nextHandler;

        public IStringMutator SetNext(IStringMutator next)
        {
            this._nextHandler = next;

            return next;
        }

        public string Mutate(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            str = new string(arr);

            if (this._nextHandler != null)
            {
                str = this._nextHandler.Mutate(str);
            }

            return str;
        }
    }
}