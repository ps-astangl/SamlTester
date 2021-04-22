# password = password
openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout security.pem -out security.crt -subj "/CN=ourdomain.com" -days 3650
openssl pkcs12 -export -out security.pfx -inkey security.pem -in security.crt