# Docker Task #3

## Setup Docker-Compose

### Using as development environment

- View contents of docker-compose.yaml
- Run dev environment 
  - $docker-compose up
- Use docker container from task 2 to test it

### Using as test / prod environment

- Add new section to config 
  webAdmin:
    image: dnatrack/web/admin
    environment:
      "Rabbit:Endpoint": "rabbitmq://rmq:5672"
      "Rabbit:Username": "rabbitmq"
      "Rabbit:Password": "rabbitmq"
    ports:
      - "8000:80"
- Run using the $docker-compose up
- Service mode
  - Run using the $docker-compose up -d
  - restart: always

### Adding configuration

- Modify web service configuration, adding
volumes:
    - path_to_config:/etc/config/appsettings.json:ro


## Now create a docker-compose for Solution

- Create Dockerfile for
	- Services.Analysis
	- Web.Client
	- Jobs.Validator
- In docker-compose
	- Services.Analysis
	- Web.Client
- Separate docker container
	- Jobs.Validator


