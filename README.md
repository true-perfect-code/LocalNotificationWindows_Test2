During my vacation I actually wanted to work with MAUI again and tackle some “old” problems, e.g. the local notification in Windows. I wanted to create this project in NET 9, Visual Studio 2022 version 17.12.3 on 14.01.2025. Unfortunately, I can't even create the Visual Studio MAUI template (Counter App) because I get the message “The program ‘[42104] LocalNotificationWindows.exe’ has exited with code 3221226356 (0xc0000374).”. I have found some posts on this topic that are very recent.

The problem with the counter app (MAUI Blazor template) has been solved by repairing Visual Studio and creating a new project. Therefore there is a second project: https://github.com/true-perfect-code/LocalNotificationWindows_Test2

Both Github posts can be seen at:

1. dotnet/maui#26875
2. dotnet/maui#27092
