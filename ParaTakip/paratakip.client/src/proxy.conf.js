const PROXY_CONFIG = [
  {
    context: [
      "/currency/get",

      "/exchangerate/get",
      "/exchangerate/load",

      "/appuser/login",
      "/appuser/register",

      "/wealth/get",
      "/wealth/update"
    ],
    target: "https://localhost:7052",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
