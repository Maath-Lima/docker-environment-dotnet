# Dump build stage
FROM postgres:16.1-alpine3.19 as dumper

COPY DockerEnvironmentDB.sql /docker-entrypoint-initdb.d/

# Replace command
RUN ["sed", "-i", "s/exec \"$@\"/echo \"skipping...\"/", "/usr/local/bin/docker-entrypoint.sh"]

ENV PG_USER=postgres
ENV POSTGRES_PASSWORD=postgres
ENV PGDATA=/data

RUN ["/usr/local/bin/docker-entrypoint.sh", "postgres"]

# Final build stage
FROM postgres:16.1-alpine3.19

COPY --from=dumper /data $PGDATA