namespace DesignPatterns.ChainOfResponsibility
{
    public class ToUpperMutator : IStringMutator
    {
        private IStringMutator _nextHandler;

        public IStringMutator SetNext(IStringMutator next)
        {
            this._nextHandler = next;

            return next;
        }

        public string Mutate(string str)
        {
            str = str.ToUpper();

            if (this._nextHandler != null)
            {
                str = this._nextHandler.Mutate(str);
            }

            return str;
        }
    }
}