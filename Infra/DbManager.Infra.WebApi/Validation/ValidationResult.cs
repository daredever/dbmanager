namespace DbManager.Infra.WebApi.Validation
{
    public readonly struct ValidationResult
    {
        public ValidationResult(string error)
        {
            Error = error;
        }

        public bool IsValid => string.IsNullOrWhiteSpace(Error);

        public string Error { get; }
    }
}