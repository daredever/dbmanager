using DbManager.Infra.WebApi.Dto;

namespace DbManager.Infra.WebApi.Validation
{
    internal static class ValidateExtensions
    {
        private const string CatalogIsEmptyErrorMessage = "Catalog must be specified.";
        private const string SchemaIsEmptyErrorMessage = "Schema must be specified.";
        private const string NameIsEmptyErrorMessage = "Name must be specified.";
        private const string TypeIsEmptyErrorMessage = "Type must be specified.";
        private const string NullableIsEmptyErrorMessage = "Is Nullable flag must be specified.";

        public static ValidationResult Validate(this ColumnDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Catalog))
            {
                return new ValidationResult(CatalogIsEmptyErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.Schema))
            {
                return new ValidationResult(SchemaIsEmptyErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return new ValidationResult(NameIsEmptyErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.Type))
            {
                return new ValidationResult(TypeIsEmptyErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.IsNullable))
            {
                return new ValidationResult(NullableIsEmptyErrorMessage);
            }

            return new ValidationResult();
        }

        public static ValidationResult Validate(this TableDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Catalog))
            {
                return new ValidationResult(CatalogIsEmptyErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.Schema))
            {
                return new ValidationResult(SchemaIsEmptyErrorMessage);
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return new ValidationResult(NameIsEmptyErrorMessage);
            }

            return new ValidationResult();
        }

        public static ValidationResult Validate(this CatalogDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return new ValidationResult(NameIsEmptyErrorMessage);
            }

            return new ValidationResult();
        }
    }
}