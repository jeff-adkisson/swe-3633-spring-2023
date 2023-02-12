using StateMachine.MVC.Email;

namespace StateMachine.Tests;

public static class SampleTextContainer
{
    public static SampleText GetProjectDescription()
    {
        var actualMatches = new Match[]
        {
            new("Jeff-Adkisson@KSU.edu", 612),
            new("j.adkisson@KSU.edu", 638),
            new("JeffAdkisson@KSU.university", 661),
            new("Jeff-Adkisson@KSU.edu", 1944),
            new("j.adkisson@KSU.edu", 1977)
        };
        
        const string text = """
        SWE 1366 HONORS PROJECT

        1. Create an MVC web application using your preferred language and framework. I suggest picking something that is easy to deploy because you also have to document how to set it up and that can be elaborate for some of the popular choices out there.
           - The home page will show a multiline input box labeled "Paste block of text"
           - Submit the text block to a controller that launches a state machine to detect whether the block of multiline text has an email address in it matching the format [alphanumericPlusDotAndDash]@[alphanumeric].[2-10 letters, alphanumeric]. For example, Jeff-Adkisson@KSU.edu and j.adkisson@KSU.edu and JeffAdkisson@KSU.university. Jeff_Adkisson@KSU.com is invalid as Jeff@KSU and Jeff@KSU.u.
           
        2. Implement the State Machine Pattern within the web application. You must implement the pattern from first principles. You cannot use a pre-made state machine library.
           - The state machine will be finite (https://en.wikipedia.org/wiki/Finite-state_machine).
           - The state machine will traverse through the submitted text block letter by letter, changing state as appropriate.
           - The state machine will return an array of found email addresses on termination (0 to *) along with their zero-based starting index. For example, if an email address is found at the start of the block, its starting index will be 0.
           - Here is an example in JavaScript I wrote in grad school for Theory of Automation that does text matching.
              https://jadkisson.github.io/dfa/
              You do not need to build a UI (though that is awesome and makes for a great demo).
           
        4. When the state machine has finished finding the email addresses, if any, return the results to the browser and render the findings. The results include the starting index of each email address and the email address. Also include a button to start a fresh search. For example:

            Found 2 Email Addresses:
            1. Jeff-Adkisson@KSU.edu, 0
            2. j.adkisson@KSU.edu, 99
        
            [Search Another?]

        --------------------
        REQUIREMENTS

        1. Build a small MVC server to host your state machine and produce a simple UI to submit a block of multiline text from a browser to a controller. You can use an existing server framework and language of your choice. Choose carefully - some are more involved than others both to implement and to configure/deploy.

        2. The controller will start a state machine whose final state returns the email addresses (if any) found in the text block. A matching email address follows this format: [alphanumericPlusDotAndDash]@[alphanumeric].[2-10 letters, alphanumeric].

        3. The text block can be between 0 and N characters and may have line breaks and other symbols in it. For example, I might pass in a block of code that contains an email address in it.

        3. You CANNOT use a regex or other text-matching function in the state machine. Detection is letter by letter only. The only place you can use those techniques is in unit tests (if you make them, recommended, not required) to test your work.

        4. The state machine will traverse the block of text letter by letter. Anytime an email address is found, record the entire address and the start index of the address.

        5. After the state machine has finished, return the array of found addresses to the browser. List the number found and their start index in the block.

        6. Post your solution to a public GitHub repo.

        7. Your solution must include a README.md that includes how to setup and execute the project, example output, and a brief description of what it does. If your MVC requires an elaborate configuration, you must document that (I have to be able to run it). Note that I recommend picking a server application that is easy to deploy for this reason.

        8. All code must be well written, well organized (think cohesion, coupling, cyclomatic complexity, readable, etc.) and professional grade. Comment as appropriate.

        9. This project will be performed individually - not in teams.

        --------------------
        RUBRIC

        1. Was the MVC pattern properly implemented on the server? 10%

        2. Does the UI accept a multiline block of text? 5%

        3. Does the UI show all of the email addresses found and their starting index? 10%

        4. Does the state machine find valid email addresses and ignore all else? 50%

        5. Was the code quality professional grade? 5%

        6. Was the project documented in the README.md? 10%

        7. Was the code easily executed from a GitHub repo? 10%

        --------------------
        PASSING

        1. Every line of the rubric must receive a score greater than 0%, and
        2. You must have a total of 80% or higher for a passing score.

        --------------------
        ASSISTANCE

        This is an honors project and therefore will require you to work out the solution on your own. I can clarify requirements or review specific issues you might have, but I cannot provide coding assistance. The topics covered in this project are covered in some form later in the semester. I do not advise waiting for that. Also, this project goes deeper than the course will on state machines, so you might need to do some research.
        """;

        return new SampleText(text, actualMatches);
    }
}