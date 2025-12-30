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

    public class InvalidCodeException : BaseCustomException
    {
        public override int StatusCode => 404;

        public InvalidCodeException(string parameter)
            : base("Código inválido ou expirado", $"Código inválido: {parameter}")
        {
        }
    }

    public class ExpiredCodeException : BaseCustomException
    {
        public override int StatusCode => 404;

        public ExpiredCodeException(string parameter)
            : base("Código inválido ou expirado", $"Código expirou: {parameter}")
        {
        }
    }

    public class UserCodeNotFoundException : BaseCustomException
    {
        public override int StatusCode => 404;

        public UserCodeNotFoundException(string parameter)
            : base("Código inválido ou expirado", $"Usuário não encontrado: {parameter}")
        {
        }
    }

    public class CodeNotFoundException : BaseCustomException
    {
        public override int StatusCode => 404;

        public CodeNotFoundException(string parameter)
            : base("Código inválido ou expirado", $"Código não encontrado: {parameter}")
        {
        }
    }
}
