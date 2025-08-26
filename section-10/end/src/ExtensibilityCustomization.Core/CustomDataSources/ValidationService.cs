namespace ExtensibilityCustomization.Core.CustomDataSources;

public class ValidationService
{
    public string ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "Input cannot be empty";

        if (input.Length < 3)
            return "Input too short";

        if (input.Contains("invalid"))
            return "Input contains invalid content";

        return "Valid";
    }

    public bool IsEmailValid(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        return email.Contains("@") && email.Contains(".");
    }
}
