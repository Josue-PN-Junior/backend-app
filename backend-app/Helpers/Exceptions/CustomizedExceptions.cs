namespace backend_app.Helpers.Exceptions;

public class CustomizedExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string parametro) 
            : base($"Usuário não encontrado: {parametro}")
        {
        }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string parametro) : base($"Credencias invalidas: {parametro}")
        {
        }
    }
}
