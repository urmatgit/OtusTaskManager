// src/types/user.ts
export interface UserGRPC {
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
}

export interface GetUserRequestGRPC {
  userId: string;
}

export interface GetUsersRequestGRPC  {
  // пустой запрос
}

export interface UsersResponseGRPC  {
  users: UserGRPC[];
}

export interface UserResponseGRPC {
  user: UserGRPC;
}