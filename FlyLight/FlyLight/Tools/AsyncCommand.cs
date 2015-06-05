using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FlyLight.Tools;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync(object parameter);
}

public abstract class AsyncCommandBase : IAsyncCommand
{
    public abstract bool CanExecute(object parameter);
    public abstract Task ExecuteAsync(object parameter);
    public async void Execute(object parameter)
    {
        await ExecuteAsync(parameter);
    }

    public event EventHandler CanExecuteChanged;
}

public class AsyncCommand<TResult> : AsyncCommandBase
{
    private readonly Func<Task<TResult>> _command;

    public AsyncCommand(Func<Task<TResult>> command)
    {
        _command = command;
    }

    public override bool CanExecute(object parameter)
    {
        return true;
    }

    public override Task ExecuteAsync(object parameter)
    {
        Execution = new NotifyTaskCompletion<TResult>(_command());
        return Execution.TaskCompletion;
    }

    // Raises PropertyChanged
    public NotifyTaskCompletion<TResult> Execution { get; private set; }

}