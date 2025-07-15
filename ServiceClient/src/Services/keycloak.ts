import Keycloak from 'keycloak-js';

const _keycloakInstance  = new Keycloak({
  url: 'http://localhost:9080',
  realm: 'TaskManagerRealm',
  clientId: 'taskmanagerID',
  aud: 'taskmanagerID'
});



export const keycloak=_keycloakInstance ;