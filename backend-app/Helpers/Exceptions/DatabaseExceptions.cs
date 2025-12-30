namespace backend_app.Helpers.Exceptions;

public class DatabaseExceptions
{
    public class DatabaseConnectionException : BaseCustomException
    {
        public override int StatusCode => 503;

        public DatabaseConnectionException(string operation, Exception innerException)
            : base("Serviço temporariamente indisponível", $"Erro de conexão com banco durante: {operation}. Detalhes: {innerException.Message}")
        {
        }
    }

    public class DatabaseOperationException : BaseCustomException
    {
        public override int StatusCode => 500; 

        public DatabaseOperationException(string operation, Exception innerException)
            : base("Erro interno do servidor", $"Erro na operação de banco '{operation}'. Detalhes: {innerException.Message}")
        {
        }
    }

    public class DuplicateKeyException : BaseCustomException
    {
        public override int StatusCode => 409;

        public DuplicateKeyException(string field, string value)
            : base("Conflito de dados", $"Tentativa de inserir valor duplicado no campo '{field}': {value}")
        {
        }
    }

    public class ConcurrencyException : BaseCustomException
    {
        public override int StatusCode => 409;

        public ConcurrencyException(string operation)
            : base("Conflito de dados", $"Conflito de concorrência durante: {operation}. Os dados foram modificados por outro processo.")
        {
        }
    }

    public class DeleteCodeException : BaseCustomException
    {
        public override int StatusCode => 404;

        public DeleteCodeException(string operation, Exception innerException)
            : base("Código inválido ou expirado", $"Erro na operação de banco '{operation}'. Detalhes: {innerException.Message}")
        {
        }
    }
}
