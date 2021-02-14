using System;
using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class RemoveNumbersMutator : IStringMutator
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
            str = new string(arr.Where(x => !char.IsDigit(x)).ToArray());

            if (this._nextHandler != null)
            { 
                str = this._nextHandler.Mutate(str);
            }

            return str;
        }
    }
}