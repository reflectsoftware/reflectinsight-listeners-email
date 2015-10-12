REFLECTSOFTWARE INC.
REFLECTINSIGHT LOGGING LIBRARY
VERSION 5.5

RELEASE NOTES

Version 5.5.3
- New feature in email details configuration. Can now explicitly set whether to send emails using async if in .NET 4.5. Default is true. 

Version 5.5.1
- Bug fixes for packages improperly being downloaded. RabbitMQ is now a nuget dependency.

Version 5.5.0
- New HttpRequest message type
- New JSON message type
- General bug fixes and enhancements
- Performance tuning

Version 5.4.0
- Added auto save log file by file size
- Added support for overloading ConfigurationMode to avoid a local configuration file
- Minor bug fixes and enhancements 

Version 5.3.1
- New message type SendMiniDumpFile 
- New message type SendTypedCollection (renamed from SendEnumerable which is now deprecated) 
- New message type SendString (can send String and StringBuilder objects, will also show NULL for null strings) 
- XML documentation for methods
- Fine tuning and performance improvements
- Bug fixes

Version 5.2.1
- Performance improvements.
- Bug fixes.

Version 5.2
- New SendEnumeration message type.
- Performance improvements.
- Bug fixes.

Version 5.1
- This version of the library now requires ReflectInsight 5.1 to be installed for the Viewer to work. 
- As of version 5.1 of the ReflectInsight NuGet Package, you are now required to manually add in the ReflectInsight configuration details that you may require. For help on configuration, please see our documentation on the website (http://reflectsoftware.com) or from the knowledgebase articles on our UserVoice site (http://reflectsoftware.uservoice.com/knowledgebase). 

Feedback and Support
http://reflectsoftware.uservoice.com/

ReflectSoftware
http://reflectsoftware.com