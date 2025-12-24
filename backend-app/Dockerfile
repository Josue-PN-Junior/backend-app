FROM postgres:15

# Variáveis de ambiente para configuração do PostgreSQL
ENV POSTGRES_PASSWORD=123
ENV POSTGRES_USER=postgres
ENV POSTGRES_DB=initial

# Expõe a porta padrão do PostgreSQL
EXPOSE 5432

# Cria um diretório para o volume de dados
RUN mkdir -p /var/lib/postgresql/data

# Define o volume para persistência dos dados
VOLUME ["/var/lib/postgresql/data"]

# O comando CMD já está definido na imagem base do postgres