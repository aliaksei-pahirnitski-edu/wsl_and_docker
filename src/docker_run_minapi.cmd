echo "mine minapi wsl_and_docker_minapi:v1 run.."
docker run -d --name=alex_cont_wsl_docker -v D:\Temp\wsl_test:/alex/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 -l my_label wsl_and_docker_minapi:v1

rem docker run -d -v D:/Temp/wsl_test/:/alex/   wsl_and_docker_minapi:v1
