namespace Identity_10_.ErrorHandeling
{
    public record Error(string code , string descreption)
    {
        public readonly static Error none = new(string.Empty, string.Empty);
    }
}
