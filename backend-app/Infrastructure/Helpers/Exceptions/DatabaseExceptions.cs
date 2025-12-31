namespace backend_app.Helpers.Exceptions;

public class DatabaseExceptions
{
    public class DatabaseConnectionException : BaseCustomException
    {
        public override int StatusCode => 503;

        public DatabaseConnectionException(string operation, Exception innerException)
            : base("Service temporarily unavailable", $"Database connection error during: {operation}. Details: {innerException.Message}")
        {
        }
    }

    public class DatabaseOperationException : BaseCustomException
    {
        public override int StatusCode => 500;

        public DatabaseOperationException(string operation, Exception innerException)
            : base("Internal server error", $"Error in bank transaction '{operation}'. Details: {innerException.Message}")
        {
        }
    }

    public class DuplicateKeyException : BaseCustomException
    {
        public override int StatusCode => 409;

        public DuplicateKeyException(string field, string value)
            : base("Data conflict", $"Attempt to enter duplicate value in the field '{field}': {value}")
        {
        }
    }

    public class ConcurrencyException : BaseCustomException
    {
        public override int StatusCode => 409;

        public ConcurrencyException(string operation)
            : base("Data conflict", $"Concurrency conflict occurred during: {operation}. The data was modified by another process.")
        {
        }
    }

    public class DeleteCodeException : BaseCustomException
    {
        public override int StatusCode => 404;

        public DeleteCodeException(string operation, Exception innerException)
            : base("Invalid or expired code", $"Error in bank transaction '{operation}'. Details: { innerException.Message}")
        {
        }
    }
}
