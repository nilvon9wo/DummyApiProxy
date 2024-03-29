import PropTypes from 'prop-types';
function ErrorAlert({message}) {
  return message ? <div style={{ color: 'red' }}>{message}</div> : null;
}

ErrorAlert.propTypes = {
  message: PropTypes.string.isRequired,
};

export default ErrorAlert;
