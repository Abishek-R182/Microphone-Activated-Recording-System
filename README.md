# Microphone-Activated Recording System (.NET + NAudio)

A .NET console application that **automatically starts recording** audio from the microphone when voice is detected and **stops recording** after 2 seconds of silence. Audio files are saved as `.wav` format in a user-specified directory.

---

## ✅ Features

- 🎙️ **Automatic Activation**: Starts recording only when sound is detected.
- 🛑 **Auto Stop**: Stops recording if silence lasts for more than 2 seconds.
- 💾 **File Output**: Saves audio files in `.wav` format with timestamped filenames.
- 🛠️ **No UI Required**: Simple console-based monitoring.
- 📦 **Built with NAudio** for audio capture in .NET.

---

## 🧑‍💻 How It Works

1. Continuously monitors your microphone input.
2. Starts recording when input exceeds a configured volume threshold.
3. Writes captured audio to disk while sound is active.
4. Stops recording if no sound is detected for **2 seconds**.
5. Automatically saves the recording in your local folder with a timestamp.

---

## 📂 Output

- Location: `C:\Users\Public\Documents`
- Filename format: `Recording_YYYYMMDD_HHMMSS.wav`

---

## 📋 Requirements

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/en-us/download)
- [NAudio Library](https://www.nuget.org/packages/NAudio)

---

## ⚙️ Installation

1. Clone or download the source code.
2. Open the project folder in your terminal/IDE.
3. Run the following command to install dependencies:

```bash
dotnet add package NAudio
