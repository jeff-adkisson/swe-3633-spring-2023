# Example Solution for SWE 1366 Honors Project
### MVC & State Machine Solution to Extract Emails From Text Blocks

Create an MVC application that:
- accepts a multiline text block in a simple browser UI,
- posts the block to a controller,
- launches a state machine to detect simple email addresses in the block,
- returns the email addresses and their start indexes to the client, and
- renders the results in the browser.

# To Execute

1. If needed, install the dotnet framework: https://dotnet.microsoft.com/download
2. Open a terminal.
3. Navigate to the `StateMachine\StateMachine.Mvc` project directory.
4. Execute the following command: `dotnet watch`. This will start the application _and_ launch a browser window.
5. Paste a block of text containing valid and invalid email addresses (per the project guidelines) into the text box, then click `Find Email Addresses`.

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

# State Machine Diagram 

![State Machine Diagram](http://www.plantuml.com/plantuml/svg/bPDHIyCm58NVyolEix0nVNCFig4J1cE24mJd7c9pwyBIt98cS_hhRLg7hN68J-EEyvtxNKWdbPVEXyA8zTa6Wy4LLl6Fk_oHdS8OzvIHtclxYXV3sGwSOxfSpUVmTF12zCCBvXB1UcsmbX_Jfz7bOBX1pi4gi4F6HMd8Ku-AAwzbwOEZfIoeRhbjydjfj4AsbIw9HzCnbeo27JMH2fvHEiwegeJerRLIRrjjw5UfILFGPRfADrn8ROzIfy9TmMDKsyu5ucTTAjLH6hPGZ-bAFN2ve3sPljnfx6yzaJIddXVmZFFHjsz4gzOwuV-n_3GwLMfM3wR-TLHLDJwhOSxXkOI9OQWstKg818WLa5-JhE7cjKDRHOTuJiOslcDmy6NmaCx9zwGbkQATO9XtySF8sJrR6sieOiTzOa9MXS9y0W00)

# State Machine Class Diagram

![State Machine Class Diagram](https://www.plantuml.com/plantuml/svg/jLJBYjj04BphA_gucx0_O8IoYR89XcGJi267aCDurBO6HjDYr3A6ZVyzhV6C7xBm9TW7FRfALTLbeUUEbUU-iPd5DGCJUBEf6IhZKRCXbschkWvASemxXhSCvFS-tJyCXoKjj7ApZhrncm-FaV5TiQwnWndCNrpVe5ShYtcO5f3d6-IYYDHvLrBJMcGKzeh8Zl2oznuT_wJE3964P1mdZfoxQsvPAwyqenUobkkHuiUg2aaU7WNVagcEywro6fFJ65uWH_t5KDlSLRWxSVPPBEkB4M2mb7BPQCcOQdixJFQ14ifBjOdAeRBVVqJ84ICmjK3hg2Rm8ZmBDTeMlQ20EzIz40THX4RfsZXC8OBOqVYbKSRArXu5ci71T5JAQQVBXYLFpF3kwUcWcsM2eryhZJ0VSc5q-ehyAbk5qT1CHBDzJ1Nk9CT5dXHo2dpGv49FyOrBhrUUqnQzDnKMnGtNSFwaEeJyhuIVSiZKIkfGcY55J8yS_xN4sDzm2XV99acSRrsgbdkFdqcyNTAUWEwmyON-HAPNrPos7GFtooFgKmqjgVs6MxGtcagKUpUMVAwRauFd7-jVvAkKy0hulrpdRl6xjAI1ElASWk_GoDbUOtmwG_QChkeR-my0)

## State Machine PlantUML

```plantuml
@startuml
[*] --> StartOfWord : Receiving block of ANSII text

StartOfWord : Loop until [azAZ09-.] found or complete
StartOfWord --> CaptureName : Found valid char
StartOfWord --> [*] : No more chars to process

CaptureName : Loop while [azAZ09-.] found or @ char
CaptureName --> CaptureDomain : Found @ char
CaptureName --> StartOfWord : Found invalid char

CaptureDomain : Loop while [azAZ09] found or . char
CaptureDomain --> CaptureTopLevelDomain : Found . char
CaptureDomain --> StartOfWord : Found invalid char


CaptureTopLevelDomain : Loop while 2 to 10 [azAZ09] chars found
CaptureTopLevelDomain --> StartOfWord : Found invalid char
CaptureTopLevelDomain --> AddEmailAddress : End of top level domain found

AddEmailAddress : Add address and start index to context output array
AddEmailAddress --> StartOfWord : Start looking for\nnext email address
@enduml

```