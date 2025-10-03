// src/services/userServiceClient.ts
import { grpc } from '@improbable-eng/grpc-web';
import { UserService } from '../proto/users_pb_service';
import {
  UserResponseGRPC,
  UsersResponseGRPC,
  GetUserRequestGRPC,
  GetUsersRequestGRPC,
} from '../proto/users_pb';

export interface User {
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
}

class UserServiceClient {
  private readonly host: string;

  constructor(host: string = 'http://localhost:5191/api/') {
    this.host = host;
  }

  private mapUserResponse(user: UserResponseGRPC): User {
    return {
      userId: user.getUserId(),
      email: user.getEmail(),
      firstName: user.getFirstName(),
      lastName: user.getLastName(),
      displayName: user.getDisplayName(),
    };
  }

  async getUser(userId: string): Promise<User> {
    return new Promise((resolve, reject) => {
      const request = new GetUserRequestGRPC();
      request.setUserId(userId);

      grpc.invoke(UserService.GetUser, {
        request,
        host: this.host,
        onMessage: (message: UserResponseGRPC) => {
          resolve(this.mapUserResponse(message));
        },
        onEnd: (code: grpc.Code, message: string) => {
          if (code !== grpc.Code.OK) {
            reject(new Error(`gRPC error: ${code} - ${message}`));
          }
        },
      });
    });
  }

  async getUsers(): Promise<User[]> {
    return new Promise((resolve, reject) => {
      const request = new GetUsersRequestGRPC();

      grpc.invoke(UserService.GetUsers, {
        request,
        host: this.host,
        onMessage: (message: UsersResponseGRPC) => {
          const users = message.getUsersList().map(user => this.mapUserResponse(user));
          resolve(users);
        },
        onEnd: (code: grpc.Code, message: string) => {
          if (code !== grpc.Code.OK) {
            reject(new Error(`gRPC error: ${code} - ${message}`));
          }
        },
      });
    });
  }
}

export const userServiceClient = new UserServiceClient();
export default UserServiceClient;