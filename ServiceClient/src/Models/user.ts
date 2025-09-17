export interface User {
  id: string;
  userName: string;
  email: string;
  role: UserRole;
  dateReg: string;
  phone?: string;
  firstName: string;
  lastName: string;
}

export enum UserRole {
  User = 1,
  Admin, 
  Owner ,
  Editor
}

export interface ChangeRoleRequest {
  userId: string;
  newRole: UserRole;
}

export interface ApiResponse<T> {
  data: T;
  message: string;
  success: boolean;
}
