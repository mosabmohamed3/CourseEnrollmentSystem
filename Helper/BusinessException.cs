namespace CourseEnrollmentSystem.Helper;

public class BusinessValidationResult
{
    public bool IsValid => !Errors.Any();
    public List<FieldError> Errors { get; set; } = new();
}

public class FieldError
{
    public string FieldName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
