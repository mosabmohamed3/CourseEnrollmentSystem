namespace CourseEnrollmentSystem.Helper.Common;

public class BusinessValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<FieldError> Errors { get; set; } = new();
}

public class FieldError
{
    public string FieldName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
