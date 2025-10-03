// src/services/UserServiceClient.ts
import { grpc } from '@improbable-eng/grpc-web';
import type { UserGRPC, GetUserRequestGRPC, GetUsersRequestGRPC } from '../proto/userGen';

export class UserServiceClient {
  private readonly host: string;

  constructor(host: string = 'http://localhost:5000') {
    this.host = host;
  }

  // Простая сериализация строковых полей
  private serializeStringField(fieldNumber: number, value: string): Uint8Array {
    const valueBytes = new TextEncoder().encode(value);
    const buffer = new Uint8Array(2 + valueBytes.length);
    
    // tag: field_number << 3 | wire_type(2 для string)
    buffer[0] = (fieldNumber << 3) | 2;
    buffer[1] = valueBytes.length;
    buffer.set(valueBytes, 2);
    
    return buffer;
  }

  private serializeGetUserRequest(request: GetUserRequestGRPC): Uint8Array {
    return this.serializeStringField(1, request.userId);
  }

  private deserializeUser(data: Uint8Array): UserGRPC {
    const decoder = new TextDecoder();
    let offset = 0;
    const user: Partial<UserGRPC> = {};

    while (offset < data.length) {
      if (offset >= data.length) break;

      const tag = data[offset++];
      const fieldNumber = tag >> 3;
      const wireType = tag & 0x07;

      if (wireType === 2) { // string
        if (offset >= data.length) break;
        
        let length = data[offset++];
        // Обработка varint для длины (упрощенная версия)
        if (length & 0x80) {
          length = (length & 0x7f) | ((data[offset++] & 0x7f) << 7);
        }

        if (offset + length > data.length) break;

        const value = decoder.decode(data.subarray(offset, offset + length));
        offset += length;

        switch (fieldNumber) {
          case 1: user.userId = value; break;
          case 2: user.email = value; break;
          case 3: user.firstName = value; break;
          case 4: user.lastName = value; break;
          case 5: user.displayName = value; break;
        }
      } else {
        // Пропускаем другие типы
        offset++;
      }
    }

    return user as UserGRPC;
  }

  async getUser(userId: string): Promise<UserGRPC> {
    return new Promise((resolve, reject) => {
      const request: GetUserRequestGRPC = { userId };
      const serializedRequest = this.serializeGetUserRequest(request);

      grpc.invoke('users.UserService/GetUser', {
        request: serializedRequest,
        host: this.host,
        onMessage: (message: Uint8Array) => {
          try {
            const user = this.deserializeUser(message);
            resolve(user);
          } catch (error) {
            reject(new Error('Failed to deserialize user response'));
          }
        },
        onEnd: (code: grpc.Code, message: string) => {
          if (code !== grpc.Code.OK) {
            reject(new Error(`gRPC error: ${code} - ${message}`));
          }
        },
      });
    });
  }

  async getUsers(): Promise<UserGRPC[]> {
    return new Promise((resolve, reject) => {
      // Пустой запрос для GetUsers
      const serializedRequest = new Uint8Array(0);

      grpc.invoke('users.UserService/GetUsers', {
        request: serializedRequest,
        host: this.host,
        onMessage: (message: Uint8Array) => {
          try {
            const users = this.deserializeUserList(message);
            resolve(users);
          } catch (error) {
            reject(new Error('Failed to deserialize users response'));
          }
        },
        onEnd: (code: grpc.Code, message: string) => {
          if (code !== grpc.Code.OK) {
            reject(new Error(`gRPC error: ${code} - ${message}`));
          }
        },
      });
    });
  }

  private deserializeUserList(data: Uint8Array): UserGRPC[] {
    const decoder = new TextDecoder();
    let offset = 0;
    const users: User[] = [];

    while (offset < data.length) {
      if (offset >= data.length) break;

      const tag = data[offset++];
      const fieldNumber = tag >> 3;
      const wireType = tag & 0x07;

      if (fieldNumber === 1 && wireType === 2) { // repeated users
        if (offset >= data.length) break;
        
        let length = data[offset++];
        // Обработка varint для длины
        if (length & 0x80) {
          length = (length & 0x7f) | ((data[offset++] & 0x7f) << 7);
        }

        if (offset + length > data.length) break;

        const userData = data.subarray(offset, offset + length);
        const user = this.deserializeUser(userData);
        users.push(user);
        offset += length;
      } else {
        // Пропускаем другие поля
        offset++;
      }
    }

    return users;
  }
}

export const userServiceClient = new UserServiceClient();