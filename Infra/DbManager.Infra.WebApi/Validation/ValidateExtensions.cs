using DbManager.Infra.WebApi.Dto;

namespace DbManager.Infra.WebApi.Validation
{
    internal static class ValidateExtensions
    {
        public static ValidationResult Validate(this ColumnDto dto)
        {
            return new ValidationResult();
        }

        public static ValidationResult Validate(this TableDto dto)
        {
            return new ValidationResult();
        }

        public static ValidationResult Validate(this CatalogDto dto)
        {
            return new ValidationResult();
        }
    }
}