const config = {
  development: {
    apiUrl: 'http://localhost:7278',
  },
  test: {
    apiUrl: 'http://localhost:7278',
  },
  production: {
    apiUrl: 'https://your-production-api.com',
  },
};

export const getApiUrl = () => {
  const env = process.env.NODE_ENV || 'development';
  return config[env].apiUrl;
};
