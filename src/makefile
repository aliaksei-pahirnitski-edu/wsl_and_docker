echo:
	echo "hello !!!"
	
image:
	echo "starting build image...."
	docker build -t wsl_and_docker_minapi:v1 ./minapi/
	echo "ended build image"
	
image-verbose:
	echo "starting build image...."
	docker build --progress=plain -t wsl_and_docker_minapi:v1 ./minapi/
	echo "ended build image"
	
run-v0:
	docker run -d -v D:/Temp/wsl_test/:/alex/  wsl_and_docker_minapi:v1

run-v1:
	docker run -d --name=alex_cont_wsl_docker  -l my_label -v D:\Temp\wsl_test:/alex/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1
	
run-v2:
	docker run -d --name=alex_v_wsl_docker -l my_label -v alexLogs:/alex/logs/ -v D:\Temp\not_existing:/alex/extra/ -v D:\Temp\wsl_test:/alex/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1
	
run:
	docker run -d --rm --name=alex_v_wsl_docker -l my_label -v alexLogs:/alex3/logs/ -v D:\Temp\not_existing:/alex3/extra/ -v D:\Temp\wsl_tst2\:/alex2/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1
		
run-it:
	docker run -it --rm --name=alex_v_wsl_docker -l my_label -v alexLogs:/alex3/logs/ -v D:\Temp\not_existing:/alex3/extra/ -v D:\Temp\wsl_tst2\:/alex2/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1
		
run-root:
	docker run -it --rm --name=alex_v_wsl_docker --user=root -l my_label -v alexLogs:/alex3/logs/ -v D:\Temp\not_existing:/alex3/extra/ -v D:\Temp\wsl_tst2\:/alex2/ -p 8080:8080 -p 8081:8081 -e ALEX_ENV=AL_VAL1 -e OTHER_ENV=Other_Val2 wsl_and_docker_minapi:v1
	
prune-image:
		docker image prune -f	
		
prune-all:
		docker builder prune -f
		docker container prune -f
		docker image prune -f