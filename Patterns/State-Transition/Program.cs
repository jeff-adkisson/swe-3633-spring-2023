
//add 10 students
var context = new Context("SWE 3313 Section 1");
context.Do(Transition.Open);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);

//start over - cancel from Initialize
context = new Context("SWE 3363 Section 1");
context.Do(Transition.Cancel);

//start over, add 10 students, then cancel from Closed
context = new Context("SWE 3313 Section 1");
context.Do(Transition.Open);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.Cancel);

//start over - add three students, then cancel from Open
context = new Context("SWE 3363 Section 2");
context.Do(Transition.Open);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.Cancel);

//try to add 11 students
context = new Context("SWE 3313 Section 3");
context.Do(Transition.Open);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
context.Do(Transition.AddStudent);
try {
    //this will cause an error...
    context.Do(Transition.AddStudent);
} catch(Exception e) {
    Console.WriteLine($"{e.Message} (NOTE: This is expected behavior)");
}

public enum Transition {
    Open,
    AddStudent,
    Close,
    Cancel
}

public class Context {

    public Context(string courseName)
    {
        CourseName = courseName;
        CurrentState = new StateInitialization(this);
    }

    public string CourseName { get; private set; }
    
    public int StudentCount { get; set; }
    
    public IState CurrentState { get; private set; }

    public void Do(Transition transition) {
        CurrentState.Do(transition);
    }

    public void ChangeState(IState newState) => CurrentState = newState;

}

public interface IState {
    void Do(Transition registrationTransition);
}

public abstract class StateBase : IState {
    protected StateBase(Context context) {
        Context = context;
    }

    protected Context Context { get; init; }

    public abstract void Do(Transition transition);
}

public class StateInitialization : StateBase
{
    public StateInitialization(Context context) : base(context)
    {
    }

    public override void Do(Transition transition)
    {
        InitializeCourse();
        switch(transition) {
            case Transition.Open: 
                OpenCourse();
                break;
            case Transition.Cancel:
                CancelCourse();
                break;
            default:
                throw new ArgumentException($"{transition} is an invalid transition from {nameof(StateInitialization)}");
        }
    }

    private void InitializeCourse()
    {
        Console.WriteLine();
        Console.WriteLine($"Initializing {Context.CourseName} Course...");
    }

    private void OpenCourse()
    {
        Console.WriteLine($"Opening {Context.CourseName} Course... set student count to 0... changing state to Open...");
        Context.StudentCount = 0;
        Context.ChangeState(new StateOpen(Context));
    }

    private void CancelCourse()
    {
        Console.WriteLine($"Cancellation requested for {Context.CourseName} Course... changed state to Cancelled...");
        Context.ChangeState(new StateCancelled(Context));
    }
} 

public class StateOpen : StateBase {
    
    private const int CloseWhenStudentCountIs = 10;

    public StateOpen(Context context) : base(context)
    {
    }

    public override void Do(Transition transition) {
        switch(transition) {
            case Transition.AddStudent: 
                AddStudent();
                break;
            case Transition.Cancel:
                CancelCourse();
                break;
            default:
                throw new ArgumentException($"{transition} is an invalid transition from {nameof(StateOpen)}");
        }
    }

    private void AddStudent()
    {
        Context.StudentCount++;
        Console.WriteLine($"- Added student {Context.StudentCount} to {Context.CourseName} Course");

        if (Context.StudentCount == CloseWhenStudentCountIs) {
            CloseCourse();
        }
    }

    private void CloseCourse()
    {
        Console.WriteLine($"{CloseWhenStudentCountIs} students reached... Closing {Context.CourseName} Course... changing state to Closed");
        Context.ChangeState(new StateClosed(Context));
    }

    private void CancelCourse()
    {
        Console.WriteLine($"Cancellation requested for {Context.CourseName} Course... changed state to Cancelled...");
        Context.ChangeState(new StateCancelled(Context));
    }
}

public class StateCancelled : StateBase {
    public StateCancelled(Context context) : base(context)
    {
        NotifyRegisteredStudents();
    }

    private bool _isCancelled;

    public override void Do(Transition transition) { 
        //_isCancelled set via constructor call to NotifyRegisteredStudents
        if (_isCancelled) {
            throw new InvalidOperationException($"{Context.CourseName} Course is already cancelled.");
        }
    }

    private void NotifyRegisteredStudents() {
        _isCancelled = true;
        Console.WriteLine($"Cancelled {Context.CourseName} Course... Notifying { Context.StudentCount } registered students...");
    }
}

public class StateClosed : StateBase {
    public StateClosed(Context context) : base(context)
    {
        Do(Transition.Close);
    }

    public override void Do(Transition transition) {
        switch(transition) {
            case Transition.Cancel:
                CancelCourse();
                break;
            case Transition.Close:
                FinalizeCourse();
                break;
            default:
                throw new ArgumentException($"{transition} is an invalid transition from {nameof(StateClosed)}");
        }
     }

    private void FinalizeCourse()
    {
        Console.WriteLine($"Finalizing {Context.CourseName} Course...");
    }

    private void CancelCourse()
    {
        Console.WriteLine($"Cancellation requested for {Context.CourseName} Course... changed state to Cancelled...");
        Context.ChangeState(new StateCancelled(Context));
    }
}