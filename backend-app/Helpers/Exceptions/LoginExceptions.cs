namespace backend_app.Helpers.Exceptions;

public class LoginExceptions
{
    public class UserNotFoundException : BaseCustomException
    {
        public override int StatusCode => 401;
        public UserNotFoundException(
            string parameter,
            string? detailedMessage = null
        ) : base("Credenciais inválidas", detailedMessage ?? $"Usuário não encontrado: {parameter}")
        {
        }
    }

    public class InvalidCredentialsException : BaseCustomException
    {
        public override int StatusCode => 401;

        public InvalidCredentialsException(string? detailedMessage = null)
            : base("Credenciais inválidas", detailedMessage ?? "Senha incorreta fornecida")
        {
        }
    }
}
