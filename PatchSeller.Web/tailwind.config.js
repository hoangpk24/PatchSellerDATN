/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Components/**/*.{razor,css,js}","./wwwroot/index.html"],
  theme: {
      extend: {
          screens: {
              xs: { max: '525px' },
              sm: { max: '768px' },
              md: { min: '769px', max: '1023px' },
              lg: { min: '1024px' },
          },
          colors: {
              "material-default": "#29A69A",
              "black-default": "#212121",
              "black-default2": "#242424",
              "black-layout": "#1A1A1A",
              "black-menu": "#0F0F0F",
              "purple-default": "#6D4EEC",
          }
      },
  },
  plugins: [],
}

