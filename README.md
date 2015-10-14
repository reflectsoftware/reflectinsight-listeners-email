# ReflectInsight-Listeners-Email

[![Build status](https://ci.appveyor.com/api/projects/status/github/reflectsoftware/reflectinsight-listeners-email?svg=true)](https://ci.appveyor.com/project/reflectsoftware/reflectinsight-listeners-email)
[![Release](https://img.shields.io/github/release/reflectsoftware/reflectinsight-listeners-email.svg)](https://github.com/reflectsoftware/reflectinsight-listeners-email/releases/latest)
[![NuGet Version](http://img.shields.io/nuget/v/reflectsoftware.insight.listeners.email.svg?style=flat)](http://www.nuget.org/packages/ReflectSoftware.Insight.Listeners.Email/)
[![NuGet](https://img.shields.io/nuget/dt/reflectsoftware.insight.listeners.email.svg)](http://www.nuget.org/packages/ReflectSoftware.Insight.Listeners.Email/)
[![Stars](https://img.shields.io/github/stars/reflectsoftware/reflectinsight-listeners-email.svg)](https://github.com/reflectsoftware/reflectinsight-listeners-email/stargazers)

## Overview ##

We've added a new destination listener extension for ReflectInsight for sending emails. This new destination listener extension is called ReflectSoftware.Insight.Listeners.Email and allows you to send logging messages via email. This is typically done for Exceptions, Errors and Fatal messages types where you might want to be notified via email if something went wrong in your application.

## Benefits of ReflectInsight Listeners ##

The benefits to using the Insight Listeners is that you can easily and quickly add them to your applicable with little effort and then send ReflectInsight logging messages to other destinations.

## Getting Started

To install ReflectSoftware.Insight.Listeners.Email listener, run the following command in the Package Manager Console:

```powershell
Install-Package ReflectSoftware.Insight.Listeners.Email
```
Then in your app.config or web.config file, add the following configuration sections:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>        
    <section name="insightSettings" type="ReflectSoftware.Insight.ConfigurationHandler,ReflectSoftware.Insight"/>
  </configSections>
	
  <insightSettings>
    <listeners>
      <listener name="Email" type="ReflectSoftware.Insight.Listeners.ListenerEmail, ReflectSoftware.Insight.Listeners.Email"/>
    </listeners>

    <emailDetails>
      <details name="emailDetails1">
        <Async>True</Async>
        <IsHtml>True</IsHtml>
        <toAddresses>YourEmail@Address.com</toAddresses>
        <ccAddresses></ccAddresses>
        <bccAddresses></bccAddresses>
        <subject>Application Alert: %message%</subject>
        <priority>High</priority>
        <body>
          <![CDATA[
          The following error was detected in application: '%application%'<br/><br/>          
          Message Type: %messagetype% 
			    Category:     %category% 
			    Computer:     %machine% 
			    Sender Id:    %senderid% 
			    Request Id:   %requestid% 
			    Process Id:   %processid% 
			    Thread Id:    %threadid% 
			    Domain Id:    %domainid% 
			    Application:  %application% 
			    User Domain:  %userdomain% 
			    Username:     %username%
                            Timestamp:    %time% or %time{dd-MM-yyyy hh:mm:ss.fff}%
          <b>%message%</b><br/><br/>  
          <b>%details%</b><br/><br/>
          Please call technical support: 1-888-555-5555
          ]]>
        </body>
      </details>
    </emailDetails>

    <listenerGroups active="Active">
      <group name="Active" enabled="true" maskIdentities="false">
        <destinations>
          <destination name="Email" enabled="true" filter="ErrorWarningFilter" details="Email[details=emailDetails1]"/>
        </destinations>
      </group>
    </listenerGroups>
    
    <filters>
      <filter name="ErrorWarningFilter" mode="Include">
        <method type="SendError"/>
        <method type="SendException"/>
        <method type="SendFatal"/>
      </filter>
    </filters>
  </insightSettings>
	
	<system.net>
		<mailSettings>
			<smtp from="ReflectInsight@demo.com">
				<network host="smtpserver1" port="25" userName="username" password="secret" defaultCredentials="true"/>
			</smtp>
		</mailSettings>
	</system.net>
</configuration>
```

Additional configuration details for the ReflectSoftware.Insight.Listeners.Email listener can be found [here](https://reflectsoftware.atlassian.net/wiki/display/RI5/Email+Listener).

## Additional Resources

[Documentation](https://reflectsoftware.atlassian.net/wiki/display/RI5/ReflectInsight+5+documentation)

[Knowledge Base](http://reflectsoftware.uservoice.com/knowledgebase)

[Submit User Feedback](http://reflectsoftware.uservoice.com/forums/158277-reflectinsight-feedback)

[Contact Support](support@reflectsoftware.com)

[ReflectSoftware Website](http://reflectsoftware.com)
