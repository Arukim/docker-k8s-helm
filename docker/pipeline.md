# Sample CI/CD pipeline for docker image

docker build -t ${service.name} -f ./src/${service.project}/Dockerfile ./src
docker tag ${service.name} ${registryRoot}/${service.name}:${tag}
docker login -u ${docker_username} -p '${docker_password}' ${registryUrl}
docker push ${registryRoot}/${service.name}:${tag}
