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
  Moderator
}
export enum Status{
    Active=0,
    Inactive,
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
 
export interface UserProfile {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  patronymic: string | null;
  role: UserRole;
  status: Status;
  dateReg: string;
  email: string;
  phone: string;
  avator: string | null;
  emailConfirmed: boolean;
}

export interface UpdateProfileRequest {
  userName?: string;
  rirstName?: string;
  lastName?: string;
  patronymic?: string | null;
  email?: string;
  phone?: string;
}