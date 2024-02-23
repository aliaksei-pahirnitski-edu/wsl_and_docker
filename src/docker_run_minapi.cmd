echo "mine minapi wsl_and_docker_minapi:v1 run.."
rem v1 docker run -d --name=alex_cont_wsl_docker  -l my_label -v D:\Temp\wsl_test:/alex/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1

docker run -d --rm --name=alex_v_wsl_docker -l my_label -v alexLogs:/alex3/logs/ -v D:\Temp\not_existing:/alex3/extra/ -v D:\Temp\wsl_tst2\:/alex2/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1


rem v0 docker run -d -v D:/Temp/wsl_test/:/alex/  wsl_and_docker_minapi:v1
