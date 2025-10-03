// Generated TypeScript interfaces from Protobuf schema
// Install required packages: npm install @grpc/grpc-js @grpc/proto-loader




export interface GetUserRequestGRPC {
  user_id: string;
}

export interface UsersResponseGRPC {
  users: UserResponseGRPC[];
}

export interface UserResponseGRPC {
  user_id: string;
  email: string;
  first_name: string;
  last_name: string;
  display_name: string;
}


// Utility types for gRPC-Web integration
export type GrpcWebMethodDescriptor<TRequest, TResponse> = {
  readonly methodName: string;
  readonly service: any;
  readonly requestStream: boolean;
  readonly responseStream: boolean;
  readonly requestType: new () => TRequest;
  readonly responseType: new () => TResponse;
};

 

// Example usage with gRPC-Web
// export class UserServiceClient {
//   private serviceUrl: string;

//   constructor(serviceUrl: string) {
//     this.serviceUrl = serviceUrl;
//   }

//   async getUser(request: { id: number }): Promise<UserResponseGRPC> {
//     // Implementation would make gRPC call
//     const mockResponse: UserResponseGRPC = {
//       user: {
//         id: request.id,
//         name: "John Doe",
//         email: "john@example.com",
//         roles: ["user"],
//         active: true,
//         created_at: Date.now(),
//         contact: {
//           phone: "+1-555-0123",
//           address: "123 Main St",
//           city: "San Francisco",
//           country: "USA"
//         },
//         permissions: []
//       },
//       message: "User retrieved successfully",
//       status_code: 200
//     };
//     return mockResponse;
//   }
// }

