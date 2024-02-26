const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function(app) {
  // Just a change to check something
  app.use(
    '/api',
    createProxyMiddleware({
      target: 'http://localhost:5299',
      changeOrigin: true,
    })
  );
};