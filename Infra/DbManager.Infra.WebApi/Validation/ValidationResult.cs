namespace DbManager.Infra.WebApi.Validation
{
    internal struct ValidationResult
    {
        public ValidationResult(string error)
        {
            Error = error;
        }

        public bool IsValid => string.IsNullOrWhiteSpace(Error);

        public string Error { get; }
    }
}