using System;

namespace OrchardProject.ViewModels
{
    public class ValidatorResponse
    {
        public string ValidationMessage;
        public bool HasErrors;
        public string[] Errors;
    }

    public class Validator
    {
        private string _propertyName;

        public Validator(string propertyName)
        {
            _propertyName = propertyName;
        }
    }
}