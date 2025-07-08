psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    CREATE DATABASE taskboardDb;
    CREATE USER taskmanager_user WITH ENCRYPTED PASSWORD 'postgres';
    GRANT ALL PRIVILEGES ON DATABASE taskboardDb TO postgres;
    
    CREATE DATABASE keycloakdb;
    CREATE USER keycloak_user WITH ENCRYPTED PASSWORD 'postgres';
    GRANT ALL PRIVILEGES ON DATABASE keycloakdb TO postgres;
EOSQL