// services/signalRService.ts
import * as signalR from '@microsoft/signalr';
const API_BASE_URL ='http://localhost:5191';
export interface SignalRNotification {
  type: string;
  title: string;
  message: string;
  timestamp: string;
  projectId?: string;
}

class SignalRService {
  private connection: signalR.HubConnection | null = null;
  private connectionId: string = '';

  async startConnection(): Promise<boolean> {
    try {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(`${API_BASE_URL}/projectHub`, {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        })
        .withAutomaticReconnect([0, 2000, 5000, 10000])
        .build();

      // Обработчики событий подключения
      this.connection.onclose(() => {
        console.log('SignalR соединение закрыто');
      });

      this.connection.onreconnecting(() => {
        console.log('SignalR переподключение...');
      });

      this.connection.onreconnected(() => {
        console.log('SignalR переподключен');
      });

      await this.connection.start();
      
      // Получаем Connection ID
      this.connectionId = await this.getConnectionId();
      console.log('SignalR подключен. Connection ID:', this.connectionId);
      
      return true;
    } catch (err) {
      console.error('Ошибка подключения SignalR: ', err);
      return false;
    }
  }

  async getConnectionId(): Promise<string> {
    if (this.connection) {
      return await this.connection.invoke('GetConnectionId');
    }
    return '';
  }

  async joinAsAdmin(adminId: string): Promise<void> {
    if (!this.connection) {
      throw new Error('Соединение не установлено');
    }

    if (!adminId) {
      throw new Error('Admin ID обязателен');
    }

    await this.connection.invoke('JoinAsAdmin', adminId);
  }

  async joinAsUser(userName: string): Promise<void> {
    if (!this.connection) {
      throw new Error('Соединение не установлено');
    }
      console.log('SignalR. joinAsUser:', userName);
    if (!userName) {
      throw new Error('Имя пользователя обязательно');
    }

    await this.connection.invoke('JoinAsUser', userName);
  }

  async joinProject(projectId: string): Promise<void> {
    if (!this.connection) {
      throw new Error('Соединение не установлено');
    }
   console.log('SignalR. joinProject:', projectId);
    if (!projectId) {
      throw new Error('Project ID обязателен');
    }

    await this.connection.invoke('JoinProject', projectId);
  }

  async leaveProject(projectId: string): Promise<void> {
    if (!this.connection) {
      throw new Error('Соединение не установлено');
    }

    if (!projectId) {
      throw new Error('Project ID обязателен');
    }

    await this.connection.invoke('LeaveProject', projectId);
  }

  // Обработчики событий
  onReceiveNotification(callback: (notification: any) => void): void {
    if (this.connection) {
      console.log('signalR ProjectInvitation');
      this.connection.on('ReceiveNotification', callback);
    }
  }

  onProjectCreated(callback: (project: any) => void): void {
    if (this.connection) {
      console.log('signalR ProjectCreated');
      this.connection.on('ProjectCreated', callback);
    }
  }

  onParticipantsAdded(callback: (data: any) => void): void {
    if (this.connection) {
      console.log('signalR ParticipantsAdded');
      this.connection.on('ParticipantsAdded', callback);
    }
  }

  onReceiveConnectionId(callback: (connectionId: string) => void): void {
    if (this.connection) {
      console.log('ReceiveConnectionId');
      this.connection.on('ReceiveConnectionId', callback);
    }
  }

  onConfirmJoin(callback: (message: string) => void): void {
    if (this.connection) {
      this.connection.on('ConfirmJoin', callback);
    }
  }

  onUserStatusChanged(callback: (data: any) => void): void {
    if (this.connection) {
      this.connection.on('UserStatusChanged', callback);
    }
  }

  onReconnecting(callback: () => void): void {
    if (this.connection) {
      this.connection.onreconnecting(callback);
    }
  }

  onReconnected(callback: () => void): void {
    if (this.connection) {
      this.connection.onreconnected(callback);
    }
  }

  getConnectionIdValue(): string {
    return this.connectionId;
  }

  stopConnection(): void {
    if (this.connection) {
      this.connection.stop();
    }
  }
}

export const signalRService = new SignalRService();