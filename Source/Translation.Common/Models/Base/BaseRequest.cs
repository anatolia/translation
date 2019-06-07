namespace Translation.Common.Models.Base
{
    public abstract class BaseRequest
    {
        public abstract bool IsValid();

        public bool IsNotValid()
        {
            return !IsValid();
        }
    }
}