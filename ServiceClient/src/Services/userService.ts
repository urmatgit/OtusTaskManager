import type { User, ChangeRoleRequest,  UserRole,ApiResponse } from '../Models/user';
import { getCurrentUser,logout } from "./authService";
const API_BASE_URL ='http://localhost:5191';

class ApiService {
  private getAuthToken(): string | undefined {
    return getCurrentUser()?.token;
  }

  private async request<T>(endpoint: string, options: RequestInit = {}): Promise<T> {
    const token = this.getAuthToken();
    const headers: HeadersInit = {
      'Content-Type': 'application/json',
      ...options.headers,
    };

    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      ...options,
      headers,
    });

    if (!response.ok) {
      if (response.status === 401) {
        logout();
        window.location.href = '/login';
      }
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response.json();
  }

  // User management methods
  async getUsers(): Promise<User[]> {
    const response = await this.request<User[]>('/api/user');
    return response;
  }

  async changeUserRole(request: ChangeRoleRequest): Promise<User> {
    const response = await this.request<User>('/api/user/changerole', {
      method: 'POST',
      body: JSON.stringify(request),
    });
    return response;
  }

  async deleteUser(userId: string): Promise<void> {
    await this.request(`/api/user/${userId}`, {
      method: 'DELETE',
    });
  }
}

export const apiService = new ApiService();