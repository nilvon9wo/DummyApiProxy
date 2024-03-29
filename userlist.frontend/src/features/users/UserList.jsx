import Header from '../../atoms/Header';
import {useState, useEffect} from 'react';
import User from '../users/UserItem';

function UserList() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    fetch('http://localhost:7278/api/v1/users')
        .then((response) => response.json())
        .then((data) => setUsers(data.users))
        .catch((error) => console.error('Error fetching data:', error));
  }, []);

  return (
    <div>
      <Header text="User List" />
      <ul>
        {users.map((user, index) => (
          <User key={index} user={user} />
        ))}
      </ul>
    </div>
  );
}

export default UserList;
