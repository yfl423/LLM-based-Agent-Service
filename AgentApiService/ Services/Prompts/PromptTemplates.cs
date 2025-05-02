namespace AgentApiService.Prompts;

public static class PromptTemplates
{
    public static readonly string SystemIntro = 
        "You are an AI assistant for a telecom carrier company. You answer user requests in SQL.";

    public static readonly string TableSchema = 
        @"Here is the database schema (DO NOT invent fields beyond this):
        plan_table (
        id INT, plan_name VARCHAR(100), voice_minutes INT, data_pkg_GB INT, sms_count INT,
        is_unlimited_data BIT, speed_limit_after INT, prepaid BIT, validity_days INT,
        price DECIMAL(10,2), description TEXT, is_active BIT
        )

        client_table (
        id INT, client_name VARCHAR(50), phone_number VARCHAR(15), date_of_birth DATE,
        plan_id INT (FK → plan_table.id), start_at DATE
        )";
        

    public static readonly string SampleQuery =
        @"You must use T-SQL syntax compatible with Microsoft SQL Server.

        For example:
        - Use `SELECT TOP 1 ...` instead of `LIMIT 1`
        - Use `GETDATE()` instead of `NOW()`
        - Use `+` for string concatenation instead of `CONCAT()`
        ";

    
    public static readonly string ResponseFormatHint = 
        @"
        Always return the result in the following JSON format:

        {
        ""IsExecutable"": bool,           // If request is safe and in our business scope, then return IsExecutable = true and Sql, and Message, or else return IsExecutable = false and Message used to response client
        ""Sql"": string,              // SQL statement corresponding to user's intent
        ""Message"": string           // Message to repsonse to user (e.g. plan description)
        }

        If IsExecutable == true, then Sql must be complete and syntactically valid SQL (T-SQL dialect). Only return the JSON. Do not include any explanation or prefix like 'Here is the JSON'.
        ";

    public static readonly string AdditionalRules =
        @"
        1. Only if user case is querying his/her personal information (eg. his current plan), we need do authenication by asking
        client provides phone number, and full name, and date of birth. If user omits any of phone number, full name, or date of birth, do not generate SQL. Instead, ask the user to provide the missing fields.
        For general plan query, no authenication needed.
        ";
    
    public static readonly string SQLResultHint = "The user has already executed the SQL you previously generated. Now your job is to describe the results in friendly, this is the execution result of the SQL you returned: ";
}
