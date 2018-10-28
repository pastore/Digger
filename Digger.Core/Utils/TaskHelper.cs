using System;
using System.Threading.Tasks;

namespace Digger.Core.Utils
{
    public  static class TaskHelper
    {
        public static Task Then(this Task task, Action<Task> next)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (next == null) throw new ArgumentNullException(nameof(next));

            var tcs = new TaskCompletionSource<Task>();

            task.ContinueWith(previousTask =>
            {
                if (previousTask.IsFaulted) tcs.TrySetException(previousTask.Exception ?? throw new InvalidOperationException());
                else if (previousTask.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(previousTask);
                        tcs.TrySetResult(default(Task));
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }
            });

            return tcs.Task;
        }
    }
}
