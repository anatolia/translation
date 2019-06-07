using System.Collections.Generic;

namespace Translation.Client.Web.Models
{
    public abstract class BaseModel
    {
        public string Title { get; set; }

        public List<string> ErrorMessages { get; set; }

        protected BaseModel()
        {
            ErrorMessages = new List<string>();
        }

        public virtual void SetInputModelValues()
        {

        }

        public virtual void SetInputErrorMessages()
        {

        }

        public bool Validate()
        {
            SetInputErrorMessages();

            if (ErrorMessages.Count > 0)
            {
                SetInputModelValues();
            }

            return ErrorMessages.Count == 0;
        }
    }
}