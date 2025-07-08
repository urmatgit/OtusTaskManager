psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE DATABASE keycloakdb;
    CREATE USER postgres WITH ENCRYPTED PASSWORD 'postgres';
    GRANT ALL PRIVILEGES ON DATABASE keycloakdb TO postgres;
EOSQL