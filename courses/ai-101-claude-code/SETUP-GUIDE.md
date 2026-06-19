# Getting the Course Materials on Your Computer

No developer tools required — just follow these steps for your operating system.

---

## Step 1: Install Git

Git is the tool that downloads (and keeps in sync) the course repository.

### Windows

1. Download Git from <https://git-scm.com/download/win> — the download should start automatically
2. Run the installer and accept all defaults (just keep clicking **Next**)
3. When finished, search for **"Git Bash"** in the Start menu and open it — this is your terminal for the remaining steps

### Mac

1. Open **Terminal** (press `Cmd + Space`, type `Terminal`, press Enter)
2. Type `git --version` and press Enter
3. If Git is not installed, macOS will prompt you to install the **Command Line Tools** — click **Install** and wait for it to finish

### Verify It Worked

In your terminal, type:

```bash
git --version
```

You should see something like `git version 2.43.0`. The exact number doesn't matter.

---

## Step 2: Open a Terminal

If you don't already have one open:

| OS | How to Open |
| --- | --- |
| **Windows** | Search for **Git Bash** in the Start menu (installed in Step 1) |
| **Mac** | Press `Cmd + Space`, type **Terminal**, press Enter |

> **Windows note:** Use **Git Bash**, not Command Prompt or PowerShell. Git Bash understands the commands below.

---

## Step 3: Create a Folder for the Course

Copy and paste this into your terminal, then press Enter:

**Windows (Git Bash):**

```bash
mkdir -p ~/repos && cd ~/repos
```

**Mac:**

```bash
mkdir -p ~/repos && cd ~/repos
```

This creates a `repos` folder in your home directory and moves into it.

---

## Step 4: Clone the Repository

This downloads all the course materials to your computer.

Copy and paste **one** of the following commands and press Enter:

### Option A: HTTPS (recommended for most people)

```bash
git clone https://gitlab.com/therealmanage/tools/dx/dx-training.git
```

When prompted, enter your **GitLab username** and **password** (or personal access token).

> **Password not working?** GitLab may require a Personal Access Token instead of your password. See [Creating a Personal Access Token](#creating-a-gitlab-personal-access-token) below.

### Option B: SSH (if you already have SSH keys set up)

```bash
git clone git@gitlab.com:therealmanage/tools/dx/dx-training.git
```

---

## Step 5: Open the Course Folder

```bash
cd dx-training
```

You now have all the course materials. The AI 101 course lives in:

```bash
cd courses/ai-101-claude-code
```

---

## Step 6: Verify Everything Worked

Run this command to see the course folders:

```bash
ls sessions/
```

You should see folders like `week-0`, `week-1`, `week-2`, etc.

---

## You're Done

The course materials are on your machine. Your instructor will guide you through what to do next.

**Quick reference for later:**

| What you want to do | Command |
| --- | --- |
| Go to the course folder | `cd ~/repos/dx-training/courses/ai-101-claude-code` |
| Get the latest updates | `cd ~/repos/dx-training && git pull` |
| See what's in a folder | `ls` |

---

## Troubleshooting

### "git: command not found"

- **Windows:** Make sure you're using **Git Bash**, not Command Prompt
- **Mac:** Run `xcode-select --install` to install the command line tools
- Close and reopen your terminal after installing Git

### "Authentication failed" when cloning

GitLab requires a **Personal Access Token** instead of your regular password. See the next section.

### "Permission denied (publickey)"

You used the SSH URL but don't have SSH keys configured. Use the HTTPS URL instead (Option A in Step 4).

### "fatal: destination path already exists"

You've already cloned the repo. Just go into the folder:

```bash
cd ~/repos/dx-training
```

To get the latest updates:

```bash
git pull
```

---

## Creating a GitLab Personal Access Token

If your GitLab password doesn't work when cloning, you need a Personal Access Token:

1. Go to <https://gitlab.com/-/user_settings/personal_access_tokens>
2. Sign in with your RealManage GitLab account
3. Click **Add new token**
4. Fill in:
   - **Token name:** `course-access` (or anything you like)
   - **Expiration date:** pick a date a few months out
   - **Scopes:** check **read_repository**
5. Click **Create personal access token**
6. **Copy the token immediately** — you won't be able to see it again
7. When Git asks for your password during clone, **paste the token** instead

---

## Need Help?

- Ask in the **#ai-exchange** Slack channel
- Ask your instructor during the session
