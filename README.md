🤖
# 🕒 TimesheetGPT

🎯 The goal is to make timesheets easier (especially for non-devs) by:
- 📊 Getting data from various sources (currently Microsoft Graph, soon GitHub, Trello, maybe DevOps)
- 🤖 Using ChatGPT API to summarize all the data
- 📝 Using all the summaries to make a Timesheet

## 🛠 Technical Stuff

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

