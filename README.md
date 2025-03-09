# Task Management System with RabbitMQ

This system allows the management of tasks through various operations, leveraging RabbitMQ for message handling and communication.

## Features:
- **Add New Tasks**: Allows users to create new tasks within the system.
- **Update Task Status**: Enables users to update the status of existing tasks.
- **View Tasks**: Provides functionality to view the list of tasks.

## ServiceBusHandler Class

The `ServiceBusHandler` class is responsible for managing interactions with the service bus, ensuring that messages are handled reliably.

### Methods:

- **SendMessage**:  
   Serializes an object and sends it as a message to the service bus for processing.
   
- **ReceiveMessage**:  
   Listens for incoming messages on the service bus, deserializes received messages, and triggers the appropriate actions within the system.

- **Event Notification**:  
   Sends an event to notify other components upon the completion of an action, ensuring other parts of the system are informed of updates.

## Arhitecture
![image](https://github.com/user-attachments/assets/8561fc34-b1e5-456b-9774-4e025b753b92)


## Application Flow
![image](https://github.com/user-attachments/assets/52647709-5494-47f5-995a-8da37c7aea6c)

## Project Notes

- **TaskManagement** project is running on [localhost:5248 (HTTP)](http://localhost:5248/swagger/index.html) for API access.
- **RabbitMQ** is available at [http://localhost:15672/](http://localhost:15672/) for message queue management.

Make sure to set up **CommandService** and **TaskManagement** services to start up together for proper operation.

## Technologies Used:
- **Programming Language**: C#
- **Service Bus**: RabbitMQ
- **Database**: Microsoft SQL Server
