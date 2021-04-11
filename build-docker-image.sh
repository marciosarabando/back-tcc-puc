docker build -t tcc.backend .
docker run --name tcc.backend -d -p 5005:80 tcc.backend