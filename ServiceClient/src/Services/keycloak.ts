import Keycloak from 'keycloak-js';

const _keycloak  = new Keycloak({
  url: 'http://localhost:9080',
  realm: 'TaskManagerRealm',
  clientId: 'taskmanagerID'
  
});



export const keycloak=_keycloak ;