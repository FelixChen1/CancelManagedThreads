using System;

namespace CancelableObjectPattern
{
    public class CancelableObject
    {
        public string id;

        public CancelableObject(string id)
        {
            this.id = id;
        }

        public void Cancel()
        {
            Console.WriteLine("Object {0} Cancel action.", id);
            // Perform object cancellation here.
        }
    }
}