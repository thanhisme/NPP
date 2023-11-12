namespace Utils.Constants.Strings
{
    public class ValidationErrorMessages
    {
        public const string FIELD_REQUIRED_ERROR_MESSAGE = "{0} is required";

        public const string LENGTH_RANGE_ERROR_MESSAGE = "{0} must have length between {2} and {1}";

        public const string CONFIRM_PASSWORD_NOT_MATCH = "Password confirm doesn't match";

        public static string MAX_FILE_SIZE_ERROR { get; } = "File size must be less than {0} MB";

        public static string ALLOWED_FILE_EXTENSIONS_ERROR { get; } = "File extension must be in: {0}";

        public static string UNIQUE_CONTRAINT_ERROR { get; } = "Value {0} already exists!";

        public static string NOT_ALLOWED_EMAIL { get; } = "This email is not allowed!";

        public static string INVALID_URL { get; } = "Invalid URL";

        public static string INVALID_TEL { get; } = "Invalid tel number";
    }
}
