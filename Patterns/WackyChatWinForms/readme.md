# Wacky Chat
Wacky Chat is a [Winforms](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/?view=netdesktop-6.0) application to demonstrate event driven development using a message bus.

The application sends chat messages to everyone connected. The back end is a free instance of a shared [RabbitMQ](https://www.rabbitmq.com/) message bus hosted by [CloudAMQP](https://www.cloudamqp.com/).

To run from a Windows machine (or Windows virtual):

1. Open a terminal and run `git clone https://github.com/jeff-adkisson/swe_3313_fall_2022.git`
	
	*Note: If you already have the repo, just switch to the root directory and run `git pull` to retrieve the latest changes.*
	
	*Note 2: You must have [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed to run Wacky Chat.
	   
2. Switch to the `LectureDemos\WackyChat` directory.
    
3. Execute `dotnet run`:
   
   ![UI](https://raw.githubusercontent.com/jeff-adkisson/swe_3313_fall_2022/main/LectureDemos/WackyChat/Diagrams/WackyChatUi.png) 

4. Type your message and press Enter. Anyone connected to the Wacky Chat message bus will see the message.

![UI](https://raw.githubusercontent.com/jeff-adkisson/swe_3313_fall_2022/main/LectureDemos/WackyChat/Diagrams/EventDrivenUi.png) 

![UI](https://raw.githubusercontent.com/jeff-adkisson/swe_3313_fall_2022/main/LectureDemos/WackyChat/Diagrams/EventDrivenViaMessageBus.png) 

![UI](https://raw.githubusercontent.com/jeff-adkisson/swe_3313_fall_2022/main/LectureDemos/WackyChat/Diagrams/CloudAMPQFreeInstance.png) 
