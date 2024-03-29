import PropTypes from 'prop-types';

function UserItem({user}) {
  return <li>{user.firstName} {user.lastName}</li>;
}

UserItem.propTypes = {
  user: PropTypes.shape({
    firstName: PropTypes.string.isRequired,
    lastName: PropTypes.string.isRequired,
  }).isRequired,
};

export default UserItem;