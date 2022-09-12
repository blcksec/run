ifdef DOCKER_USER
	NAME ?= ${DOCKER_USER}/runner-devcontainer
else
	NAME ?= fdev321123/runner-devcontainer
endif

VERSION ?= latest

docker-build:
	docker build \
		-t "${NAME}:${VERSION}" \
		-f base.Dockerfile \
		. \

docker-push:
	make docker-build
	docker push ${NAME}:${VERSION}
