namespace DesignPatterns.ChainOfResponsibility
{
    public class TrimMutator : IStringMutator
    {
        private IStringMutator _nextHandler;

        public IStringMutator SetNext(IStringMutator next)
        {
            this._nextHandler = next;

            return next;
        }

        public string Mutate(string str)
        {
            str = str.Trim();

            if (this._nextHandler != null)
            {
               str = this._nextHandler.Mutate(str);
            }

            return str;
        }
    }
}