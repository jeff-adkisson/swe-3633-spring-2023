# Example Solution for SWE 1366 Honors Project
### MVC & State Machine Solution to Extract Emails From Text Blocks

Create an MVC application that:
- accepts a multiline text block in a simple browser UI,
- posts the block to a controller,
- launches a state machine to detect simple email addresses in the block,
- returns the email addresses and their start indexes to the client, and
- renders the results in the browser.

# To Execute MVC Solution

1. If needed, install the dotnet framework: https://dotnet.microsoft.com/download
2. Open a terminal.
3. Navigate to the `StateMachine\src\StateMachine.Mvc` project directory.
4. Execute the following command: `dotnet watch`. This will start the application _and_ launch a browser window.
5. Paste a block of text containing valid and invalid email addresses (per the project guidelines) into the text box, then click `Find Email Addresses`.

# To View Swagger API

1. Start the application.
2. Navigate to the '/swagger' endpoint.

# Example Input

```
Valid Examples
1. Jeff-Adkisson@KSU.edu 
2. j.adkisson@KSU.edu
3. JeffAdkisson@KSU.university

Invalid Examples
1. Jeff_Adkisson@KSU.com [underscore not allowed]
2. Jeff@KSU [top level domain is required]
3. Jeff@KSU.u [top level domain must be 2-10 letters, alphanumeric]
4. JeffAdkisson@KSU.universitymail [top level domain must be 2-10 letters, alphanumeric]
5. JeffAdkisson@K_S_U.university [underscore not allowed]
```

# Example Output

``` 
Found 3 Valid Email Addresses:
 1. Jeff-Adkisson@KSU.edu, 19
 2. j.adkisson@KSU.edu, 46
 3. JeffAdkisson@KSU.university, 69
```

# Calling the State Machine

```csharp
var matches = Email.Finder.Find(model.TextToScan);
```

# State Machine Diagram 

![State Machine Diagram](http://www.plantuml.com/plantuml/svg/ZPFDJW8n4CVlVOevcq3GcnmC134X8PY8YOd0eMvdPKEti-bs4NrwErqt52hH4nJ-7x_fnwXsqhgcr69rjO5UxmwM_CDZ_a9MmH2UCCDYNvXtUDEKxO1o62-MinauF3WXJiLpeWeQumeDA_arVXtSzleRoAan2iX2HcMbqM7i2fKJMRd6uaAM24Du2Fgzr8M2R2jjAczpMBiWACbYqDNW22fB6TQr475c68FTOLWeZCB3sWJQZssXyLVQKLiUvSIizrJAmdIqbyGHUIVrxT5euZmkXOgG-b5BvucXdgcQunxr6Tn5szzuQN22USFRVZquuhPd4Q1J__-xBrl7IatzGljFwy_QswUyFxa7gK2p65IhRY54Ql3VGFwif3cv63nEHeRlDr3ZgiQ1j5P-9Ydf5645ccZ73oOdkpQ6Ov1jNPyGtVsDm5bbKMPRLCU_kO1VvH0cBK_WydFLwtQZkk20DqAZcb9_0m00)

# State Machine Class Diagram

![State Machine Class Diagram](http://www.plantuml.com/plantuml/svg/fPH1ZzCm48Nl-HLpJ45RxJagOLUbe4fGLceb3i31jQUDHM9NzgHqMVQ_CqdiR1HLQANqqDOpp-zxyLYvyoGSrLMPb7WW80jE7mi2fHrAqjOa_fSs9-541R8KtjyJERsl2TVIcgt-Wt2R96q7bjOGNWYU4k3FsxXEcw-TtKfXCi-RpV7zB2Y-2985UiXWlJ9rXKxiIroqrHmMYoDIdiCxCFWxZLlMVcchKuc4l6jlRHbr4LexXuQM1KUMX8vLal-FLFTAjPHqnlsprQglduMH-62zRXv-tDuY1VzjSsnhe_3oedcE1x71IoCWPAhdNotpl3-Lfwxo39-G5hjUcgTKQ0_hTf4r6cJZi6PhOmc1x0diOU2VmUf2Q3ndXGgfi0fgZmhsZs7dd2czF9tzCCs4aMfdW0e4moreOJ7xHsqKkly5RoNjKFjnGjQDyllFKTBEhOs9iYRq7Gmg8Gjj48HH8Oxl740u9nwxCB4K0wmgeKkUd4DkPT31HuDzcGihjA7OvbmXoyrDiCtWTZv_aoIiGdSG4iEH1Tw3iv91gUnkSHPkyt0e8MMxubp3V37qgsoI05xydS-lkWr1It6Ysk47otqp2JFokWBPY0gd8wBJ5S3EdZxZ6Sl-QIQJHfP3PditlXw-MQUcFTJGQGW8Ru-TtR35T0p6Drra36iJhAz6JUxGgBeg_m40)

## State Machine PlantUML

```plantuml
@startuml
[*] --> StartOfWord : Receiving block of ASCII text

StartOfWord : Loop until [azAZ09-.] found or complete
StartOfWord --> CaptureName : Found valid char
StartOfWord --> Complete : No more chars to process

CaptureName : Record start index\nLoop while [azAZ09-.] found or @ char
CaptureName --> CaptureDomain : Found @ char
CaptureName --> StartOfWord : Found invalid char

CaptureDomain : Loop while [azAZ09] found or . char
CaptureDomain --> CaptureTopLevelDomain : Found . char
CaptureDomain --> StartOfWord : Found invalid char

CaptureTopLevelDomain : Loop while 2 to 10 [azAZ09] chars found
CaptureTopLevelDomain --> StartOfWord : Found invalid char
CaptureTopLevelDomain --> AddEmailAddress : End of top level domain found

AddEmailAddress : Add address and start index to context output array
AddEmailAddress --> StartOfWord : Start looking for \n next email address

Complete : End of text reached
Complete --> [*] : Context contains \n valid email array
@enduml

```