DOCKERFILE_DATA_DIR:= Data/Dockerfile
DOCKERCONTEXT_DATA_DIR:= Data

DOCKERFILE_COMMANDS_DIR:= Docker.Commands/Dockerfile
DOCKERCONTEXT_COMMANDS_DIR:= Docker.Commands

DOCKERFILE_SESSION_DIR:= Docker.Session/Dockerfile
DOCKERCONTEXT_SESSION_DIR:= Docker.Session

.PHONY: docker-build-all
docker-build-all:
	docker build -t data-docker-environment:latest -f ${DOCKERFILE_DATA_DIR} ${DOCKERCONTEXT_DATA_DIR}

	docker build -t docker-commands:latest -f ${DOCKERFILE_COMMANDS_DIR} ${DOCKERCONTEXT_COMMANDS_DIR}

	docker build -t docker-session:latest -f ${DOCKERFILE_SESSION_DIR} ${DOCKERCONTEXT_SESSION_DIR}

.PHONY: docker-run-all
docker-run-all:
	

	docker run -d \
	-p 5432:5432 \
	--name docker-db \
	-e POSTGRES_PASSWORD=postgres \
	-e POSTGRES_USER=postgres \
	data-docker-environment:latest

	docker run -d \
	--name docker-session \
	-p 54099:8080 \
	docker-session:latest