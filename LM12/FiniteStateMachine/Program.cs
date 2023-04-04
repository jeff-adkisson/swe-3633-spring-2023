using System.Text.RegularExpressions;

while (true) //loop until empty input is received
{
    Console.WriteLine();
    var input = GetInput();
    if (string.IsNullOrWhiteSpace(input)) return;
    Execute(input);
}

string GetInput()
{
    while (true) //loop until valid input is received
    {
        Console.WriteLine("Enter an even-length string of 0's and 1's (empty input to exit):");
        Console.Write("> ");
        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input)) return ""; //stop
        
        if (!Regex.IsMatch(input, "^[01]+$"))
        {
            Console.WriteLine($"ERROR: Input can only contain 0's and 1's{Environment.NewLine}");
            continue;
        }

        if (input.Length % 2 == 0) return input;
        
        Console.WriteLine($"ERROR: Input must be an even-length string{Environment.NewLine}");
    }
}

State ChangeState(char currentChar, State newState, string remainingInput)
{
    Console.WriteLine(
        $"Character: {currentChar}, " +
        $"Next State: {newState}, " +
        $"Remaining Input: {remainingInput}");
    return newState;
}

void Execute(string input)
{
    var originalInput = input;
    var currentState = State.S1; //initial state
    while (!string.IsNullOrWhiteSpace(input))
    {
        var ele = input[0]; //first element from the string
        input = input.Length > 0 ? input[1..] : ""; //delete first element from the string
        currentState = currentState switch
        {
            State.S1 when ele == '0' => ChangeState(ele, State.S2, input),
            State.S1 when ele == '1' => ChangeState(ele, State.S4, input),
            State.S2 when ele == '0' => ChangeState(ele, State.S1, input),
            State.S2 when ele == '1' => ChangeState(ele, State.S3, input),
            State.S3 when ele == '0' => ChangeState(ele, State.S4, input),
            State.S3 when ele == '1' => ChangeState(ele, State.S2, input),
            State.S4 when ele == '0' => ChangeState(ele, State.S3, input),
            State.S4 when ele == '1' => ChangeState(ele, State.S1, input),
            _ => throw new Exception("Invalid input")
        };
    }

    ShowAcceptance(currentState, originalInput);
}

void ShowAcceptance(State finalState, string originalInput)
{
    Console.WriteLine();
    if (finalState == State.S1)
    {
        Console.WriteLine(
            $"SUCCESS: Input accepted (ended in state {State.S1}): {originalInput}");
        return;
    }

    Console.WriteLine(
        $"REJECTED: Input rejected (ended in state {finalState}, " +
        $"must end in state {State.S1} for acceptance): {originalInput}");
}

internal enum State
{
    S1,
    S2,
    S3,
    S4
}