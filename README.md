# Project Overview

## 🔧 Project Structure

This project follows a clean and modular architecture using several design patterns and best practices:

- **Service Locator Pattern** – Centralized access to game-wide services.
- **MVC (Model-View-Controller)** – Clean separation of game logic, UI, and data.
- **Observer Pattern** – Event-based communication between systems.
- **Singleton Pattern** – Ensures a single instance for critical managers.

All scripts communicate through a central `GameService`, promoting loose coupling and maintainability.

---

## 🎯 Quest System Design

The quest system is built to be **scalable**, **modular**, and **extensible** using **ScriptableObjects**:

- A **base ScriptableObject class** (`QuestSO`) defines the core structure and methods for quests.
- Each specific quest inherits from this base and overrides relevant methods to define custom behavior.
- This design allows easy addition of new quests without modifying existing code, supporting flexibility and reusability.

---

## ✨ Additional Features

- **Shooting System**: Implemented using **Raycasting** to simulate bullets and hit detection.
- **Particle Effects**: Integrated with the shooting mechanic to enhance visual feedback.
- **Clean Separation of Concerns**: Each system is independently designed for easy testing and scalability.

---

## 📁 Included

- Used AI for Planning the project.
- Video Link : https://drive.google.com/file/d/1NE7mA_XmWuEqkg46iKHh4OmZK7AyJu7B/view?usp=drive_link

---

# Realm Rush – Quest & Game Data Editor Tool

## 📋 Tool Overview
A Unity Editor tool for designers to create, edit, and manage quest data as ScriptableObjects — no coding required.
- Video Link : https://drive.google.com/file/d/1CGPEjfp0vG9paFFFUMNoDSCF3npu36nT/view?usp=drive_link

## 🚀 Features
- Create Fetch, Kill, and Explore quests
- User-friendly editor window
- Dynamic fields based on quest type
- Validation and feedback
- Inline tooltips and help text
- Data saved to `Assets/ScriptableObjects/Quests/`

## 🧰 Tool Usage
1. Open via `RealmRush Tools > Quest Editor`
2. Choose Quest Type
3. Fill out required fields
4. Click **Create Quest**

## 🏗️ Architecture
- `QuestSO.cs`: Base abstract ScriptableObject
- Specific quest types: `FetchQuestSO.cs`, `KillQuestSO.cs`, `ExploreQuestSO.cs`
- `QuestEditor.cs`: UnityEditor tool handling logic and UI

## 🧠 AI Usage
- Referred Youtube for gaining knowledge of editor windows.
- Used AI for planning the project and to Add comments. 
