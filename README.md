## What is this project?
This is an example of comunication between microservices using message queue, where I used MassTransit and RabbitMQ

## What the app does?
Microservice1 is listening messages (ICreateBetCommand) from the "create_bet" queue.  
Microservice2 (ConsoleApp1) produce messages(ICreateBetCommand).  
When you insert a new message in the queue with Microservice2, this new message is going to be intercepted (read) by the "CreateBetConsumer" at Microservice1.  

Just it. :)


## How to use:
- You will need .Net CORE 2.2 and docker 
- Before run the projects, run RabbitMQ container with docker

	docker run -p 5672:5672 -p 15672:15672 rabbitmq:3-management
	http://localhost:15672/#/
	user: guest
	passw: guest


## Contact:
[Diego Tavares Ferreira](https://www.linkedin.com/in/diego-tavares-ferreira/)

