using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Bankamatik.Business.Services;
using Bankamatik.Core.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace LogMailerApp
{
    internal class Program
    {

        private static LogService logService = new LogService(new Bankamatik.DataAccess.Repositories.LogRepository());
        static void Main(string[] args)
        {
            SendMail();
            Console.WriteLine("COMPLETE. PRESS ANY KEY TO CLOSE.");
            Console.ReadKey();
        }
        static void SendMail()
        {
            DateTime suankiZaman = DateTime.Now;
            var logs = logService.GetLogsByFilters(new Log()
            {
                StartDate = new DateTime(suankiZaman.Year, suankiZaman.Month, suankiZaman.Day),
                EndDate = new DateTime(suankiZaman.Year, suankiZaman.Month, suankiZaman.Day).AddDays(1).AddTicks(-1),
            });
            if (logs.Count > 0)
            {


                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Arda Tandogan", "ardavb4545@gmail.com"));
                message.To.Add(new MailboxAddress("Kendim", "ardavb4545@gmail.com"));
                message.Subject = $"Log Kaydı: {DateTime.Now:yyyy-MM-dd}";

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

            else
            {
                Console.WriteLine( "log kaydi yok");
            }
        }

        //log listesini html e dönüştürüyor
        static string BuildHtmlBody(List<Log> logListesi)
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

            sb.AppendLine("<th>LogID</th>");
            sb.AppendLine("<th>UserID</th>");
            sb.AppendLine("<th>ActionType</th>");
            sb.AppendLine("<th>Description</th>");
            sb.AppendLine("<th>CreatedAt</th>");
            sb.AppendLine("</tr></thead><tbody>");

            foreach (var log in logListesi)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td>{0}</td>", log.LogID);
                sb.AppendFormat("<td>{0}</td>", log.UserID?.ToString() ?? "NULL");
                sb.AppendFormat("<td>{0}</td>", System.Net.WebUtility.HtmlEncode(log.ActionType));
                sb.AppendFormat("<td>{0}</td>", System.Net.WebUtility.HtmlEncode(log.Description));
                sb.AppendFormat("<td>{0}</td>", log.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody></table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }



    }

}
