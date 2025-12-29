namespace backend_app.Helpers.Exceptions;

public class CustomizedExceptions
{
    public class UserNotFoundException : BaseCustomException
    {
        public override int StatusCode => 404;

        public UserNotFoundException(string parameter)
            : base("Usuário não encontrado", $"Usuário não encontrado para operação: {parameter}")
        {
        }
    }

    public class EmailAlredyExistException : BaseCustomException
    {
        public override int StatusCode => 409;

        public EmailAlredyExistException(string parameter)
            : base("Tente cadastrar outro email", $"E-mail já cadastrado: {parameter}")
        {
        }
    }
}
