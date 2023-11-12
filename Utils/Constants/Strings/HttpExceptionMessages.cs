namespace Utils.Constants.Strings
{
    public static class HttpExceptionMessages
    {
        public static string INTERNAL_SERVER_ERROR { get; } = "Internal Server Error";

        public static string UNAUTHORIZED { get; } = "Unauthorized";

        public static string NOT_FOUND { get; } = "Not Found";

        public static string FORBIDDEN { get; } = "Forbidden";

        public static string CONTRAINT_ERRORS { get; } = "Contraint Errors";

        public static string VALIDATION_ERRORS { get; } = "Validation Errors";

        public static string UNAUTHENTICATED_USER_ONLY { get; } = "Unauthenticated user only";

        public static string INVALID_USERNAME_OR_PASSWORD { get; } = "Invalid username or password";

        public static string INVALID_REFRESH_TOKEN { get; } = "Invalid refresh token";

        public static string TOKEN_IN_BLACKLIST { get; } = "Token is in black list!";
    }
}
