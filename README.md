# 🕒 TimesheetGPT

https://timesheet-gpt.azurewebsites.net/

🎯 The goal is to make timesheets easier (especially for non-devs) by:
- 📊 Getting data from various sources (currently Microsoft Graph (sent emails, meetings, calls), soon GitHub, maybe Trello and DevOps)
- 🤖 Using Semantic Kernal to summarize all the data
- 📝 Using all the summaries to make a timesheet

![](https://github.com/bradystroud/TimesheetGPT/assets/38869720/c953851f-b5f8-4f95-aed9-bcbcb21d1e3d)
**Figure: **

## 🛠 Technical Stuff

### 🚀 Working on TimesheetGPT

Welcome, and thank you for contributing to TimesheetGPT! Before diving into the code, here’s how to get everything set up and running on your local machine.

#### 🧰 Prerequisites

Ensure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or a similar IDE with support for .NET 8
- [Git](https://git-scm.com/)

#### 🖥 Setup

##### 1. **Clone the Repository**

Open a terminal or command prompt and run:
   ```bash
   git clone https://github.com/SSWConsulting/TimesheetGPT.git
   ```
Navigate to the project folder:
   ```bash
   cd TimesheetGPT/src/TimesheetGPT.WebUI
   ```

##### 2. **Run the Application**

Run the application using the following command:
   ```bash
   dotnet run
   ```
Now, you should be able to navigate to `https://localhost:7270` (or another port if you have configured it differently) in your web browser to view the application.

#### 🛠 Develop and Contribute

- **Branching Strategy:** Ensure you create a new branch for the feature or fix you are working on. Do not push changes directly to the main branch.  
As per https://www.ssw.com.au/rules/do-you-know-when-to-branch-in-git
- **Commit Messages:** Write clear and concise commit messages describing the changes you are making and the reason.  
As per https://www.ssw.com.au/rules/use-emojis-in-your-commits
- **Testing:** Ensure to test your features or fixes before submitting a pull request.
- **Pull Requests:** Make sure your code is well-commented, follows the established coding style, and passes any tests before submitting a pull request.  
As per https://www.ssw.com.au/rules/write-a-good-pull-request

#### 🔗 Additional Links
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Semantic Kernel](https://learn.microsoft.com/en-us/semantic-kernel/overview/)

#### 🙌 Thanks for Contributing!
Your contributions and insights are integral to the growth and functionality of TimesheetGPT. Feel free to submit issues, create pull requests, or document any bugs found. Let's make TimesheetGPT stellar together!
