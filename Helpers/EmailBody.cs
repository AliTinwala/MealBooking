using System.Web;

namespace MEAL_2024_API.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"
    <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #ffffff;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    background-color: #007BFF;
                    color: #ffffff;
                    padding: 10px 0;
                    text-align: center;
                }}
                .content {{
                    padding: 20px;
                }}
                .button {{
                    display: inline-block;
                    padding: 10px 20px;
                    margin: 20px 0;
                    font-size: 16px;
                    color: #ffffff;
                    background-color: #007BFF;
                    text-decoration: none;
                    border-radius: 5px;
                }}
                .footer {{
                    text-align: center;
                    font-size: 12px;
                    color: #888888;
                    margin-top: 20px;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Reset Your Password</h1>
                </div>
                <div class='content'>
                    <p>Hi,</p>
                    <p>You requested to reset your password for your Meal Facility account associated with {email}. Click the button below to reset it.</p>
                    <a href=""http://localhost:4200/reset?email={HttpUtility.UrlEncode(email)}&code={emailToken}"" class='button'>Reset Password</a>
                    <p>If you did not request a password reset, please ignore this email or contact support if you have questions.</p>
                    <p>Thanks,<br/>The Meal Facility Team</p>
                </div>
                <div class='footer'>
                    <p>&copy; {DateTime.Now.Year} Meal Facility. All rights reserved.</p>
                </div>
            </div>
        </body>
    </html>";
        }
    }
}
