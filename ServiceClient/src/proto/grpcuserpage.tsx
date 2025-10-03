import React, { useState, useEffect } from "react";
import { UserServiceClient } from "../proto/UserServiceClient";
import { GetUserRequestGRPC, GetUsersRequestGRPC } from "./proto/users_pb";

const client = new UserServiceClient(); // URL вашего gRPC-Web сервера

function App() {
  const [user, setUser] = useState(null);
  const [users, setUsers] = useState([]);

  useEffect(() => {
    const request = new GetUserRequestGRPC();
    request.setUserId("1");

    client.getUser(request, {}, (err, response) => {
      if (err) {
        console.error(err);
        return;
      }
      setUser(response.toObject());
    });
  }, []);

  const fetchUsers = () => {
    const request = new GetUsersRequestGRPC();

    client.getUsers(request, {}, (err, response) => {
      if (err) {
        console.error(err);
        return;
      }
      setUsers(response.getUsersList().map((user) => user.toObject()));
    });
  };

  return (
    <div>
      <h1>User</h1>
      {user && (
        <div>
          <p>User ID: {user.userId}</p>
          <p>Email: {user.email}</p>
          <p>First Name: {user.firstName}</p>
          <p>Last Name: {user.lastName}</p>
          <p>Display Name: {user.displayName}</p>
        </div>
      )}

      <h1>Users</h1>
      <button onClick={fetchUsers}>Fetch Users</button>
      {users.map((u) => (
        <div key={u.userId}>
          <p>User ID: {u.userId}</p>
          <p>Email: {u.email}</p>
          <p>First Name: {u.firstName}</p>
          <p>Last Name: {u.lastName}</p>
          <p>Display Name: {u.displayName}</p>
        </div>
      ))}
    </div>
  );
}

export default App;
