using System;
using System.Collections.Generic;
using System.Net.Mail;

using ReflectSoftware.Insight.Common;
using ReflectSoftware.Insight.Common.Data;

using RI.Utils.Strings;
using RI.System.Configuration;

namespace ReflectSoftware.Insight.Listeners
{
    internal class ListenerEmail : IReflectInsightListener
	{
        private String ToAddresses { get; set; }
        private String CcAddresses { get; set; }
        private String BccAddresses { get; set; }
        private String Subject { get; set; }
        private String Body { get; set; }
        private Boolean IsHtml { get; set; }
        private MailPriority Priority { get; set; }
        private List<String> SubjectTimePatterns { get; set; }
        private List<String> BodyTimePatterns { get; set; }

        ///--------------------------------------------------------------------
        public void UpdateParameterVariables(IListenerInfo listener)
        {
            String details = listener.Params["details"];
            if (StringHelper.IsNullOrEmpty(details))
                throw new ReflectInsightException(String.Format("Missing details parameter for listener: '{0}' using details: '{1}'.", listener.Name, listener.Details));

            ConfigNode settings = new ConfigNode(ReflectInsightConfig.Settings.XmlSection);
            if (!settings.IsSectionSet)
                throw new ReflectInsightException(String.Format("Cannot find Email Details node '{0}' in configuration settings.", details));

            ConfigNode emailSettings = settings.GetConfigNode(String.Format("./emailDetails/details[@name='{0}']", details));
            if (emailSettings == null || !emailSettings.IsSectionSet)
                throw new ReflectInsightException(String.Format("Cannot find Email Details node '{0}' in configuration settings.", details));

            ToAddresses = emailSettings.GetNodeInnerText("toAddresses", String.Empty).Replace(";", ",");
            if (StringHelper.IsNullOrEmpty(ToAddresses))
                throw new ReflectInsightException(String.Format("toAddresses node text value for Email Details '{0}' in configuration settings is required.", details));

            CcAddresses = emailSettings.GetNodeInnerText("ccAddresses", String.Empty).Replace(";", ",");
            BccAddresses = emailSettings.GetNodeInnerText("bccAddresses", String.Empty).Replace(";", ",");
            Subject = emailSettings.GetNodeInnerText("subject");
            Body = emailSettings.GetNodeInnerText("body");
            IsHtml = emailSettings.GetNodeInnerText("IsHtml").Trim().ToLower() == "true";

            String sPriority = emailSettings.GetNodeInnerText("priority").ToLower().Trim();
            Priority = MailPriority.Normal;
            if (sPriority == "high")
                Priority = MailPriority.High;
            else if (sPriority == "low")
                Priority = MailPriority.Low;

            SubjectTimePatterns = RIUtils.GetListOfTimePatterns(Subject);
            BodyTimePatterns = RIUtils.GetListOfTimePatterns(Body);
        }

        ///--------------------------------------------------------------------
        public void Receive(ReflectInsightPackage[] messages)
        {
            #if NET20
            SmtpClient client = new SmtpClient();
            #else
            using (SmtpClient client = new SmtpClient())
            #endif
            {
                foreach (ReflectInsightPackage package in messages)
                {
                    String details = String.Empty;
                    if (package.IsDetail<DetailContainerString>())
                    {
                        details = package.GetDetails<DetailContainerString>().FData;
                        if (IsHtml)
                        {
                            details = details.Replace(Environment.NewLine, "<br/>");
                        }
                    }

                    using (MailMessage email = new MailMessage())
                    {
                        email.IsBodyHtml = IsHtml;
                        email.Priority = Priority;
                        email.To.Add(ToAddresses);
                        email.Subject = RIUtils.PrepareString(Subject, package, String.Empty, SubjectTimePatterns);
                        email.Body = RIUtils.PrepareString(Body, package, details, BodyTimePatterns);
                        
                        if (!StringHelper.IsNullOrEmpty(CcAddresses)) email.CC.Add(CcAddresses);
                        if (!StringHelper.IsNullOrEmpty(BccAddresses)) email.Bcc.Add(CcAddresses);

                        #if NET45
                        client.SendAsync(email, null);
                        #else
                        client.Send(email);
                        #endif
                    }
                }
            }
        }
	}
}
