# linux-monitoring-console
A console to send notification to email or discord when disk usage has reached the specified threshold using C# (.NET 6).

Since runcloud did not have feature to monitor external disk attached in the server, I decided to make a console program to monitor the disk usage.
This is usefull when you have use case of putting database files in the block storage. This make the storage easily scalable and not bind to the linode pricing plans.
For now the console only support disk checking only

To setup the console:
1) Download the release.
2) Upload the binary into your server.
3) Change the AppSettings.json.
4) Make the LinuxMonitoringConsole executable.
5) Create a new cronjob following your specification (either check every minute, 10 minute or so on). For example:
    > */10 * * * *  cd /home/runcloud/console/LinuxMonitoring && ./LinuxMonitoringConsole

AppSettings.json details: 
```
{
    "AppSettings": {
        "ServerName": "Test",             // Your Server name, will be used for title.
        "CheckDiskUsage": {
            "Enabled": true,              // If disable the program will skip treshold check.
            "FileSystems": [              // Filesystem to be check.
                "/dev/sdb",
                "/mnt/c"
            ],
            "TresholdAlert": 90,          // Treshold precentage to be checked.
            "NotifyEmail": false,         // If Enabled, Notification will be send to email.
            "NotifyDiscord": true,        // If Enabled, Notification will be send to discord.
            "NotifyOnSpecificDate": null  // If null, The program will make a check daily, If an array of date is entered, it will only check on the specific day defined only.
        },
        "CheckAllDisk": {
            "Enabled": true,                      // If disable the program will skip all disk status check.
            "NotifyEmail": true,                  // If Enabled, Notification will be send to email.
            "NotifyDiscord": true,                // If Enabled, Notification will be send to discord.
            "NotifyOnSpecificDate": [ 1, 16, 9 ]  // If null, The program will make a check daily, If an array of date is entered, it will only check on the specific day 
        },
        "EmailSettings": {                         // SMTP Settings, if NotifyEmail is true, this block become mandatory
            "Host": "email-smtp.ap-southeast-2.amazonaws.com",
            "Username": "",
            "Password": "",
            "Port": 587,
            "Encryption": "ssl",
            "FromEmail": "no-reply@centricpos.com",
            "FromName": "Server Monitoring",
            "ToEmail": [
                ""
            ],
            "CcEmail": [
                ""
            ]
        },
        "DiscordSettings": {            // Discord Settings, if NotifyDiscord is true, this block become mandatory
            "WebhookId": "",            // Refer disord documentation to get the webhook id and token
            "WebhookToken": ""
        }
    }
}
```

Feel free to make contribution on the project.
