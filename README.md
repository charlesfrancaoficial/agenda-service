
# Agenda Service

This is a .net core microservice example that can be uses as an appointment service. It creates a couple of api's and background services that can be used to send notifications, schedule a service appointment and much more.

## Tech Stack

**Client:** React, Redux, TailwindCSS

**Server:** .Net core, Hangfire, RabbitMq


## Features

- Restfull Api's to manage appointments
- Background jobs to send notifications and comunicate to other services
- Queue comunication
- Cross platform


## Run Locally
You gonna need [docker desktop](https://www.docker.com/products/docker-desktop) installed or change the connection strings to be able to run it.

Assuming you have docker installed, let's go to the step by step to run this application.

Clone the project

```bash
  git clone https://github.com/charlesfrancaoficial/agenda-service.git
```

Go to the project root directory and run docker compose to start the database, kibana, RabbitMQ and the SqlServer

```bash
  docker-compose up -d
```

After that if you go to the docker desktop app and switch to the containers tab, you should see the main services started

![App Screenshot](https://github.com/charlesfrancaoficial/agenda-service/blob/master/assets/docker-services.png?raw=true)

Now, on your terminal, go to the project src directory and run the following command without the "-d" flag

```bash
  docker-compose up
```

After having all the services running you can access the services url's and see it working

For the Agenda Service API you can open the address: [http://localhost:5001/swagger/index.html](http://localhost:5001/swagger/index.html)

![App Screenshot](https://github.com/charlesfrancaoficial/agenda-service/blob/master/assets/swagger-index.png?raw=true)

The project includes database Seeding for a first run without needing any configurations, so you can try the Appointments get method directly on the swagger documentation

![App Screenshot](https://github.com/charlesfrancaoficial/agenda-service/blob/master/assets/swagger-appointments.png?raw=true)


The service also exposes a working hangfire server at this address: [https://localhost:5004/hangfire](https://localhost:5004/hangfire)

![App Screenshot](https://github.com/charlesfrancaoficial/agenda-service/blob/master/assets/hangfire.png?raw=true)
