using System;
using School.People.Core;
using System.Threading.Tasks;

namespace Cdln.School.People.Uwp
{
    public static class Extensions
    {
        public static string FullName(this IPerson person)
        {
            string nameExtension = person.NameExtension == null ? null : $", {person.NameExtension}";
            return $"{person.Title} {person.FirstName} {person.MiddleName} {person.LastName}{nameExtension}";
        }

        public static async void FireAndForget(this Task task, bool continueOnCapturedContext = true, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
