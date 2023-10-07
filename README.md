🤖
# 🕒 TimesheetGPT

🎯 The goal is to make timesheets easier (especially for non-devs) by:
- 📊 Getting data from various sources (currently Microsoft Graph, soon GitHub, Trello, maybe DevOps)
- 🤖 Using ChatGPT API to summarize all the data
- 📝 Using all the summaries to make a Timesheet

<img width="1800" alt="MicrosoftTeams-image (1)" src="https://github.com/bradystroud/TimesheetGPT/assets/38869720/c953851f-b5f8-4f95-aed9-bcbcb21d1e3d">

## 🛠 Technical Stuff

## 🚀 Working on TimesheetGPT

Welcome, and thank you for contributing to TimesheetGPT! Before diving into the code, here’s how to get everything set up and running on your local machine.

### 🧰 Prerequisites

Ensure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or a similar IDE with support for .NET 8
- [Git](https://git-scm.com/)

### 🖥 Setup

#### 1. **Clone the Repository**

Open a terminal or command prompt and run:
   ```bash
   git clone https://github.com/SSWConsulting/TimesheetGPT.git
   ```
Navigate to the project folder:
   ```bash
   cd TimesheetGPT/src/TimesheetGPT.WebUI
   ```

#### 2. **Run the Application**

Run the application using the following command:
   ```bash
   dotnet run
   ```
Now, you should be able to navigate to `https://localhost:7270` (or another port if you have configured it differently) in your web browser to view the application.

### 🛠 Develop and Contribute

- **Branching Strategy:** Ensure you create a new branch for the feature or fix you are working on. Do not push changes directly to the main branch.  
As per https://www.ssw.com.au/rules/do-you-know-when-to-branch-in-git
- **Commit Messages:** Write clear and concise commit messages describing the changes you are making and the reason.  
As per https://www.ssw.com.au/rules/use-emojis-in-your-commits
- **Testing:** Ensure to test your features or fixes before submitting a pull request.
- **Pull Requests:** Make sure your code is well-commented, follows the established coding style, and passes any tests before submitting a pull request.  
As per https://www.ssw.com.au/rules/write-a-good-pull-request

### 🔗 Additional Links
- **Blazor Documentation:** For help with Blazor, visit [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-5.0).
- **.NET Documentation:** For deeper dives into .NET, check out [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/).

### 🙌 Thanks for Contributing!
Your contributions and insights are integral to the growth and functionality of TimesheetGPT. Feel free to submit issues, create pull requests, or document any bugs found. Let's make TimesheetGPT stellar together!

---

Feel free to modify as per your project’s specific needs and guidelines. This is a generic guide that will help developers set up and start contributing to your project.

👉 Right now, the API isn't used at all; the Blazor Server app handles auth and then talks directly to the Application layer. This is okay for now, but the plan is to use the API to handle all the work, making it easier to integrate this into timesheeting software later.

### 🤨 Challenges and Current Plan

#### 🎫 Token Handling

💭 We're facing some challenges around where to handle OAuth token generation. Should this happen on the client side or directly within the API? For now, the thought is that acquiring the token on the client side makes the most sense, as it can then be forwarded to the API.

#### 🔒 Token Validation

🤷 Another question is how the API should validate the received token. Should there be explicit validation, or can we simply make the API request and handle any failures as they come? This is still under consideration.

#### 🚀 Sequence of Operations (Desired, but not yet implemented)

📋 The desired sequence of operations is as follows:
1. User logs in on the Blazor Client.
2. Generate an OAuth token with the required permissions (scopes).
3. Pass this token to the external API.
4. The external API uses the token to interact with Microsoft Graph or other services.

👨‍💻 We're focusing on making it work with Blazor Server first and will address these token-handling complexities later.

