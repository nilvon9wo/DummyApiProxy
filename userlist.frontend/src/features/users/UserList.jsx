import ErrorAlert from '../../atoms/ErrorAlert';
import Header from '../../atoms/Header';
import {useState, useEffect} from 'react';
import User from '../users/UserItem';
import { getApiUrl } from '../../../config';

function UserList() {
    const [users, setUsers] = useState([]);
    const [error, setError] = useState(null); 

  useEffect(() => {
    const baseUrl = getApiUrl();
    fetch(`${baseUrl}/api/v1/users`)
        .then((response) => {
            if (!response.ok) {
              throw new Error('Failed to fetch users');
            }
            return response.json();
          })
        .then((data) => {
            setUsers(data.users);
            setError(null);
          })
        .catch((error) => {
            console.error('Error fetching data:', error);
            setError(error.message); 
          });
  }, []);

  return (
    <div>
          <Header text="User List" />
          <ErrorAlert message={error} />
      <ul>
        {users.map((user, index) => (
          <User key={index} user={user} />
        ))}
      </ul>
    </div>
  );
}

export default UserList;
