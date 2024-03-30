import PropTypes from 'prop-types';
function Header({text}) {
  if (!text) {
    throw new Error('text prop is required');
  }

  return <h1>{text}</h1>;
}
Header.propTypes = {
  text: PropTypes.string.isRequired,
};

export default Header;