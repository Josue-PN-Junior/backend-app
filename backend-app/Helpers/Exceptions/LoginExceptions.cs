namespace backend_app.Helpers.Exceptions;

public class LoginExceptions
{
    public class UserNotFoundException : BaseCustomException
    {
        public override int StatusCode => 401;
        public UserNotFoundException(
            string parameter,
            string? detailedMessage = null
        ) : base("Invalid credentials", detailedMessage ?? $"User not found: {parameter}")
        {
        }
    }

    public class InvalidCredentialsException : BaseCustomException
    {
        public override int StatusCode => 401;

        public InvalidCredentialsException(string? detailedMessage = null)
            : base("Invalid credentials", detailedMessage ?? "Incorrect password provided")
        {
        }
    }

    public class TokenFailGenerationException : BaseCustomException
    {
        public override int StatusCode => 500;

        public TokenFailGenerationException(string? detailedMessage = null)
            : base("Internal server error", detailedMessage ?? "Token creation failed")
        {
        }
    }
}
