using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace LogMailerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SendMail();
            Console.WriteLine("COMPLETE. PRESS ANY KEY TO CLOSE.");
            Console.ReadKey();
        }

        static void SendMail()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=BankamatikDB;Trusted_Connection=True;";

            var logs = GetTodaysLogs(connectionString);

            if (logs.Rows.Count == 0)
            {
                Console.WriteLine("No Log For Today.");
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Arda Tandogan", "ardavb4545@gmail.com"));
            message.To.Add(new MailboxAddress("Kendim", "ardavb4545@gmail.com"));
            message.Subject = $"Log Kaydı: {DateTime.Now:yyyyMMdd}";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = BuildHtmlBody(logs)
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("ardavb4545@gmail.com", "uazsehhtzezncasq");
                client.Send(message);
                client.Disconnect(true);

                Console.WriteLine("Mail sent succesfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mail send error: " + ex.Message);
            }
        }

        static DataTable GetTodaysLogs(string connectionString)
        {
            var dt = new DataTable();

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand("SELECT LogID, UserID, ActionType, Description, CreatedAt FROM Logs WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            dt.Load(reader);

            return dt;
        }

        static string BuildHtmlBody(DataTable logs)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"tr\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"UTF-8\" />");
            sb.AppendLine("  <style>");
            sb.AppendLine("    table { border-collapse: collapse; width: 90%; margin: 20px auto; font-family: Arial, sans-serif; }");
            sb.AppendLine("    th, td { border: 1px solid #444; padding: 8px 12px; text-align: center; }");
            sb.AppendLine("    th { background-color: #007BFF; color: white; }");
            sb.AppendLine("    tr:nth-child(even) { background-color: #f2f2f2; }");
            sb.AppendLine("    tr:hover { background-color: #d1e7ff; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h2 style='text-align:center;'>Bugünün Log Kayıtları</h2>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead><tr>");

            foreach (DataColumn col in logs.Columns)
                sb.AppendFormat("<th>{0}</th>", col.ColumnName);

            sb.AppendLine("</tr></thead><tbody>");

            foreach (DataRow row in logs.Rows)
            {
                sb.AppendLine("<tr>");
                foreach (var item in row.ItemArray)
                    sb.AppendFormat("<td>{0}</td>", System.Net.WebUtility.HtmlEncode(item?.ToString() ?? ""));
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody></table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}
