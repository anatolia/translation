namespace Translation.Common.Enumerations
{
    public class ResponseStatus : Enumeration
    {
        public static readonly ResponseStatus Unknown = new ResponseStatus(0, "Unknown", "not_informed_reason");
        public static readonly ResponseStatus Success = new ResponseStatus(1, "Success", "worked_successfully");
        public static readonly ResponseStatus Failed = new ResponseStatus(2, "Failed", "request_failed");
        public static readonly ResponseStatus Invalid = new ResponseStatus(3, "Invalid", "request_not_valid");

        private ResponseStatus(int value, string displayName, string description) : base(value, displayName, description) { }

        public bool IsSuccess { get { return Value == Success.Value && DisplayName == Success.DisplayName; } }
        public bool IsNotSuccess { get { return !IsSuccess; } }
    }
}