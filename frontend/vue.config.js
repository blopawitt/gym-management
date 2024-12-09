module.exports = {
  devServer: {
    proxy: {
      "/api": {
        target: "http://localhost:7032",
        changeOrigin: true,
      },
    },
  },
};
